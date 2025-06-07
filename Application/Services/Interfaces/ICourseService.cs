using CourseService.Domain.DTOs.CourseSubjects;
using CourseService.Domain.Entities.Concretes;

namespace CourseService.Application.Services.Interfaces
{
    public interface ICourseService : ISearchableService<Course, Guid>
    {
        Task<bool> AssignSubjects(CourseSubjectsDTO subjects, Guid tenantId);
        Task<bool> RevokeSubjects(CourseSubjectsDTO subjects, Guid tenantId);
    }
}
