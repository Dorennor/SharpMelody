using System.Text.RegularExpressions;
using SharpMelody.Music.Data;
using SharpMelody.Music.Entities;

using TagLib.Flac;

namespace SharpMelody.Music.Services
{
    //TODO Interface
    public class LibraryService
    {
        private readonly Regex _pattern;
        private readonly FileSystemWatcher _musicFileSystemWatcher;
        private readonly FileSystemWatcher _inputFIleSystemWatcher;
        private readonly MusicDbContext _musicDbContext;
        private readonly string _musicPath;
        private readonly string _inputPath;
        public List<Performer> Performers { get; set; } = new List<Performer>();

        public LibraryService(MusicDbContext musicDbContext)
        {
            _musicDbContext = musicDbContext;

            _musicPath = Path.Combine(Directory.GetCurrentDirectory(), "Music");

            InitializeLibrary();

            _musicFileSystemWatcher = new FileSystemWatcher
            {
                Path = _musicPath,
                NotifyFilter = NotifyFilters.LastWrite,
                Filter = "*.*",
                EnableRaisingEvents = true,
                IncludeSubdirectories = true
            };

            _musicFileSystemWatcher.Changed += new FileSystemEventHandler((sender, args) => InitializeLibrary());

            _inputPath = Path.Combine(Directory.GetCurrentDirectory(), "Input");

            _inputFIleSystemWatcher = new FileSystemWatcher
            {
                Path = _inputPath,
                NotifyFilter = NotifyFilters.LastWrite,
                Filter = "*.*",
                EnableRaisingEvents = true,
                IncludeSubdirectories = true
            };

            _inputFIleSystemWatcher.Changed += new FileSystemEventHandler((sender, args) => InitialzieStorage());

            _pattern = new Regex("[;:,*?|/]|[.]{3}");

        }

        public async Task Refresh()
        {
            await Task.Run(InitializeLibrary);
        }


        private void InitialzieStorage()
        {
            Thread.Sleep(300);

            string[] filesPath = Directory.GetFiles(_inputPath);

            foreach (var filePath in filesPath)
            {
                //TODO StringBuilder?
                var songFile = TagLib.File.Create(filePath);
                var songName = filePath.Split('\\').Last();

                var newFilePath = _musicPath + "\\" + _pattern.Replace(songFile.Tag.FirstPerformer, "_");

                Directory.CreateDirectory(newFilePath);

                newFilePath += "\\" + _pattern.Replace(songFile.Tag.Album, "_");

                Directory.CreateDirectory(newFilePath);

                newFilePath += "\\" + _pattern.Replace(songName, "_");

                System.IO.File.Move(filePath, newFilePath, true);
            }
        }

        private void InitializeLibrary()
        {
            Thread.Sleep(300);

            //TODO StringBuilder?
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

                    //_musicDbContext.Performsers.Add(performer);
                    //_musicDbContext.Albums.Add(album);

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
                            //TODO remove empty strings
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

                        //_musicDbContext.Songs.Add(song);
                    }
                }

                //_musicDbContext.SaveChanges();
            }
        }
    }
}