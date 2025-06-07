using CourseService.Domain.Entities.Concretes;
using CourseService.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseService.Infrastructure.Repositories.Abstracts
{
    public class AbstractSubjectRepository(
        DbContext context,
        IRepository<SubjectRequisite, int> subjectRequisiteRepository,
        IRepository<SubjectProgram, int> subjectProgramRepository
    ) : Repository<Subject, Guid>(context), ISubjectRepository
    {
        private readonly IRepository<SubjectRequisite, int> _subjectRequisiteRepository =
            subjectRequisiteRepository;
        private readonly IRepository<SubjectProgram, int> _subjectProgramRepository =
            subjectProgramRepository;

        public async Task<bool> AssignPrograms(IEnumerable<SubjectProgram> programs)
        {
            try
            {
                var tasks = programs.Select(async program =>
                {
                    var existing = await _context
                        .Set<SubjectProgram>()
                        .FirstOrDefaultAsync(r =>
                            r.SubjectId == program.SubjectId
                            && r.AcademicProgramId == program.AcademicProgramId
                            && r.TenantId == program.TenantId
                        );

                    if (existing == null)
                    {
                        await _subjectProgramRepository.Create(program);
                    }
                    else if (!existing.IsActive)
                    {
                        existing.IsActive = true;
                        existing.Deleted = null;
                        await _subjectProgramRepository.Update(
                            existing.Id,
                            existing,
                            existing.TenantId
                        );
                    }
                });

                await Task.WhenAll(tasks);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AssignRequisites(IEnumerable<SubjectRequisite> requisites)
        {
            try
            {
                var tasks = requisites.Select(async requisite =>
                {
                    var existing = await _context
                        .Set<SubjectRequisite>()
                        .FirstOrDefaultAsync(r =>
                            r.SubjectId == requisite.SubjectId
                            && r.RequisteId == requisite.RequisteId
                            && r.TenantId == requisite.TenantId
                        );

                    if (existing == null)
                    {
                        await _subjectRequisiteRepository.Create(requisite);
                    }
                    else if (!existing.IsActive)
                    {
                        existing.IsActive = true;
                        existing.Deleted = null;
                        await _subjectRequisiteRepository.Update(
                            existing.Id,
                            existing,
                            existing.TenantId
                        );
                    }
                });

                await Task.WhenAll(tasks);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RevokePrograms(IEnumerable<SubjectProgram> programs)
        {
            try
            {
                var tasks = programs.Select(program =>
                    _context
                        .Set<SubjectProgram>()
                        .Where(x =>
                            x.SubjectId == program.SubjectId
                            && x.AcademicProgramId == program.AcademicProgramId
                            && x.TenantId == program.TenantId
                        )
                        .ExecuteUpdateAsync(setters =>
                            setters
                                .SetProperty(x => x.IsActive, false)
                                .SetProperty(x => x.Deleted, DateTime.UtcNow)
                        )
                );

                await Task.WhenAll(tasks);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RevokeRequisites(IEnumerable<SubjectRequisite> requisites)
        {
            try
            {
                var tasks = requisites.Select(requisite =>
                    _context
                        .Set<SubjectRequisite>()
                        .Where(x =>
                            x.SubjectId == requisite.SubjectId
                            && x.RequisteId == requisite.RequisteId
                            && x.TenantId == requisite.TenantId
                        )
                        .ExecuteUpdateAsync(setters =>
                            setters
                                .SetProperty(x => x.IsActive, false)
                                .SetProperty(x => x.Deleted, DateTime.UtcNow)
                        )
                );
                await Task.WhenAll(tasks);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
