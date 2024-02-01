using SharpMelody.Music.Data;
using SharpMelody.Music.Entities;

using TagLib.Flac;

namespace SharpMelody.Music.Services
{
    public class LibraryService
    {
        private readonly FileSystemWatcher _fileSystemWatcher;
        private readonly MusicDbContext _musicDbContext;
        private readonly string _musicPath;
        public List<Performer> Performers { get; set; } = new List<Performer>();

        public LibraryService(MusicDbContext musicDbContext)
        {
            _musicDbContext = musicDbContext;

            _musicPath = Path.Combine(Directory.GetCurrentDirectory(), "Music");

            InitializeLibrary();

            _fileSystemWatcher = new FileSystemWatcher
            {
                Path = _musicPath,
                NotifyFilter = NotifyFilters.LastWrite,
                Filter = "*.*",
                EnableRaisingEvents = true,
                IncludeSubdirectories = true
            };

            _fileSystemWatcher.Changed += new FileSystemEventHandler((sender, args) => InitializeLibrary());
        }

        public async Task Refresh()
        {
            await Task.Run(InitializeLibrary);
        }

        private void InitializeLibrary()
        {
            Performers = new List<Performer>();

            string[] performersPaths = Directory.GetDirectories(_musicPath);

            foreach (var performerPath in performersPaths)
            {
                Performer performer = new Performer
                {
                    Id = Guid.NewGuid(),
                    Name = performerPath.Split('\\').Last(),
                    Albums = new List<Album>()
                };

                string[] albumsPaths = Directory.GetDirectories(performerPath);

                foreach (var albumPath in albumsPaths)
                {
                    Album album = new Album
                    {
                        Id = Guid.NewGuid(),
                        Title = albumPath.Split('\\').Last(),
                        Songs = new List<Song>(),
                        PerformerId = performer.Id
                    };

                    performer.Albums.Add(album);

                    _musicDbContext.Performsers.Add(performer);
                    _musicDbContext.Albums.Add(album);

                    foreach (var songName in Directory.GetFiles(albumPath))
                    {
                        var songFile = TagLib.File.Create(songName);

                        performer.MetaGenre = songFile.Tag.FirstGenre;
                        album.Genres = songFile.Tag.Genres;

                        Song song = new Song
                        {
                            Id = Guid.NewGuid(),
                            Title = songFile.Tag.Title,
                            AlbumId = album.Id,
                            PerformerId = performer.Id,
                            Genres = songFile.Tag.Genres,
                            Year = songFile.Tag.Year
                        };

                        var tags = songFile.Tag.GetType().GetProperties();

                        foreach (var tag in tags)
                        {
                            try
                            {
                                var value = (string)(tag.GetValue(songFile.Tag) ?? string.Empty);
                                song.Metadata.Add(tag.Name, value);
                            }
                            catch
                            {
                                continue;
                            }
                        }

                        album.Songs.Add(song);
                        Performers.Add(performer);

                        _musicDbContext.Songs.Add(song);
                    }
                }

                _musicDbContext.SaveChanges();
            }
        }
    }
}