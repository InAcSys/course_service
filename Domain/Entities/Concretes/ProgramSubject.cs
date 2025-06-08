using CourseService.Domain.Entities.Abstracts;

namespace CourseService.Domain.Entities.Concretes
{
    public class ProgramSubject : Entity<int>
    {
        public int ProgramId { get; set; }
        public Guid SubjectId { get; set; }
    }
}
