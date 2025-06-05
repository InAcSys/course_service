namespace CourseService.Domain.Entities.Interfaces
{
    public interface IEntity<TKey> : IDateStamp, ITenant
    {
        TKey? Id { get; set; }
        string Name { get; set; }
        bool IsActive { get; set; }
    }
}
