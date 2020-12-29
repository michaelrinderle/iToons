using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using iToons.Library.Entity;
using Microsoft.Extensions.Logging;

namespace iToons.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {
        private readonly ILogger<DataController> _logger;

        public DataController(ILogger<DataController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<List<User>> GetAllUsers()
        {
            return await Program.Data.GetAllUsers();
        }
    }
}
