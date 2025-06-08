using CourseService.Domain.Entities.Concretes;
using CourseService.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseService.Infrastructure.Repositories.Abstracts
{
    public abstract class AbstractProgramRepository(
        IRepository<ProgramLevel, int> programLevelRepository,
        IRepository<ProgramSubject, int> programSubjectRepository,
        DbContext context
    ) : Repository<AcademicProgram, int>(context), IProgramRepository
    {
        protected readonly IRepository<ProgramLevel, int> _programLevelRepository =
            programLevelRepository;
        protected readonly IRepository<ProgramSubject, int> _programSubjectRepository =
            programSubjectRepository;

        public async Task<bool> AssignLevels(IEnumerable<ProgramLevel> levels)
        {
            try
            {
                foreach (var level in levels)
                {
                    var existing = await _context
                        .Set<ProgramLevel>()
                        .FirstOrDefaultAsync(r =>
                            r.LevelId == level.LevelId
                            && r.ProgramId == level.ProgramId
                            && r.TenantId == level.TenantId
                        );

                    if (existing == null)
                    {
                        await _programLevelRepository.Create(level);
                    }
                    else if (!existing.IsActive)
                    {
                        existing.IsActive = true;
                        existing.Deleted = null;
                        await _programLevelRepository.Update(
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

        public async Task<bool> AssignSubjects(IEnumerable<ProgramSubject> subjects)
        {
            try
            {
                foreach (var subject in subjects)
                {
                    var existing = await _context
                        .Set<ProgramSubject>()
                        .FirstOrDefaultAsync(r =>
                            r.SubjectId == subject.SubjectId
                            && r.ProgramId == subject.ProgramId
                            && r.TenantId == subject.TenantId
                        );

                    if (existing == null)
                    {
                        await _programSubjectRepository.Create(subject);
                    }
                    else if (!existing.IsActive)
                    {
                        existing.IsActive = true;
                        existing.Deleted = null;
                        await _programSubjectRepository.Update(
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

        public async Task<bool> RevokeLevels(IEnumerable<ProgramLevel> levels)
        {
            try
            {
                foreach (var level in levels)
                {
                    await _context
                        .Set<ProgramLevel>()
                        .Where(x =>
                            x.LevelId == level.LevelId
                            && x.ProgramId == level.ProgramId
                            && x.TenantId == level.TenantId
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

        public async Task<bool> RevokeSubjects(IEnumerable<ProgramSubject> subjects)
        {
            try
            {
                foreach (var subject in subjects)
                {
                    await _context
                        .Set<ProgramSubject>()
                        .Where(x =>
                            x.SubjectId == subject.SubjectId
                            && x.ProgramId == subject.ProgramId
                            && x.TenantId == subject.TenantId
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
