using CourseService.Domain.Entities.Concretes;

namespace CourseService.Infrastructure.Repositories.Interfaces
{
    public interface ISubjectRepository : ISearchableRepository<Subject, Guid>
    {
        Task<bool> AssignRequisites(IEnumerable<SubjectRequisite> requisites);
        Task<bool> RevokeRequisites(IEnumerable<SubjectRequisite> requisites);
        Task<bool> RevokePrograms(IEnumerable<SubjectProgram> programs);
        Task<bool> AssignPrograms(IEnumerable<SubjectProgram> programs);
    }
}
