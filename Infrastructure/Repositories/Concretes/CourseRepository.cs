using CourseService.Domain.Entities.Concretes;
using CourseService.Infrastructure.Repositories.Abstracts;
using CourseService.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseService.Infrastructure.Repositories.Concretes
{
    public class CourseRepository(DbContext context, IRepository<CourseSubject, int> repository)
        : AbstractCourseRepository(context, repository) { }
}
