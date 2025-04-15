using CourseService.Domain.Entities.Abstracts;

namespace CourseService.Domain.Entities.Concretes
{
    public class Program : Entity<int>
    {
        public string Name { get; set; } = "";
        public int AcademicLevel { get; set; }
    }
}