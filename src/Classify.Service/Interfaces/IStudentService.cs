using Classify.Domain.Configurations;
using Classify.Domain.Entities;
using Classify.Service.DTOs.Students;
using Classify.Service.DTOs.Users;
using System.Linq.Expressions;

namespace Classify.Service.Interfaces;

public interface IStudentService
{
    public ValueTask<StudentForResultDto> AddAsync(StudentCreationDto studentCreationDto);
    public ValueTask<IEnumerable<StudentForResultDto>> RetrieveAllAsync(
        PaginationParams @params, string search = null);
    public ValueTask<IEnumerable<StudentForResultDto>> RetrieveByRegionAsync(
       PaginationParams @params, string search = null);
    public ValueTask<IEnumerable<StudentForResultDto>> RetrieveBySchoolAsync(
        PaginationParams @params, string search = null);
    public ValueTask<StudentForResultDto> RetrieveByPassportNumberAsync(string search = null);
    public ValueTask<StudentForResultDto> RetrieveByBirthCertificateNumberAsync(string search = null);
    public ValueTask<StudentForResultDto> RetrieveById(int id);
    public ValueTask<bool> RemoveAsync(int id);
    public ValueTask<StudentForResultDto> ModifyAsync(int id, StudentUpdateDto dto);
}
