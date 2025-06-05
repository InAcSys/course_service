using AutoMapper;
using CourseService.Domain.DTOs.Subjects;
using CourseService.Domain.Entities.Concretes;

namespace CourseService.Presentation.Profiles
{
    public class SubjectProfile : Profile
    {
        public SubjectProfile()
        {
            CreateMap<Subject, CreateSubjectDTO>().ReverseMap();
            CreateMap<Subject, UpdateSubjectDTO>().ReverseMap();
        }
    }
}
