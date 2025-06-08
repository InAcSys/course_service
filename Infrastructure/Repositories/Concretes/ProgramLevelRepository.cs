using CourseService.Domain.Entities.Concretes;
using CourseService.Infrastructure.Repositories.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace CourseService.Infrastructure.Repositories.Concretes
{
    public class ProgramLevelRepository(DbContext context)
        : AbstractRepository<ProgramLevel, int>(context) { }
}
