using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations;

public class RecipeEntityTypeConfiguration : IEntityTypeConfiguration<Recipe>
{
    public void Configure(EntityTypeBuilder<Recipe> recipe)
    {
        recipe.HasKey(x => x.Id);

        recipe.OwnsMany(r => r.Ingredients, ingredient =>
        {
            ingredient.WithOwner().HasForeignKey("RecipeId");
            ingredient.Property<int>("Id");
            ingredient.HasKey("Id");

            ingredient.OwnsOne(i => i.Quantity, quantity =>
            {
                quantity.Property(q => q.Amount).IsRequired();
                quantity.Property(q => q.Unit).IsRequired();
            });

            ingredient.ToTable("RecipeIngredients");
        });

        recipe.Metadata
            .FindNavigation(nameof(Recipe.Ingredients))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}