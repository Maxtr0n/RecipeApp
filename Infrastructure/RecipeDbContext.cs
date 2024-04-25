using Domain.Entities;
using Infrastructure.EntityConfigurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;
public class RecipeDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public RecipeDbContext(DbContextOptions<RecipeDbContext> options) : base(options) { }

    public DbSet<Recipe> Recipes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RecipeEntityTypeConfiguration).Assembly);
    }
}
