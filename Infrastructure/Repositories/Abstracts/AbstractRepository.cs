using CourseService.Domain.Entities.Abstracts;
using CourseService.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseService.Infrastructure.Repositories.Abstracts
{
    public class AbstractRepository<T, TKey>(DbContext dbContext) : IRepository<T, TKey>
        where T : Entity<TKey>
    {
        protected readonly DbContext _context = dbContext;

        public virtual async Task<T> Create(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<bool> Delete(TKey id, Guid tenantId)
        {
            var entity = await GetById(id, tenantId);
            if (entity is null)
            {
                return false;
            }
            entity.IsActive = false;
            entity.Deleted = DateTime.UtcNow;
            var result = await Update(id, entity, tenantId);
            return result is not null;
        }

        public virtual async Task<IEnumerable<T>> GetAll(
            int pageNumber,
            int pageSize,
            Guid tenantId
        )
        {
            if (pageNumber < 1)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(pageNumber),
                    "Page number must be greater than or equal to 1."
                );
            }

            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(pageSize),
                    "Page size must be greater than or equal to 1."
                );
            }

            var skip = (pageNumber - 1) * pageSize;

            var query = _context.Set<T>().Where(x => x.IsActive);

            if (tenantId != Guid.Empty)
            {
                query = query.Where(x => x.TenantId == tenantId || x.TenantId == Guid.Empty);
            }

            var entities = await query.Skip(skip).Take(pageSize).ToListAsync();

            return entities;
        }

        public virtual async Task<T?> GetById(TKey id, Guid tenantId)
        {
            if (EqualityComparer<TKey>.Default.Equals(id, default))
            {
                throw new ArgumentException("The ID cannot be the default value.", nameof(id));
            }

            var entity = await _context
                .Set<T>()
                .FirstOrDefaultAsync(r =>
                    Equals(r.Id, id)
                    && (Equals(r.TenantId, tenantId) || Equals(r.TenantId, Guid.Empty))
                );

            if (entity is null)
            {
                return null;
            }

            return entity;
        }

        public virtual async Task<T> Update(TKey id, T entity, Guid tenantId)
        {
            if (EqualityComparer<TKey>.Default.Equals(id, default))
            {
                throw new ArgumentNullException(nameof(id));
            }

            var existingEntity = await GetById(id, tenantId);
            if (existingEntity is null)
            {
                throw new InvalidOperationException("The entity to update was not found.");
            }

            var keyProperty = typeof(T).GetProperty("Id");
            if (keyProperty != null && keyProperty.CanWrite)
            {
                keyProperty.SetValue(entity, keyProperty.GetValue(existingEntity));
            }

            existingEntity.Updated = DateTime.UtcNow;
            if (existingEntity.TenantId != Guid.Empty)
            {
                existingEntity.TenantId = tenantId;
            }

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();

            return existingEntity;
        }

        public async Task<int> Count(Guid tenantId)
        {
            var size = await _context
                .Set<T>()
                .Where(user => user.TenantId == tenantId && user.IsActive)
                .CountAsync();
            return size;
        }

        public IEnumerable<T> GetAllBy(Func<T, bool> predicate)
        {
            var entities = _context.Set<T>().Where(predicate).ToList();
            return entities;
        }
    }
}
