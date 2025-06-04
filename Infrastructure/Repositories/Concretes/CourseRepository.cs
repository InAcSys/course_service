using CourseService.Domain.Entities.Concretes;
using CourseService.Infrastructure.Repositories.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace CourseService.Infrastructure.Repositories.Concretes
{
    public class CourseRepository : Repository<Course, Guid>
    {
        public CourseRepository(DbContext dbContext)
            : base(dbContext) { }
    }
}
