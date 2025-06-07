using CourseService.Application.Services.Abstracts;
using CourseService.Application.Validators.Interfaces;
using CourseService.Domain.Entities.Concretes;
using CourseService.Infrastructure.Repositories.Interfaces;

namespace CourseService.Application.Services.Concretes
{
    public class CourseService(
        ICreateValidator<Course> createValidator,
        IUpdateValidator<Course> updateValidator,
        ISearchableRepository<Course, Guid> repository
    ) : Service<Course, Guid>(createValidator, updateValidator, repository) { }
}
