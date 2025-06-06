using CourseService.Domain.Entities.Interfaces;

namespace CourseService.Domain.Entities.Abstracts
{
    public class MainEntity<TKey> : Entity<TKey>, IMainEntity<TKey>
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Code { get; set; } = "";
    }
}
