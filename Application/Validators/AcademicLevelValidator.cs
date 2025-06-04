using CourseService.Domain.Entities.Concretes;
using FluentValidation;

namespace CourseService.Application.Validators
{
    public class AcademicLevelValidator : AbstractValidator<AcademicLevel>
    {
        public AcademicLevelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Academic level name cannot be empty.")
                .Length(2, 100)
                .WithMessage("Academic level name must be between 2 and 100 characters.");
        }
    }
}
