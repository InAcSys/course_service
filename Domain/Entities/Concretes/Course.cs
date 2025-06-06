using CourseService.Domain.Entities.Abstracts;

namespace CourseService.Domain.Entities.Concretes
{
    public class Course : MainEntity<Guid>
    {
        public int AcademicLevelId { get; set; }
    }
}
