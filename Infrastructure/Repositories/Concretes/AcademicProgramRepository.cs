using CourseService.Domain.Entities.Concretes;
using CourseService.Infrastructure.Repositories.Abstracts;
using CourseService.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseService.Infrastructure.Repositories.Concretes
{
    public class AcademicProgramRepository(
        IRepository<ProgramLevel, int> programLevelRepository,
        IRepository<ProgramSubject, int> programSubjectRepository,
        DbContext context
    ) : AbstractProgramRepository(programLevelRepository, programSubjectRepository, context) { }
}
