using CourseService.Application.Services.Interfaces;
using CourseService.Application.Validators.Interfaces;
using CourseService.Domain.Entities.Concretes;
using CourseService.Infrastructure.Repositories.Interfaces;

namespace CourseService.Application.Services.Abstracts
{
    public abstract class AbstractLevelService(
        ICreateValidator<AcademicLevel> createValidator,
        IUpdateValidator<AcademicLevel> updateValidator,
        ILevelRepository levelRepository,
        ISubjectRepository subjectRepository,
        ICourseRepository courseRepository
    )
        : Service<AcademicLevel, int>(createValidator, updateValidator, levelRepository),
            ILevelService
    {
        private readonly ISubjectRepository _subjectRepository = subjectRepository;
        private readonly ICourseRepository _courseRepository = courseRepository;

        public IEnumerable<Course> GetCourses(int levelId, Guid tenantId)
        {
            var courses = _courseRepository.GetAllBy(c =>
                c.AcademicLevelId == levelId && c.TenantId == tenantId
            );
            return courses;
        }

        public IEnumerable<Subject> GetSubjects(int levelId, Guid tenantId)
        {
            var subjects = _subjectRepository.GetAllBy(s =>
                s.AcademicLevelId == levelId && s.TenantId == tenantId
            );
            return subjects;
        }
    }
}
