using CourseService.Application.Validators.Interfaces;
using CourseService.Domain.Entities.Concretes;
using FluentValidation;

namespace CourseService.Application.Validators.Concretes.AcademicPrograms
{
    public class UpdateAcademicProgramValidator
        : AbstractValidator<AcademicProgram>,
            IUpdateValidator<AcademicProgram>
    {
        public UpdateAcademicProgramValidator() { }
    }
}
