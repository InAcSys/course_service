using CourseService.Domain.Entities.Concretes;
using FluentValidation;

namespace CourseService.Application.Validators
{
    public class CourseValidator : AbstractValidator<Course>
    {
        public CourseValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Course name is required.")
                .Length(2, 100)
                .WithMessage("Course name must be between 2 and 100 characters.");
            RuleFor(x => x.ShortName)
                .NotEmpty()
                .WithMessage("Course short name is required.")
                .Length(2, 50)
                .WithMessage("Course short name must be between 2 and 50 characters.");
            RuleFor(x => x.TeacherId)
                .NotEmpty()
                .WithMessage("Teacher ID is required.")
                .Must(teacherId => Guid.TryParse(teacherId.ToString(), out _))
                .WithMessage("Teacher ID must be a valid GUID.");
        }
    }
}