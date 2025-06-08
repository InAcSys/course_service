using CourseService.Application.Services.Abstracts;
using CourseService.Application.Validators.Interfaces;
using CourseService.Domain.Entities.Concretes;
using CourseService.Infrastructure.Repositories.Interfaces;

namespace CourseService.Application.Services.Concretes
{
    public class AcademicLevelService(
        ICreateValidator<AcademicLevel> createValidator,
        IUpdateValidator<AcademicLevel> updateValidator,
        ILevelRepository levelRepository,
        ICourseRepository courseRepository,
        ISubjectRepository subjectRepository
    )
        : AbstractLevelService(
            createValidator,
            updateValidator,
            levelRepository,
            subjectRepository,
            courseRepository
        ) { }
}
