using Application.Common.Dtos;
using Application.Common.Extensions;
using Domain.Entities;
using Domain.ValueObjects;

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
            Ingredients = recipe.Ingredients.MapToReadDtos(),
            Description = recipe.Description,
            PreparationTimeInMinutes = recipe.PreparationTimeInMinutes,
            CookingTimeInMinutes = recipe.CookingTimeInMinutes,
            Servings = recipe.Servings,
            Images = recipe.ImageUrls.SplitStrings().ToList(),
            CreatedAtUtc = recipe.CreatedAtUtc,
            UpdatedAtUtc = recipe.UpdatedAtUtc
        };
    }

    public static List<RecipeReadDto> MapToReadDtos(this List<Recipe> recipes)
    {
        return recipes.Select(MapToReadDto).ToList();
    }

    public static IngredientReadDto MapToReadDto(this Ingredient ingredient)
    {
        return new IngredientReadDto { Quantity = ingredient.Quantity.MapToReadDto(), Name = ingredient.Name };
    }

    public static List<IngredientReadDto> MapToReadDtos(this IReadOnlyCollection<Ingredient> ingredients)
    {
        return ingredients.Select(MapToReadDto).ToList();
    }

    public static QuantityReadDto MapToReadDto(this Quantity quantity)
    {
        return new QuantityReadDto { Amount = quantity.Amount, Unit = quantity.Unit };
    }

    public static Recipe MapToEntity(this RecipeCreateDto recipeCreateDto, string authorId)
    {
        return new Recipe(recipeCreateDto.Title,
            recipeCreateDto.Ingredients.MapToEntities(),
            recipeCreateDto.Instructions,
            recipeCreateDto.PreparationTimeInMinutes,
            recipeCreateDto.CookingTimeInMinutes,
            recipeCreateDto.Servings,
            authorId,
            recipeCreateDto.Description,
            recipeCreateDto.ImageUrls.JoinStrings()
        );
    }

    public static Ingredient MapToEntity(this IngredientCreateDto ingredientCreateDto)
    {
        return new Ingredient(ingredientCreateDto.Name, ingredientCreateDto.Quantity.MapToEntity());
    }

    public static IReadOnlyCollection<Ingredient> MapToEntities(this List<IngredientCreateDto> ingredientCreateDtos)
    {
        return ingredientCreateDtos.Select(MapToEntity).ToList();
    }

    public static Quantity MapToEntity(this QuantityCreateDto quantityCreateDto)
    {
        return new Quantity(quantityCreateDto.Amount, quantityCreateDto.Unit);
    }
}