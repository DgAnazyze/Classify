using Classify.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Classify.Api.Controllers;

[Route("api/students")]
[ApiController]
public class Students : ControllerBase
{
    protected readonly IExcelReaderService readerService;
    protected readonly IStudentService studentService;

    public Students(IExcelReaderService readerService, IStudentService studentService)
    {
        this.readerService = readerService;
        this.studentService = studentService;
    }

    [HttpPost("Path")]
    public async Task<IActionResult> Post(string path)
    {
        //return Ok(await this.readerService.GetFromExcelAsync("C:\\Users\\Djava\\Desktop\\SirdaryoPrezident.xlsx"));
        return Ok(await this.readerService.GetFromExcelAsync(path));
    }
    [HttpGet("Id")]
    public async Task<IActionResult> Get(int id)
        => Ok(await this.studentService.GetAsync(x => x.Id == id));

}