using CourseService.Domain.Entities.Abstracts;

namespace CourseService.Domain.Entities.Concretes
{
    public class AcademicProgram : Entity<int>
    {
        public string Name { get; set; } = "";
        public int AcademicLevelId { get; set; }
    }
}