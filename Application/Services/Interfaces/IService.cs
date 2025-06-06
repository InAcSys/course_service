namespace CourseService.Application.Services.Interfaces
{
    public interface IService<T, TKey>
    {
        public Task<IEnumerable<T>> GetAll(int pageNumber, int pageSize, Guid tenantId);
        public Task<T?> GetById(TKey id, Guid tenantId);
        public Task<T> Create(T entity);
        public Task<T> Update(TKey id, T entity, Guid tenantId);
        public Task<bool> Delete(TKey id, Guid tenantId);
        Task<int> Count(Guid tenantId);
        Task<IEnumerable<T>> Search(int pageNumber, int pageSize, Guid tenantId, string search);
        Task<int> CountSearchResults(string search, Guid tenantId);
    }
}
