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

        
        student.Grade = (studentUpdateDto.Grade == 0 || studentUpdateDto.Grade <= 12) ? student.Grade : studentUpdateDto.Grade;
        student.FirstName = String.IsNullOrEmpty(studentUpdateDto.FirstName) ? student.FirstName : studentUpdateDto.FirstName;
        student.LastName = String.IsNullOrEmpty(studentUpdateDto.LastName) ? student.LastName : studentUpdateDto.LastName;
        student.Surname = String.IsNullOrEmpty(studentUpdateDto.Surname) ? student.Surname : studentUpdateDto.Surname;
        student.BirthCertificateSeria = String.IsNullOrEmpty(studentUpdateDto.BirthCertificateSeria) ? student.BirthCertificateSeria : studentUpdateDto.BirthCertificateSeria;
        student.BirthCertificateNumber = String.IsNullOrEmpty(studentUpdateDto.BirthCertificateNumber) ? student.BirthCertificateNumber : studentUpdateDto.BirthCertificateNumber;
        student.PassportNumber = String.IsNullOrEmpty(studentUpdateDto.PassportNumber) ? student.PassportNumber : studentUpdateDto.PassportNumber;
        student.PassportSeria = String.IsNullOrEmpty(studentUpdateDto.PassportSeria) ? student.PassportSeria : studentUpdateDto.PassportSeria;
        student.Gender = studentUpdateDto.Gender;
        student.Region = String.IsNullOrEmpty(studentUpdateDto.Region) ? student.Region : studentUpdateDto.Region;
        student.School = String.IsNullOrEmpty(studentUpdateDto.School) ? student.School : studentUpdateDto.School;
        student.Bearings = String.IsNullOrEmpty(studentUpdateDto.Bearings) ? student.Bearings : studentUpdateDto.Bearings;
        student.Language = String.IsNullOrEmpty(studentUpdateDto.Language) ? student.Language : studentUpdateDto.Language;

        student.UpdatedAt = DateTime.UtcNow;
        await this.repository.SavaAsync();

        return this.mapper.Map<StudentForResultDto>(student);
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
