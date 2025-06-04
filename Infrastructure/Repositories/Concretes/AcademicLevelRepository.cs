using CourseService.Domain.Entities.Concretes;
using CourseService.Infrastructure.Repositories.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace CourseService.Infrastructure.Repositories.Concretes
{
    public class AcademicLevelRepository : Repository<AcademicLevel, int>
    {
        public AcademicLevelRepository(DbContext dbContext)
            : base(dbContext) { }
    }
}
