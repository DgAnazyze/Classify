using AutoMapper;
using Classify.DataAccess.Interfaces;
using Classify.Domain.Configurations;
using Classify.Domain.Entities;
using Classify.Service.DTOs.Students;
using Classify.Service.Commons.Exceptions;
using Classify.Service.Interfaces;
using System.Linq.Expressions;

namespace Classify.Service.Services;

public class StudentService : IStudentService
{
    protected readonly IRepository<Student> repository;
    protected readonly IMapper mapper;

    public StudentService(IRepository<Student> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async ValueTask<StudentForResultDto> AddAsync(StudentCreationDto studentCreationDto)
    {
        var alreadyExistStudent = await this.repository.SelectAsync(u =>
        u.BirthCertificateNumber == studentCreationDto.BirthCertificateNumber ||
        u.PassportNumber == studentCreationDto.PassportNumber);
        if (alreadyExistStudent is not null)
            throw new CustomerException(403, "Student already exist");

        var mapperStudent = this.mapper.Map<Student>(studentCreationDto);
        var result = await this.repository.InserAsync(mapperStudent);
        await this.repository.SavaAsync();

        return this.mapper.Map<StudentForResultDto>(result);
    }

    public async ValueTask<StudentForResultDto> ModifyAsync(int id, StudentUpdateDto studentUpdateDto)
    {
        var student =  await this.repository.SelectAsync(s => s.Id == id);
        if (student is null)
            throw new CustomerException(404, "Couldn't find student for given id");

        if (studentUpdateDto is not null)
        {
            student.FirstName = String.IsNullOrEmpty(studentUpdateDto.FirstName) ? student.FirstName : studentUpdateDto.FirstName;
            student.LastName = String.IsNullOrEmpty(studentUpdateDto.LastName) ? student.LastName : studentUpdateDto.LastName;
            student.LastName = String.IsNullOrEmpty(studentUpdateDto.LastName) ? student.LastName : studentUpdateDto.LastName;
            student.LastName = String.IsNullOrEmpty(studentUpdateDto.LastName) ? student.LastName : studentUpdateDto.LastName;
            student.LastName = String.IsNullOrEmpty(studentUpdateDto.LastName) ? student.LastName : studentUpdateDto.LastName;
            student.LastName = String.IsNullOrEmpty(studentUpdateDto.LastName) ? student.LastName : studentUpdateDto.LastName;


        }
    }

    public async ValueTask<bool> RemoveAsync(int id)
    {
        var alreadyExistStudent = await this.repository.SelectAsync(u => u.Id == id);
        if (alreadyExistStudent is null)
            throw new CustomerException(404, "Student not found");

        await this.repository.DeleteAsync(x => x.Id == id);
        await this.repository.SavaAsync();
        return true;
    }

    public ValueTask<IEnumerable<StudentForResultDto>> RetrieveAllAsync(PaginationParams @params, string search = null)
    {
        throw new NotImplementedException();
    }

    public ValueTask<StudentForResultDto> RetrieveByBirthCertificateNumberAsync(Expression<Func<Student, bool>> expression = null)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<StudentForResultDto> RetrieveById(int id)
    {
        var alreadyExistStudent = await this.repository.SelectAsync(u => u.Id == id);
        if (alreadyExistStudent is null || alreadyExistStudent.IsDeleted)
            throw new CustomerException(404, "Student Not Found");

        return this.mapper.Map<StudentForResultDto>(alreadyExistStudent);
    }

    public ValueTask<StudentForResultDto> RetrieveByPassportNumberAsync(Expression<Func<Student, bool>> expression = null)
    {
        throw new NotImplementedException();
    }

    public ValueTask<IEnumerable<StudentForResultDto>> RetrieveByRegionAsync(PaginationParams @params, string search = null)
    {
        throw new NotImplementedException();
    }

    public ValueTask<IEnumerable<StudentForResultDto>> RetrieveBySchoolAsync(PaginationParams @params, string search = null)
    {
        throw new NotImplementedException();
    }
}
