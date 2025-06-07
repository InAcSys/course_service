using CourseService.Application.Services.Abstracts;
using CourseService.Application.Validators.Interfaces;
using CourseService.Domain.Entities.Concretes;
using CourseService.Infrastructure.Repositories.Interfaces;

namespace CourseService.Application.Services.Concretes
{
    public class AcademicLevelService(
        ICreateValidator<AcademicLevel> createValidator,
        IUpdateValidator<AcademicLevel> updateValidator,
        ISearchableRepository<AcademicLevel, int> repository
    ) : Service<AcademicLevel, int>(createValidator, updateValidator, repository) { }
}
