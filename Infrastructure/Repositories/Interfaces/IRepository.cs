namespace CourseService.Infrastructure.Repositories.Interfaces
{
    public interface IRepository<T, TKey>
    {
        Task<IEnumerable<T>> GetAll(int pageNumber, int pageSize, Guid tenantId);
        Task<T?> GetById(TKey id, Guid tenantId);
        Task<T> Create(T entity);
        Task<T> Update(TKey id, T entity, Guid tenantId);
        Task<bool> Delete(TKey id, Guid tenantId);
        Task<int> Count(Guid tenantId);
        IEnumerable<T> GetAllBy(Func<T, bool> predicate);
    }
}
