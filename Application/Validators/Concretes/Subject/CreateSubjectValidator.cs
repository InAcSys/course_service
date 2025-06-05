using CourseService.Application.Validators.Interfaces;
using CourseService.Domain.Entities.Concretes;
using FluentValidation;

namespace CourseService.Application.Validators.Concretes.Subjects
{
    public class CreateSubjectValidator : AbstractValidator<Subject>, ICreateValidator<Subject>
    {
        public CreateSubjectValidator() { }
    }
}
