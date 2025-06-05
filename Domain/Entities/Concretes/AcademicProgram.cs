using CourseService.Domain.Entities.Abstracts;

namespace CourseService.Domain.Entities.Concretes
{
    public class AcademicProgram : Entity<int>
    {
        public string Name { get; set; } = "";
    }
}
