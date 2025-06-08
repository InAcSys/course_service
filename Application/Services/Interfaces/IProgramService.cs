using CourseService.Domain.DTOs.ProgramLevels;
using CourseService.Domain.DTOs.ProgramSubjects;
using CourseService.Domain.Entities.Concretes;

namespace CourseService.Application.Services.Interfaces
{
    public interface IProgramService : ISearchableService<AcademicProgram, int>
    {
        Task<bool> AssignSubjects(ProgramSubjectsDTO subjects, Guid tenantId);
        Task<bool> AssignLevels(ProgramLevelsDTO levels, Guid tenantId);
        Task<bool> RevokeSubjects(ProgramSubjectsDTO subjects, Guid tenantId);
        Task<bool> RevokeLevels(ProgramLevelsDTO levels, Guid tenantId);
    }
}
