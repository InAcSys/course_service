using CourseService.Domain.Entities.Abstracts;

namespace CourseService.Domain.Entities.Concretes
{
    public class Subject : Entity<int>
    {
        public string Name { get; set; } = "";
    }
}
