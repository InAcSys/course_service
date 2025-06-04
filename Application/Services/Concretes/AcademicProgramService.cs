using CourseService.Application.Services.Abstracts;
using CourseService.Domain.Entities.Concretes;
using CourseService.Infrastructure.Repositories.Interfaces;
using FluentValidation;

namespace CourseService.Application.Services.Concretes
{
    public class AcademicProgramService : Service<AcademicProgram, int>
    {
        public AcademicProgramService(
            IValidator<AcademicProgram> validator,
            IRepository<AcademicProgram, int> repository
        )
            : base(validator, repository) { }
    }
}
