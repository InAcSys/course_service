using CourseService.Application.Services.Abstracts;
using CourseService.Application.Validators.Interfaces;
using CourseService.Domain.Entities.Concretes;
using CourseService.Infrastructure.Repositories.Interfaces;

namespace CourseService.Application.Services.Concretes
{
    public class SubjectService(
        ICreateValidator<Subject> createValidator,
        IUpdateValidator<Subject> updateValidator,
        IRepository<Subject, Guid> repository
    ) : Service<Subject, Guid>(createValidator, updateValidator, repository) { }
}
