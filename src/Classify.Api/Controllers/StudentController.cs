using Classify.Api.ExcelReadre;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.PortableExecutable;

namespace Classify.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Students : ControllerBase
    {
        private readonly ILogger<Students> logger;
        ExcelReader reader;

        public Students(ILogger<Students> logger)
        {
            reader = new ExcelReader();
            this.logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }
    }
}