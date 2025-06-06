using CourseService.Domain.Entities.Abstracts;

namespace CourseService.Domain.Entities.Concretes
{
    public class SubjectProgram : Entity<int>
    {
        public Guid SubjectId { get; set; }
        public int AcademicProgramId { get; set; }
    }
}
