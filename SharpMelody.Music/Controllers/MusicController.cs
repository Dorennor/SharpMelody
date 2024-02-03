using Microsoft.AspNetCore.Mvc;

using Serilog;
using SharpMelody.Music.Entities;
using SharpMelody.Music.Services;

namespace SharpMelody.Music.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private readonly LibraryService _libraryService;

        public MusicController(LibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        [Route("")]
        [HttpGet]
        public string GetGreetings()
        {
            return "SharpMelody";
        }

        [Route("refresh")]
        [HttpPost]
        public async void RefreshLibrary()
        {
            await _libraryService.Refresh();
        }

        [Route("performers/{name}")]
        [HttpGet]
        public Performer GetPerformer(string name = "Alestorm")
        {
            var performer = _libraryService.Performers.FirstOrDefault(item => string.Equals(item.Name, name, StringComparison.InvariantCulture));

            if (performer == null)
            {
                Log.Warning($"{DateTime.Now} Performer {name} doesn't exist.");
                throw new NullReferenceException();
            }

            return performer;
        }

        [Route("performers/getPerformerById/{id}")]
        [HttpGet]
        public Performer GetPerformer(Guid id)
        {
            var performer = _libraryService.Performers.FirstOrDefault(item => item.Id.Equals(id));

            if (performer == null)
            {
                Log.Warning($"{DateTime.Now} Performer {id} doesn't exist.");
                throw new NullReferenceException();
            }

            return performer;
        }

        [Route("performers/getPerformerAlbumsByName/{name}")]
        [HttpGet]
        public List<Album> GetPerformerAlbums(string name = "Alestorm")
        {
            var performer = _libraryService.Performers.FirstOrDefault(item => string.Equals(item.Name, name, StringComparison.InvariantCulture));

            if (performer == null)
            {
                Log.Warning($"{DateTime.Now} Performer {name} doesn't exist.");
                throw new NullReferenceException();
            }

            var albums = performer.Albums;

            if (albums == null)
            {
                Log.Warning($"{DateTime.Now} No albums.");
                throw new NullReferenceException();
            }

            return albums;
        }

        [Route("performers/getPerformerAlbumsById/{id}")]
        [HttpGet]
        public List<Album> GetPerformerAlbums(Guid id)
        {
            var performer = _libraryService.Performers.FirstOrDefault(item => item.Id.Equals(id));

            if (performer == null)
            {
                Log.Warning($"{DateTime.Now} Performer {id} doesn't exist.");
                throw new NullReferenceException();
            }

            var albums = performer.Albums;

            if (albums == null)
            {
                Log.Warning($"{DateTime.Now} No albums.");
                throw new NullReferenceException();
            }

            return albums;
        }
    }
}