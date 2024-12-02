using Domain.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class ApplicationUser : IdentityUser<Guid>, IAggregateRoot
{
    public List<Recipe> Recipes { get; set; } = [];

    public Recipe AddRecipe(
        string title,
        string ingredients,
        string description,
        string? images)
    {
        var recipe = new Recipe(Guid.NewGuid(), title, ingredients, description, images, this);

        Recipes.Add(recipe);

        return recipe;
    }
}