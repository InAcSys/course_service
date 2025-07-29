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
                foreach (var program in programs)
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
                }

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
                foreach (var requisite in requisites)
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
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<Subject>> GetMySubjects(
            Guid teacherId,
            Guid tenantId,
            int pageNumber,
            int pageSize
        )
        {
            if (pageNumber < 1)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(pageNumber),
                    "Page number must be greater than or equal to 1."
                );
            }

            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(pageSize),
                    "Page size must be greater than or equal to 1."
                );
            }

            var skip = (pageNumber - 1) * pageSize;

            var query = _context.Set<Subject>().Where(x => x.IsActive);

            if (tenantId != Guid.Empty)
            {
                query = query.Where(x => x.TenantId == tenantId || x.TenantId == Guid.Empty);
            }

            if (teacherId != Guid.Empty)
            {
                query = query.Where(x => x.TeacherId == teacherId || x.TeacherId == Guid.Empty);
            }

            var subjects = await query.Skip(skip).Take(pageSize).ToListAsync();

            return subjects;
        }

        public async Task<bool> RevokePrograms(IEnumerable<SubjectProgram> programs)
        {
            try
            {
                foreach (var program in programs)
                {
                    await _context
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
                        );
                }

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
                foreach (var requisite in requisites)
                {
                    await _context
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
                        );
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
