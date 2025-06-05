using CourseService.Domain.Entities.Abstracts;

namespace CourseService.Domain.Entities.Concretes
{
    public class Subject : Entity<Guid>
    {
        public string ShortName { get; set; } = "";
        public string Code { get; set; } = "";
        public int LMSId { get; set; }
        public Guid CourseId { get; set; }
    }
}
