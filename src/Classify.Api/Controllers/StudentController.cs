using Classify.Domain.Entities;
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

    [HttpPost("upload")]
    public async Task<IActionResult> Post([FromForm] FileDto file)//string path)//[FromForm] FileDto file)
    {
        // return Ok(await this.readerService.GetFromExcelAsync(path));
        if (file != null && file.File.Length > 0)
        {
            var filePath = Path.GetTempFileName(); // Генерируем уникальное имя для временного файла
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.File.CopyTo(stream); // Сохраняем файл на сервере
            }

            // Получаем путь к сохраненному файлу
            var savedFilePath = Path.GetFullPath(filePath);

            // Дальнейшая обработка пути файла
            return Ok(await this.readerService.GetFromExcelAsync(savedFilePath));
        }
        else
        {
            return BadRequest("Файл не был загружен.");
        }
    }

    [HttpGet("Id")]
    public async Task<IActionResult> GetById(int id) =>
        Ok(await studentService.RetrieveById(id));
    }