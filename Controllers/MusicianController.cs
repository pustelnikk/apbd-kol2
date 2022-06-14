using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using kol2b.Models.DTOs;
using kol2b.Services;
using System.Threading.Tasks;
using System.Linq;
using kol2b.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace kol2b.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicianController : ControllerBase
    {

        private readonly IMusicService _service;

        public MusicianController(IMusicService service)
        {
            _service = service;
        }

        [HttpGet("{IdMusician}")]
        public async Task<IActionResult> GetMusician(int IdMusician)
        {

            var exist = await _service.DoesMusicianExist(IdMusician).FirstOrDefaultAsync();

            if (exist == null)
            {
                return NotFound();
            }

            return Ok(await _service.GetMusician(IdMusician).Select(e =>
            new MusicianGet
            {
                FirstName = e.FirstName,
                LastName = e.LastName,
                Nickname = e.Nickname,
                Tracks = e.MusicianTrack.Select(e => new Track
                {
                    IdTrack = e.IdTrack,
                    TrackName = e.Track.TrackName,
                    Duration = e.Track.Duration,
                    IdMusicAlbum = e.Track.IdMusicAlbum

                }).ToList()

            }).ToListAsync()
            );

        }


        [HttpDelete]
        public async Task<IActionResult> DeleteMusician(int IdMusician)
        {
            var exist = await _service.DoesMusicianExist(IdMusician).FirstOrDefaultAsync();

            if (exist == null) return NotFound();


            var toBeDeleted = await _service.GetMusician(IdMusician).Select(e =>
              new MusicianGet
              {
                  FirstName = e.FirstName,
                  LastName = e.LastName,
                  Nickname = e.Nickname,
                  Tracks = e.MusicianTrack.Select(e => new Track
                  {
                      IdTrack = e.IdTrack,
                      TrackName = e.Track.TrackName,
                      Duration = e.Track.Duration,
                      IdMusicAlbum = e.Track.IdMusicAlbum

                  }).ToList()

              }).ToListAsync();

            var tracks = toBeDeleted[0];

            foreach (Track track in tracks.Tracks)
            {
                if (track.IdMusicAlbum != null) return Conflict();
            }

            await _service.DeleteMusician(IdMusician);





            return NoContent();

        }
    }
}
