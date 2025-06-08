using CourseService.Domain.Entities.Concretes;
using CourseService.Infrastructure.Repositories.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace CourseService.Infrastructure.Repositories.Concretes
{
    public class ProgramSubjectRepository(DbContext context)
        : AbstractRepository<ProgramSubject, int>(context) { }
}
