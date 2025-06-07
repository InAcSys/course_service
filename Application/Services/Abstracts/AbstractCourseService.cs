using CourseService.Application.Services.Interfaces;
using CourseService.Application.Validators.Interfaces;
using CourseService.Domain.DTOs.CourseSubjects;
using CourseService.Domain.Entities.Concretes;
using CourseService.Infrastructure.Repositories.Interfaces;

namespace CourseService.Application.Services.Abstracts
{
    public abstract class AbstractCourseService(
        ICreateValidator<Course> createValidator,
        IUpdateValidator<Course> updateValidator,
        ICourseRepository courseRepository
    ) : Service<Course, Guid>(createValidator, updateValidator, courseRepository), ICourseService
    {
        private readonly ICourseRepository _courseRepository = courseRepository;

        public async Task<bool> AssignSubjects(CourseSubjectsDTO subjects, Guid tenantId)
        {
            var courseSubjects = ConvertDTOToList(subjects, tenantId);
            var result = await _courseRepository.AssignSubjects(courseSubjects);
            return result;
        }

        public async Task<bool> RevokeSubjects(CourseSubjectsDTO subjects, Guid tenantId)
        {
            var courseSubjects = ConvertDTOToList(subjects, tenantId);
            var result = await _courseRepository.RevokeSubjects(courseSubjects);
            return result;
        }

        private List<CourseSubject> ConvertDTOToList(CourseSubjectsDTO subjects, Guid tenantId)
        {
            if (subjects.SubjectsIds == null || !subjects.SubjectsIds.Any())
                throw new ArgumentNullException(nameof(subjects), "Empty subjects");

            var list = new List<CourseSubject>();

            foreach (var subject in subjects.SubjectsIds)
            {
                var auxiliar = new CourseSubject
                {
                    CourseId = subjects.CourseId,
                    SubjectId = subject,
                    TenantId = tenantId,
                };
                list.Add(auxiliar);
            }

            return list;
        }
    }
}
