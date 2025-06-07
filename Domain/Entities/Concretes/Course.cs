using CourseService.Domain.Entities.Abstracts;

namespace CourseService.Domain.Entities.Concretes
{
    public class Course : MainEntity<Guid>
    {
        public string AcademicPeriod { get; set; } = "";
        public int AcademicLevelId { get; set; }
        public int AcademicProgramId { get; set; }
    }
}
