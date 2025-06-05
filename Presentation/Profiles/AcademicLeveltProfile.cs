using AutoMapper;
using CourseService.Domain.DTOs.AcademicLevels;
using CourseService.Domain.Entities.Concretes;

namespace CourseService.Presentation.Profiles
{
    public class AcademicLevelProfile : Profile
    {
        public AcademicLevelProfile()
        {
            CreateMap<AcademicLevel, CreateAcademicLevelDTO>().ReverseMap();
            CreateMap<AcademicLevel, UpdateAcademicLevelDTO>().ReverseMap();
        }
    }
}
