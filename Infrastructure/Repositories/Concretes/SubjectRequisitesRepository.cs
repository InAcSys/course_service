using CourseService.Domain.Entities.Concretes;
using CourseService.Infrastructure.Repositories.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace CourseService.Infrastructure.Repositories.Concretes
{
    public class SubjectRequisitesRepository(DbContext context)
        : AbstractRepository<SubjectRequisite, int>(context) { }
}
