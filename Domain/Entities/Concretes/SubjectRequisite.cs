using CourseService.Domain.Entities.Abstracts;

namespace CourseService.Domain.Entities.Concretes
{
    public class SubjectRequisite : Entity<int>
    {
        public Guid SubjectId { get; set; }
        public Guid SubjectRequisiteId { get; set; }
    }
}
