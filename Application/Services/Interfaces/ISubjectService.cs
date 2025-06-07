using CourseService.Domain.DTOs.SubjectRequisites;
using CourseService.Domain.DTOs.Subjects;
using CourseService.Domain.Entities.Concretes;

namespace CourseService.Application.Services.Interfaces
{
    public interface ISubjectService : ISearchableService<Subject, Guid>
    {
        Task<bool> AssignRequisites(SubjectRequisitesDTO requisites, Guid tenantId);
        Task<bool> RevokeRequisites(SubjectRequisitesDTO requisites, Guid tenantId);
        Task<bool> AssignPrograms(SubjectProgramsDTO programs, Guid tenantId);
        Task<bool> RevokePrograms(SubjectProgramsDTO programs, Guid tenantId);
    }
}
