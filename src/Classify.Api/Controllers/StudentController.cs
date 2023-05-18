using Classify.Service.DTOs.LoginDto;
using Classify.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Classify.Api.Controllers;

[Route("api/students")]
[ApiController]
public class Students : ControllerBase
{

    protected readonly ILogger<Students> logger;
    protected readonly IExcelReaderService readerService;
    protected readonly IStudentService studentService;

    public Students(ILogger<Students> logger, IExcelReaderService readerService, IStudentService studentService)
    {
        this.logger = logger;
        this.readerService = readerService;
        this.studentService = studentService;
    }

    [HttpPost("Path")]
    public async Task<IActionResult> Post([FromForm] FileDto file)
    {
        //string d = file;
        //return Ok(await this.readerService.GetFromExcelAsync("C:\\Users\\Djava\\Desktop\\SirdaryoPrezident.xlsx"));
        return Ok(await this.readerService.GetFromExcelAsync(d));
    }

    [HttpGet("Id")]
    public async Task<IActionResult> GetById(int id) =>
        Ok(await studentService.RetrieveById(id));
    }