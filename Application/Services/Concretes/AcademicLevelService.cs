using CourseService.Application.Services.Abstracts;
using CourseService.Domain.Entities.Concretes;
using CourseService.Infrastructure.Repositories.Interfaces;
using FluentValidation;

namespace CourseService.Application.Services.Concretes
{
    public class AcademicLevelService : Service<AcademicLevel, int>
    {
        public AcademicLevelService(
            IValidator<AcademicLevel> validator,
            IRepository<AcademicLevel, int> repository
        )
            : base(validator, repository) { }
    }
}
