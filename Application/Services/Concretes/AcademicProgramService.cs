using CourseService.Application.Services.Abstracts;
using CourseService.Application.Validators.Interfaces;
using CourseService.Domain.Entities.Concretes;
using CourseService.Infrastructure.Repositories.Interfaces;

namespace CourseService.Application.Services.Concretes
{
    public class AcademicProgramService(
        ICreateValidator<AcademicProgram> createValidator,
        IUpdateValidator<AcademicProgram> updateValidator,
        IProgramRepository repository
    ) : AbstractProgramService(createValidator, updateValidator, repository) { }
}
