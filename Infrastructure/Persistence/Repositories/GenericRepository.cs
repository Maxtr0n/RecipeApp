using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class GenericRepository<T>(RecipeDbContext context) : IGenericRepository<T>
    where T : class, IAggregateRoot
{
    private readonly RecipeDbContext _context = context;
    private readonly DbSet<T> _dbSet = context.Set<T>();

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<List<T>> GetAllAsync(bool tracking = true)
    {
        IQueryable<T> query = _dbSet;

        if (!tracking)
        {
            query = query.AsNoTracking();
        }

        return await query.ToListAsync();
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var entityToDelete = await _dbSet.FindAsync(id);

        if (entityToDelete == null)
        {
            return;
        }

        _dbSet.Remove(entityToDelete);
    }
}