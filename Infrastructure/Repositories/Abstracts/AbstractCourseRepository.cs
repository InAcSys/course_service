using CourseService.Domain.Entities.Concretes;
using CourseService.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseService.Infrastructure.Repositories.Abstracts
{
    public abstract class AbstractCourseRepository(
        DbContext context,
        IRepository<CourseSubject, int> courseSubjectRepository
    ) : Repository<Course, Guid>(context), ICourseRepository
    {
        private readonly IRepository<CourseSubject, int> _courseSubjectRepository =
            courseSubjectRepository;

        public async Task<bool> AssignSubjects(IEnumerable<CourseSubject> subjects)
        {
            try
            {
                foreach (var subject in subjects)
                {
                    var existing = await _context
                        .Set<CourseSubject>()
                        .FirstOrDefaultAsync(r =>
                            r.SubjectId == subject.SubjectId
                            && r.CourseId == subject.CourseId
                            && r.TenantId == subject.TenantId
                        );

                    if (existing == null)
                    {
                        await _courseSubjectRepository.Create(subject);
                    }
                    else if (!existing.IsActive)
                    {
                        existing.IsActive = true;
                        existing.Deleted = null;
                        await _courseSubjectRepository.Update(
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

        public async Task<bool> RevokeSubjects(IEnumerable<CourseSubject> subjects)
        {
            try
            {
                foreach (var subject in subjects)
                {
                    await _context
                        .Set<CourseSubject>()
                        .Where(x =>
                            x.SubjectId == subject.SubjectId
                            && x.CourseId == subject.CourseId
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
