using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;
public class RecipeEntityTypeConfiguration : IEntityTypeConfiguration<Recipe>
{
    public void Configure(EntityTypeBuilder<Recipe> recipeConfiguration)
    {
        recipeConfiguration
            .Property(x => x.Ingredients)
            .HasField("_ingredients")
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        recipeConfiguration
            .Property(x => x.Images)
            .HasField("_images")
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        //var ingredientsProp = recipeConfiguration.Metadata.AddProperty(nameof(Recipe.Ingredients));
        //ingredientsProp.SetPropertyAccessMode(PropertyAccessMode.Field);

        //var imagesProp = recipeConfiguration.Metadata.AddProperty(nameof(Recipe.Images));
        //imagesProp.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
