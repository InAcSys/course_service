using CourseService.Application.Validators.Interfaces;
using CourseService.Domain.Entities.Concretes;
using FluentValidation;

namespace CourseService.Application.Validators.Concretes
{
    public class UpdateSubjectValidator : AbstractValidator<Subject>, IUpdateSubjectValidator
    {
        public UpdateSubjectValidator() { }
    }
}
