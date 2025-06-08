using CourseService.Domain.Entities.Abstracts;

namespace CourseService.Domain.Entities.Concretes
{
    public class AcademicLevel : MainEntity<int>
    {
        public int Order { get; set; }
    }
}
