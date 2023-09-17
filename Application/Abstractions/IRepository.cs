using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Abstractions;

public interface IRepository<TEntity> where TEntity : EntityBase
{
    public Task<Guid> CreateAsync(TEntity toCreate);

    public Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "");

    public Task<TEntity> GetByIdAsync(Guid id);

    public Task<TEntity> UpdateAsync(Guid id, TEntity entity);

    public Task DeleteAsync(Guid id);

    public Task DeleteAsync(TEntity entityToDelete);
}
