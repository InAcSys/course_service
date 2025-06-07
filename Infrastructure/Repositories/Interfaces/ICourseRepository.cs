using CourseService.Domain.Entities.Concretes;

namespace CourseService.Infrastructure.Repositories.Interfaces
{
    public interface ICourseRepository : ISearchableRepository<Course, Guid>
    {
        Task<bool> AssignSubjects(IEnumerable<CourseSubject> subjects);
        Task<bool> RevokeSubjects(IEnumerable<CourseSubject> subjects);
    }
}
