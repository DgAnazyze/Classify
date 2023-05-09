using AutoMapper;
using Classify.Domain.Entities;
using Classify.Service.DTOs.Students;

namespace Classify.Service.Mappers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Student, StudentCreationDto>().ReverseMap();
        CreateMap<Student, StudentForResultDto>().ReverseMap();
        CreateMap<Student, StudentUpdateDto>().ReverseMap();
    }
}
