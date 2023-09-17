using Application.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
{
    protected readonly RecipeDbContext _context;
    protected readonly DbSet<TEntity> dbSet;

    public GenericRepository(RecipeDbContext context)
    {
        _context = context;
        dbSet = context.Set<TEntity>();
    }

    public virtual Task<Guid> CreateAsync(TEntity toCreate)
    {
        throw new NotImplementedException();
    }

    public virtual Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public virtual Task DeleteAsync(TEntity entityToDelete)
    {
        throw new NotImplementedException();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "")
    {
        IQueryable<TEntity> query = dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (string includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if (orderBy != null)
        {
            return await orderBy(query).ToListAsync();
        }
        else
        {
            return await query.ToListAsync();
        }
    }

    public virtual Task<TEntity> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public virtual Task<TEntity> UpdateAsync(Guid id, TEntity entity)
    {
        throw new NotImplementedException();
    }
}