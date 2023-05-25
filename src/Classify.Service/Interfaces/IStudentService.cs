using Classify.Domain.Configurations;
using Classify.Domain.Entities;
using Classify.Service.DTOs.Students;
using Classify.Service.DTOs.Users;
using System.Linq.Expressions;

namespace Classify.Service.Interfaces;

public interface IStudentService
{
    public ValueTask<StudentResultDto> AddAsync(StudentCreationDto studentCreationDto);
    public ValueTask<IEnumerable<StudentResultDto>> RetrieveAllAsync(
        PaginationParams @params, string search = null);
    public ValueTask<IEnumerable<StudentResultDto>> RetrieveByRegionAsync(
       PaginationParams @params, string search = null);
    public ValueTask<IEnumerable<StudentResultDto>> RetrieveBySchoolAsync(
        PaginationParams @params, string search = null);
    public ValueTask<StudentResultDto> RetrieveByPassportNumberAsync(string search = null);
    public ValueTask<StudentResultDto> RetrieveByBirthCertificateNumberAsync(string search = null);
    public ValueTask<StudentResultDto> RetrieveById(int id);
    public ValueTask<bool> RemoveAsync(int id);
    public ValueTask<StudentResultDto> ModifyAsync(int id, StudentUpdateDto dto);
}
