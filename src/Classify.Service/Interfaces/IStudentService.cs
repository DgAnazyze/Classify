using Classify.Domain.Entities;
using Classify.Service.DTOs.Students;
using System.Linq.Expressions;

namespace Classify.Service.Interfaces;

public interface IStudentService
{
    ValueTask<StudentResultDto> CreateAsync(StudentCreationDto studentCreationDto);
    ValueTask<StudentResultDto> UpdateAsync(int id, StudentUpdateDto studentUpdateDto);
    Task<bool> DeleteAsync(int id);
    //ValueTask<IEnumerable<StudentResultDto>> GetAllAsync()
    ValueTask<StudentResultDto> GetAsync(Expression<Func<Student, bool>> predicate);
}
