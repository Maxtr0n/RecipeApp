using Application.Common.Dtos;
using Application.Common.Extensions;
using Domain.Entities;

namespace Application.Common.Mappings;

public static class RecipeMappings
{
    public static RecipeReadDto MapToReadDto(this Recipe recipe)
    {
        return new RecipeReadDto
        {
            Id = recipe.Id,
            Title = recipe.Title,
            Instructions = recipe.Instructions,
            AuthorId = recipe.AuthorId,
            Ingredients = recipe.Ingredients.SplitStrings().ToList(),
            Description = recipe.Description,
            PreparationTimeInMinutes = recipe.PreparationTimeInMinutes,
            CookingTimeInMinutes = recipe.CookingTimeInMinutes,
            Servings = recipe.Servings,
            Images = recipe.ImageUrls.SplitStrings().ToList()
        };
    }

    public static List<RecipeReadDto> MapToReadDtos(this List<Recipe> recipes)
    {
        var recipeReadDtos = new List<RecipeReadDto>();

        foreach (var recipe in recipes)
        {
            recipeReadDtos.Add(MapToReadDto(recipe));
        }

        return recipeReadDtos;
    }
}