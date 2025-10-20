using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SharedKernel;

namespace Infrastructure.Persistence.Interceptors;

public class AuditableInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        Guard.Against.Null(eventData);

        if (eventData.Context is RecipeDbContext context)
        {
            UpdateAuditableEntities(context);
        }


        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new())
    {
        Guard.Against.Null(eventData);

        if (eventData.Context is RecipeDbContext context)
        {
            UpdateAuditableEntities(context);
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void UpdateAuditableEntities(RecipeDbContext context)
    {
        var now = DateTimeOffset.UtcNow;
        var entities = context.ChangeTracker.Entries<IAuditable>().ToList();

        foreach (var entityEntry in entities)
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Entity.CreatedAtUtc = now;
                entityEntry.Entity.UpdatedAtUtc = now;
            }
            else if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Entity.UpdatedAtUtc = now;
            }
        }
    }
}