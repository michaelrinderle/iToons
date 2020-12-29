using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace iToons.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MusicController : ControllerBase
    {
        private readonly ILogger<MusicController> _logger;

        public MusicController(ILogger<MusicController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("GetMetaData")]
        public ActionResult GetMetaData(int id)
        {
            // need object result because of Metadata byte[] prop
            return new ObjectResult(Program.Music.GetMetaData(id));
        }

        [HttpGet]
        [Route("GetSongStream")]
        public ActionResult GetSongStream(int id)
        {
            return File(Program.Music.GetSongStream(id), "audio/mpeg", "id.mp3");
        }
    }
}
