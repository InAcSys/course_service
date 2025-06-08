using CourseService.Domain.Entities.Concretes;

namespace CourseService.Infrastructure.Repositories.Interfaces
{
    public interface ILevelRepository : ISearchableRepository<AcademicLevel, int> { }
}
