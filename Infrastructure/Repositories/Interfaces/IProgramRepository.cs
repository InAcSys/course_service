using CourseService.Domain.Entities.Concretes;

namespace CourseService.Infrastructure.Repositories.Interfaces
{
    public interface IProgramRepository : ISearchableRepository<AcademicProgram, int>
    {
        Task<bool> AssignSubjects(IEnumerable<ProgramSubject> subjects);
        Task<bool> AssignLevels(IEnumerable<ProgramLevel> levels);
        Task<bool> RevokeSubjects(IEnumerable<ProgramSubject> subjects);
        Task<bool> RevokeLevels(IEnumerable<ProgramLevel> levels);
    }
}
