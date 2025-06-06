namespace CourseService.Domain.Entities.Interfaces
{
    public interface IMainEntity<TKey> : IEntity<TKey>
    {
        string Name { get; set; }
        string Description { get; set; }
        string Code { get; set; }
    }
}
