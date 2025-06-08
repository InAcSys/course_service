using CourseService.Domain.Entities.Concretes;

namespace CourseService.Application.Services.Interfaces
{
    public interface ILevelService : ISearchableService<AcademicLevel, int>
    {
        IEnumerable<Subject> GetSubjects(int levelId, Guid tenantId);
        IEnumerable<Course> GetCourses(int levelId, Guid tenantId);
    }
}
