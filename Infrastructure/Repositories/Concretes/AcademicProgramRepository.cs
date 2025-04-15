using CourseService.Domain.Entities.Concretes;
using CourseService.Infrastructure.Repositories.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace CourseService.Infrastructure.Repositories.Concretes
{
    public class AcademicProgramRepository : Repository<AcademicProgram, int>
    {
        public AcademicProgramRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}