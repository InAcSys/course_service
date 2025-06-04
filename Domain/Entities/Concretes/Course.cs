using CourseService.Domain.Entities.Abstracts;

namespace CourseService.Domain.Entities.Concretes
{
    public class Course : Entity<Guid>
    {
        public string Name { get; set; } = "";
        public string ShortName { get; set; } = "";
        public int LMSId { get; set; }
        public int GradeId { get; set; }
        public int SubjectId { get; set; }
        public Guid TeacherId { get; set; }
    }
}
