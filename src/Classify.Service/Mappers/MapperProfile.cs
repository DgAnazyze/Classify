using AutoMapper;
using Classify.Domain.Entities;
using Classify.Service.DTOs.Students;
using Classify.Service.DTOs.Users;

namespace Classify.Service.Mappers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Student, StudentCreationDto>().ReverseMap();
        CreateMap<Student, StudentResultDto>().ReverseMap();
        CreateMap<Student, StudentUpdateDto>().ReverseMap();

        CreateMap<User, UserChangePasswordDto>().ReverseMap();
        CreateMap<User, UserCreationDto>().ReverseMap();
        CreateMap<User, UserResultDto>().ReverseMap();
        CreateMap<User, UserUpdateDto>().ReverseMap();
    }
}
