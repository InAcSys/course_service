using CourseService.Application.Services.Abstracts;
using CourseService.Application.Validators.Interfaces;
using CourseService.Domain.Entities.Concretes;
using CourseService.Infrastructure.Repositories.Interfaces;

namespace CourseService.Application.Services.Concretes
{
    public class CourseService(
        ICreateValidator<Course> createValidator,
        IUpdateValidator<Course> updateValidator,
        ICourseRepository repository
    ) : AbstractCourseService(createValidator, updateValidator, repository) { }
}
