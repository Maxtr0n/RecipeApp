using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations;

public class ApplicationUserEntityTypeConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder
            .HasMany(x => x.Recipes)
            .WithOne()
            .HasForeignKey(x => x.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}