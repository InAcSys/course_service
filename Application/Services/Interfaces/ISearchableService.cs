namespace CourseService.Application.Services.Interfaces
{
    public interface ISearchableService<T, TKey> : IService<T, TKey>
    {
        Task<IEnumerable<T>> Search(int pageNumber, int pageSize, Guid tenantId, string search);
        Task<int> CountSearchResults(string search, Guid tenantId);
    }
}
