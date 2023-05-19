using AutoMapper;
using Classify.DataAccess.Interfaces;
using Classify.Domain.Configurations;
using Classify.Domain.Entities;
using Classify.Service.DTOs.Students;
using Classify.Service.Commons.Exceptions;
using Classify.Service.Interfaces;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using Classify.Service.Commons.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

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

    /// <summary>
    /// To add new Student
    /// </summary>
    /// <param name="studentCreationDto"></param>
    /// <returns></returns>
    /// <exception cref="CustomerException"></exception>
    public async ValueTask<StudentForResultDto> AddAsync(StudentCreationDto studentCreationDto)
    {
        var student = await this.repository.SelectAsync(u =>
        u.BirthCertificateNumber == studentCreationDto.BirthCertificateNumber ||
        u.PassportNumber == studentCreationDto.PassportNumber);
        if (student is not null && student.IsDeleted == false)
            throw new CustomerException(403, "Student already exist");

        var mapperStudent = this.mapper.Map<Student>(studentCreationDto);
        var result = await this.repository.InserAsync(mapperStudent);
        await this.repository.SavaAsync();

        return this.mapper.Map<StudentForResultDto>(result);
    }

    /// <summary>
    /// To Update
    /// </summary>
    /// <param name="id"></param>
    /// <param name="studentUpdateDto"></param>
    /// <returns></returns>
    /// <exception cref="CustomerException"></exception>
    public async ValueTask<StudentForResultDto> ModifyAsync(int id, StudentUpdateDto studentUpdateDto)
    {
        var student =  await this.repository.SelectAsync(s => s.Id == id);
        if (student is null && student.IsDeleted == false)
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

    /// <summary>
    /// To delete Students
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="CustomerException"></exception>
    public async ValueTask<bool> RemoveAsync(int id)
    {
        var student = await this.repository.SelectAsync(u => u.Id == id);
        if (student is null || student.IsDeleted == true)
            throw new CustomerException(404, "Student not found");

        await this.repository.DeleteAsync(x => x.Id == id);
        await this.repository.SavaAsync();
        return true;
    }

    /// <summary>
    /// Return all Student with pagination 
    /// </summary>
    /// <param name="params"></param>
    /// <param name="search"></param>
    /// <returns></returns>
    /// <exception cref="CustomerException"></exception>
    public async ValueTask<IEnumerable<StudentForResultDto>> RetrieveAllAsync(PaginationParams @params, string search = null)
    {
        var students = await this.repository.SelectAll().
            Where(x => x.IsDeleted == false)
            .ToPagedList(@params)
            .ToListAsync();
        if (students is null)
            throw new CustomerException(404, "Students aren't found");

        return this.mapper.Map<IEnumerable<StudentForResultDto>>(students);
    }

    /// <summary>
    /// Select student by birth certificate number
    /// </summary>
    /// <param name="search"></param>
    /// <returns></returns>
    /// <exception cref="CustomerException"></exception>
    public async ValueTask<StudentForResultDto> RetrieveByBirthCertificateNumberAsync(string search = null)
    {
        var Student = await this.repository
            .SelectAsync(x => x.BirthCertificateNumber == search &&
                              x.IsDeleted == false);
        if (Student is null)
            throw new CustomerException(404, "Couldn't find student by given birth certificate number!");

        return this.mapper.Map<StudentForResultDto>(Student);
    }

    /// <summary>
    /// Serach student by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="CustomerException"></exception>
    public async ValueTask<StudentForResultDto> RetrieveById(int id)
    {
        var student = await this.repository.SelectAsync(x => x.Id == id && x.IsDeleted == false);
        if (student is null)
            throw new CustomerException(404, "Student Not Found");

        return this.mapper.Map<StudentForResultDto>(student);
    }
   
    /// <summary>
    /// Serach student with passport number
    /// </summary>
    /// <param name="search"></param>
    /// <returns></returns>
    /// <exception cref="CustomerException"></exception>
    public async ValueTask<StudentForResultDto> RetrieveByPassportNumberAsync(string search = null)
    {
        var student = await this.repository.SelectAsync(x => x.PassportNumber == search && x.IsDeleted == false);
        if (student is null)
            throw new CustomerException(404, "Student with this passport number couldn't find!");

        return this.mapper.Map<StudentForResultDto>(student);
    }

    /// <summary>
    /// Кeturn all students from the given region
    /// </summary>
    /// <param name="params"></param>
    /// <param name="search"></param>
    /// <returns></returns>
    public async ValueTask<IEnumerable<StudentForResultDto>> RetrieveByRegionAsync(PaginationParams @params, string search = null)
    {
        var students = await this.repository.SelectAll()
            .Where(x => x.Region == search && x.IsDeleted == false)
            .ToPagedList(@params)
            .ToListAsync();
    
        return this.mapper.Map<IEnumerable<StudentForResultDto>>(students);
    }

    /// <summary>
    /// return all students from the given school
    /// </summary>
    /// <param name="params"></param>
    /// <param name="search"></param>
    /// <returns></returns>
    public async ValueTask<IEnumerable<StudentForResultDto>> RetrieveBySchoolAsync(PaginationParams @params, string search = null)
    {
        var students = await this.repository.SelectAll()
            .Where(x => x.School == search && x.IsDeleted == false)
            .ToPagedList(@params)
            .ToListAsync();

        return this.mapper.Map<IEnumerable<StudentForResultDto>>(students);
    }
}
