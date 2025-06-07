using AutoMapper;
using CourseService.Domain.DTOs.Courses;
using CourseService.Domain.Entities.Concretes;

namespace CourseService.Presentation.Profiles
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<Course, CreateCourseDTO>().ReverseMap();
            CreateMap<Course, UpdateCourseDTO>().ReverseMap();
        }
    }
}
