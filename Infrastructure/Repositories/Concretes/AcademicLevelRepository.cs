using CourseService.Infrastructure.Repositories.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace CourseService.Infrastructure.Repositories.Concretes
{
    public class AcademicLevelRepository(DbContext context) : AbstractLevelRepository(context) { }
}
