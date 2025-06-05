using CourseService.Application.Validators.Interfaces;
using CourseService.Domain.Entities.Concretes;
using FluentValidation;

namespace CourseService.Application.Validators.Concretes.AcademicLevels
{
    public class CreateAcademicLevelValidator
        : AbstractValidator<AcademicLevel>,
            ICreateValidator<AcademicLevel>
    {
        public CreateAcademicLevelValidator() { }
    }
}
