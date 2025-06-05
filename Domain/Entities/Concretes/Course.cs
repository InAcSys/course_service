using CourseService.Domain.Entities.Abstracts;

namespace CourseService.Domain.Entities.Concretes
{
    public class Course : Entity<Guid>
    {
        public string Name { get; set; } = "";
    }
}
