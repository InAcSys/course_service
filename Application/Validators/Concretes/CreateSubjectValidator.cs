using CourseService.Application.Validators.Interfaces;
using CourseService.Domain.Entities.Concretes;
using FluentValidation;

namespace CourseService.Application.Validators.Concretes
{
    public class CreateSubjectValidator : AbstractValidator<Subject>, ICreateSubjectValidator
    {
        public CreateSubjectValidator() { }
    }
}
