using AutoMapper;
using Classify.Domain.Entities;
using Classify.Service.DTOs.Administrator;
using Classify.Service.DTOs.Students;
using Classify.Service.DTOs.Users;

namespace Classify.Service.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Admin, AdminChangePasswordDto>().ReverseMap();
        CreateMap<Admin, AdminCreationDto>().ReverseMap();
        CreateMap<Admin, AdminResultDto>().ReverseMap();
        CreateMap<Admin, AdminUpdateDto>().ReverseMap();

        CreateMap<Student, StudentCreationDto>().ReverseMap();
        CreateMap<Student, StudentResultDto>().ReverseMap();
        CreateMap<Student, StudentUpdateDto>().ReverseMap();

        CreateMap<User, UserChangePasswordDto>().ReverseMap();
        CreateMap<User, UserCreationDto>().ReverseMap();
        CreateMap<User, UserResultDto>().ReverseMap();
        CreateMap<User, UserUpdateDto>().ReverseMap();
    }
}
