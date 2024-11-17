using Application.Common.Dtos;
using Application.Common.Extensions;
using Domain.Entities;

namespace Application.Common.Mappings;

public static class RecipeMappings
{
    public static Recipe MapToEntity(this RecipeCreateDto recipeCreateDto)
    {
        return new Recipe(Guid.NewGuid(), recipeCreateDto.Title, recipeCreateDto.Ingredients.JoinStrings(),
            recipeCreateDto.Description, recipeCreateDto.Images.JoinStrings(), recipeCreateDto.AuthorId);
    }

    public static RecipeReadDto MapToReadDto(this Recipe recipe)
    {
        return new RecipeReadDto
        {
            Id = recipe.Id,
            Title = recipe.Title,
            Description = recipe.Description,
            AuthorId = recipe.AuthorId.ToString(),
            Ingredients = recipe.Ingredients.SplitStrings().ToList(),
            Images = recipe.Images.SplitStrings().ToList()
        };
    }

    public static List<RecipeReadDto> MapToReadDtos(this List<Recipe> recipes)
    {
        var recipeReadDtos = new List<RecipeReadDto>();

        foreach (var recipe in recipes)
        {
            recipeReadDtos.Add(new RecipeReadDto
            {
                Id = recipe.Id,
                Title = recipe.Title,
                Description = recipe.Description,
                AuthorId = recipe.AuthorId.ToString(),
                Ingredients = recipe.Ingredients.SplitStrings().ToList(),
                Images = recipe.Images.SplitStrings().ToList()
            });
        }

        return recipeReadDtos;
    }
}