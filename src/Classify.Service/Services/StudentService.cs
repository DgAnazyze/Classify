using AutoMapper;
using Classify.DataAccess.Interfaces;
using Classify.Domain.Entities;
using Classify.Service.DTOs.Students;
using Classify.Service.Exceptions;
using Classify.Service.Interfaces;
using System.Linq.Expressions;

namespace Classify.Service.Services;

public class StudentService : IStudentService
{
    protected readonly IRepository<Student> studentRepository;
    protected readonly IMapper mapper;
    public StudentService(IRepository<Student> studentRepository, IMapper mapper)
    {
        this.studentRepository = studentRepository;
        this.mapper = mapper;
    }

    public async ValueTask<StudentResultDto> CreateAsync(StudentCreationDto studentCreationDto)
    {
        var alreadyExistStudent = await this.studentRepository.SelectAsync(u =>
        u.PassportNumber == studentCreationDto.PassportNumber ||
        u.BirthCertificateNumber == studentCreationDto.BirthCertificateNumber);

        if (alreadyExistStudent is not null)
            throw new CustomerException(400, "User with such username already exist");

        var student = await this.studentRepository
            .InserAsync(this.mapper.Map<Student>(studentCreationDto));

        await studentRepository.SavaAsync();

        return this.mapper.Map<StudentResultDto>(student);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var isDeleted = this.studentRepository.DeleteAsync(u => u.Id == id);
        if (!isDeleted)
            throw new CustomerException(404, "Student not found");

        return true;
    }

    public async ValueTask<StudentResultDto> GetAsync(Expression<Func<Student, bool>> predicate)
    {
        var student = await this.studentRepository.SelectAsync(predicate);
        if (student is null)
            throw new CustomerException(404, "Student not found");

        return this.mapper.Map<StudentResultDto>(student);
    }

    public ValueTask<StudentResultDto> UpdateAsync(int id, StudentUpdateDto studentUpdateDto)
    {
        throw new NotImplementedException();
    }
}
