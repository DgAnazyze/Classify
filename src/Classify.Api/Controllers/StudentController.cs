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
      Ok(await this.studentService.AddAsync(dto));

    [HttpPatch("update")]
    public async Task<IActionResult> UpdateStudentAsync(int id, StudentUpdateDto dto) =>
        Ok(await this.studentService.ModifyAsync(id, dto));
   
    [HttpDelete("id")]
    public async Task<IActionResult> DeleteAsync(int id) =>
        Ok(await this.studentService.RemoveAsync(id));

    [HttpGet("Id")]
    public async Task<IActionResult> GetByIdAsync(int id) =>
        Ok(await this.studentService.RetrieveById(id));

    [HttpGet("all")]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params, string search) =>
       Ok(await this.studentService.RetrieveAllAsync(@params, search));

    [HttpGet("Number of birth certificate")]
    public async Task<IActionResult> GetByBirthCertificateNumberAsync(string certificateNumber) =>
        Ok(await this.studentService.RetrieveByBirthCertificateNumberAsync(certificateNumber));

    [HttpGet("Number of passport")]
    public async Task<IActionResult> GetByPassportNumberAsync(string passportNumber) =>
        Ok(await this.studentService.RetrieveByPassportNumberAsync(passportNumber));

    [HttpGet("school")]
    public async Task<IActionResult> GetBySchoolAsync([FromQuery] PaginationParams @params, string school) =>
        Ok(await this.studentService.RetrieveBySchoolAsync(@params, school));

    [HttpGet("region")]
    public async Task<IActionResult> GetByRegionAsync([FromQuery] PaginationParams @params, string region) =>
        Ok(await this.studentService.RetrieveByRegionAsync(@params, region));    
}