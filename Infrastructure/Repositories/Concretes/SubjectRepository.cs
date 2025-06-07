using CourseService.Domain.Entities.Concretes;
using CourseService.Infrastructure.Repositories.Abstracts;
using CourseService.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseService.Infrastructure.Repositories.Concretes
{
    public class SubjectRepository(
        DbContext context,
        IRepository<SubjectRequisite, int> subjectRequisiteRepository,
        IRepository<SubjectProgram, int> subjectProgramRepository
    ) : AbstractSubjectRepository(context, subjectRequisiteRepository, subjectProgramRepository) { }
}
