using CourseService.Domain.Entities.Abstracts;

namespace CourseService.Domain.Entities.Concretes
{
    public class ProgramLevel : Entity<int>
    {
        public int ProgramId { get; set; }
        public int LevelId { get; set; }
    }
}
