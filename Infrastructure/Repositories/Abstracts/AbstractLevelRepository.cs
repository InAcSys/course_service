using CourseService.Domain.Entities.Concretes;
using CourseService.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseService.Infrastructure.Repositories.Abstracts
{
    public abstract class AbstractLevelRepository(DbContext context)
        : Repository<AcademicLevel, int>(context),
            ILevelRepository { }
}
