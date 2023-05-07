using Classify.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Classify.Api.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class Students : ControllerBase
    {

        protected readonly ILogger<Students> logger;
        protected readonly IExcelReaderService readerService;

        public Students(ILogger<Students> logger, IExcelReaderService readerService)
        {
            this.logger = logger;
            this.readerService = readerService;
        }

        [HttpGet("Path")]
        public async Task<IActionResult> Get(string path)
        {
            //return Ok(await this.readerService.GetFromExcelAsync("C:\\Users\\Djava\\Desktop\\SirdaryoPrezident.xlsx"));
            return Ok(await this.readerService.GetFromExcelAsync(path));
        }
    }
}