namespace CourseService.Infrastructure.Repositories.Interfaces
{
    public interface ISearchableRepository<T, TKey> : IRepository<T, TKey>
    {
        Task<T?> GetByName(string name, Guid tenantId);
        Task<IEnumerable<T>> Search(int pageNumber, int pageSize, Guid tenantId, string search);
        Task<int> CountSearchResults(string search, Guid tenantId);
    }
}
