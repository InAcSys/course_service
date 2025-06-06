using CourseService.Application.Validators.Interfaces;
using CourseService.Domain.Entities.Concretes;
using FluentValidation;

namespace CourseService.Application.Validators.Concretes.Courses
{
    public class UpdateCourseValidator : AbstractValidator<Course>, IUpdateValidator<Course>
    {
        public UpdateCourseValidator() { }
    }
}
