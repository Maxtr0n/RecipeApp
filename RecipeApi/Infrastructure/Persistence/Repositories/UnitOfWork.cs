using Domain.Abstractions;

namespace Infrastructure.Persistence.Repositories;

public class UnitOfWork(RecipeDbContext dbContext) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.SaveChangesAsync(cancellationToken);
    }
}