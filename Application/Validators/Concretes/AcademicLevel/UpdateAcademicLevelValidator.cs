using CourseService.Application.Validators.Interfaces;
using CourseService.Domain.Entities.Concretes;
using FluentValidation;

namespace CourseService.Application.Validators.Concretes.AcademicLevels
{
    public class UpdateAcademicLevelValidator
        : AbstractValidator<AcademicLevel>,
            IUpdateValidator<AcademicLevel>
    {
        public UpdateAcademicLevelValidator() { }
    }
}
