using CourseService.Application.Validators.Interfaces;
using CourseService.Domain.Entities.Concretes;
using FluentValidation;

namespace CourseService.Application.Validators.Concretes.Courses
{
    public class CreateCourseValidator : AbstractValidator<Course>, ICreateValidator<Course>
    {
        public CreateCourseValidator() { }
    }
}
