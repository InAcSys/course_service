using CourseService.Domain.Entities.Concretes;
using FluentValidation;

namespace CourseService.Application.Validators
{
    public class AcademicProgramValidator : AbstractValidator<AcademicProgram>
    {
        public AcademicProgramValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Academic program name cannot be empty.")
                .Length(2, 100)
                .WithMessage("Academic program name must be between 2 and 100 characters.");
        }
    }
}