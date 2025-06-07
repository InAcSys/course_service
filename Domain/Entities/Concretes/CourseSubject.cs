using CourseService.Domain.Entities.Abstracts;

namespace CourseService.Domain.Entities.Concretes
{
    public class CourseSubject : Entity<int>
    {
        public Guid CourseId { get; set; }
        public Guid SubjectId { get; set; }
    }
}
