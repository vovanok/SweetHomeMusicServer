using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SweetHomeMusicServer.Data;
using SweetHomeMusicServer.Services;

namespace SweetHomeMusicServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private readonly MusicService musicService;

        private readonly Random random = new();
        private MusicCatalog? lastMusicCatalog;

        public MusicController(MusicService musicService)
        {
            this.musicService = musicService;
        }

        [HttpGet("~/getmusiccatalog")]
        public MusicCatalog GetMusicCatalog()
        {
            return GetLastMusicCatalog();
        }

        [HttpGet("~/gettrack")]
        public ActionResult GetTrack(string bandName, string trackName)
        {
            MusicTrack? musicTrack = musicService.GetTrack(GetLastMusicCatalog(), bandName, trackName);
            if (musicTrack == null)
                return new EmptyResult();
            
            return File(musicTrack.FilePath, MediaTypeNames.Application.Octet);
        }
        
        [HttpGet("~/getrandomtrack")]
        public ActionResult GetRandomTrack()
        {
            MusicCatalog musicCatalog = GetLastMusicCatalog();
            
            MusicBand randomBand = musicCatalog.Bands[random.Next(musicCatalog.Bands.Length)];
            MusicTrack randomTrack = randomBand.Tracks[random.Next(randomBand.Tracks.Length)];
        
            return GetTrack(randomBand.Name, randomTrack.Name);
        }

        private MusicCatalog GetLastMusicCatalog()
        {
            if (lastMusicCatalog == null)
            {
                lastMusicCatalog = musicService.GetCatalog("/Users/vovanok/Builds/TestMusic");
                Console.WriteLine($">>> GetLastMusicCatalog {lastMusicCatalog.Bands.Length}");
            }

            return lastMusicCatalog ?? new MusicCatalog(Array.Empty<MusicBand>());
        }
    }
}
