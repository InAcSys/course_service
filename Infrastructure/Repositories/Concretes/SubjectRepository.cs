using CourseService.Domain.Entities.Concretes;
using CourseService.Infrastructure.Repositories.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace CourseService.Infrastructure.Repositories.Concretes
{
    public class SubjectRepository : Repository<Subject, int>
    {
        public SubjectRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}