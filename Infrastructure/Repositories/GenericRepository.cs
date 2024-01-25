using Application.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
{
    protected readonly RecipeDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public GenericRepository(RecipeDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = context.Set<TEntity>();
    }

    public virtual Guid Create(TEntity toCreate)
    {
        _dbSet.Add(toCreate);
        return toCreate.Id;
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        TEntity? entityToDelete = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        if (entityToDelete != null)
        {
            _dbSet.Remove(entityToDelete);
        }
    }

    public virtual async Task<IEnumerable<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "")
    {
        IQueryable<TEntity> query = _dbSet.AsNoTracking();

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

    public virtual async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _dbSet
              .AsNoTracking()
              .FirstOrDefaultAsync(e => e.Id == id);
    }

    public virtual async Task<TEntity> UpdateAsync(Guid id, TEntity entity)
    {
        TEntity? entityToUpdate = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        if (entityToUpdate == null)
        {
            //TODO: throw EntityNotFoundException()
            throw new Exception();
        }

        _dbSet.Update(entityToUpdate);
        return entityToUpdate;
    }
}