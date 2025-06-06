using CourseService.Domain.Entities.Abstracts;

namespace CourseService.Domain.Entities.Concretes
{
    public class Subject : MainEntity<Guid>
    {
        public int Credits { get; set; }
        public int LMSId { get; set; }
        public int AcademicLevelId { get; set; }
    }
}
