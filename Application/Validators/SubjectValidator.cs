using CourseService.Domain.Entities.Concretes;
using FluentValidation;

namespace CourseService.Application.Validators
{
    public class SubjectValidator : AbstractValidator<Subject>
    {
        public SubjectValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Subject name cannot be empty.")
                .Length(2, 100)
                .WithMessage("Subject name must be between 2 and 100 characters.");
        }
    }
}
