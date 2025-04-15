using CourseService.Domain.Entities.Abstracts;

namespace CourseService.Domain.Entities.Concretes
{
    public class AcademicLevel : Entity<int>
    {
        public string Name { get; set; } = "";
    }
}