using CourseService.Application.Services.Interfaces;
using CourseService.Application.Validators.Interfaces;
using CourseService.Infrastructure.Repositories.Interfaces;
using FluentValidation;

namespace CourseService.Application.Services.Abstracts
{
    public class Service<T, TKey>(
        ICreateValidator<T> createValidator,
        IUpdateValidator<T> updateValidator,
        IRepository<T, TKey> repository
    ) : IService<T, TKey>
    {
        protected readonly ICreateValidator<T> _createValidator = createValidator;
        protected readonly IUpdateValidator<T> _updateValidator = updateValidator;
        protected readonly IRepository<T, TKey> _repository = repository;

        public Task<int> Count(Guid tenantId)
        {
            return _repository.Count(tenantId);
        }

        public async Task<IEnumerable<T>> Search(
            int pageNumber,
            int pageSize,
            Guid tenantId,
            string search
        )
        {
            return await _repository.Search(pageNumber, pageSize, tenantId, search);
        }

        public Task<int> CountSearchResults(string search, Guid tenantId)
        {
            return _repository.CountSearchResults(search, tenantId);
        }

        public virtual Task<T> Create(T entity)
        {
            var result = _createValidator.Validate(entity);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            var createdEntity = _repository.Create(entity);
            return createdEntity;
        }

        public virtual Task<bool> Delete(TKey id, Guid tenantId)
        {
            if (EqualityComparer<TKey>.Default.Equals(id, default))
            {
                throw new ArgumentNullException(nameof(id));
            }
            var result = _repository.Delete(id, tenantId);
            return result;
        }

        public virtual Task<IEnumerable<T>> GetAll(int pageNumber, int pageSize, Guid tenantId)
        {
            var entities = _repository.GetAll(pageNumber, pageSize, tenantId);
            return entities;
        }

        public virtual Task<T?> GetById(TKey id, Guid tenantId)
        {
            if (EqualityComparer<TKey>.Default.Equals(id, default))
            {
                throw new ArgumentNullException(nameof(id));
            }
            var entity = _repository.GetById(id, tenantId);
            return entity;
        }

        public virtual Task<T> Update(TKey id, T entity, Guid tenantId)
        {
            if (EqualityComparer<TKey>.Default.Equals(id, default))
            {
                throw new ArgumentNullException(nameof(id));
            }
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var result = _updateValidator.Validate(entity);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            var updatedEntity = _repository.Update(id, entity, tenantId);
            return updatedEntity;
        }
    }
}
