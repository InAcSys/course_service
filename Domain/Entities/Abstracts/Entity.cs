using CourseService.Domain.Entities.Interfaces;

namespace CourseService.Domain.Entities.Abstracts
{
    public class Entity<TKey> : IEntity<TKey>
    {
        public TKey? Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Deleted { get; set; }
        public Guid TenantId { get; set; }
    }
}