using AutoMapper;
using CourseService.Domain.DTOs.AcademicPrograms;
using CourseService.Domain.Entities.Concretes;

namespace CourseService.Presentation.Profiles
{
    public class AcademicProgramProfile : Profile
    {
        public AcademicProgramProfile()
        {
            CreateMap<AcademicProgram, CreateAcademicProgramDTO>().ReverseMap();
            CreateMap<AcademicProgram, UpdateAcademicProgramDTO>().ReverseMap();
        }
    }
}
