using CourseService.Domain.Entities.Abstracts;

namespace CourseService.Domain.Entities.Concretes
{
    public class AcademicLevel : Entity<int>
    {
        public int AcademicProgramId { get; set; }
    }
}
