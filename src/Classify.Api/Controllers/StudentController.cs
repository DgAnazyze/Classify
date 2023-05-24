using Classify.Domain.Configurations;
using Classify.Domain.Entities;
using Classify.Service.DTOs.LoginDto;
using Classify.Service.DTOs.Students;
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
    [HttpPost("add")]
    public async Task<IActionResult> AddStudentAsync(StudentCreationDto dto) =>
      Ok(await studentService.AddAsync(dto));

    [HttpPatch("update")]
    public async Task<IActionResult> UpdateStudentAsync(int id, StudentUpdateDto dto) =>
        Ok(await studentService.ModifyAsync(id, dto));

    [HttpGet("Id")]
    public async Task<IActionResult> GetByIdAsync(int id) =>
        Ok(await studentService.RetrieveById(id));

    [HttpGet("all")]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params, string search) =>
       Ok(await studentService.RetrieveAllAsync(@params, search));

    [HttpGet("Number of birth certificate")]
    public async Task<IActionResult> GetByBirthCertificateNumberAsync(string certificateNumber) =>
        Ok(await studentService.RetrieveByBirthCertificateNumberAsync(certificateNumber));

    [HttpGet("Number of passport")]
    public async Task<IActionResult> GetByPassportNumberAsync(string passportNumber) =>
        Ok(await studentService.RetrieveByPassportNumberAsync(passportNumber));

    [HttpGet("school")]
    public async Task<IActionResult> GetBySchoolAsync([FromQuery] PaginationParams @params, string school) =>
        Ok(await studentService.RetrieveBySchoolAsync(@params, school));

    [HttpGet("region")]
    public async Task<IActionResult> GetByRegionAsync([FromQuery] PaginationParams @params, string region) =>
        Ok(await studentService.RetrieveByRegionAsync(@params, region));    
}