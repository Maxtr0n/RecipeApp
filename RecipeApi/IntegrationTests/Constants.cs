using Application.Common.Dtos;

namespace IntegrationTests;

public static class Constants
{
    public const string UserId = "2eeb7e68-e906-4992-9ef5-5072f02e90ca";
    public const string RecipeTitle = "TestRecipe";
    public const string RecipeDescription = "TestRecipeDescription";
    public const string RecipeInstructions = "TestRecipeInstructions";
    public const int RecipeServings = 5;
    public const int RecipeCookingTime = 30;
    public const int RecipePrepTime = 20;
    public static readonly List<string> RecipeImageUrlDtos = ["Images 1", "Images 2", "Images 3"];
    public const string RecipeImageUrls = "Images 1;Images 2;Images 3";

    public static readonly List<IngredientCreateDto> RecipeIngredients =
    [
        new() { Name = "Ingredient1", Quantity = new QuantityCreateDto { Amount = 5.0, Unit = "liter" } }
    ];

    public static readonly List<IngredientReadDto> RecipeIngredientReadDtos =
   [
       new() { Name = "Ingredient1", Quantity = new QuantityReadDto { Amount = 5.0, Unit = "liter" } }
   ];

    public const string UpdatedRecipeTitle = "UpdatedTestRecipe";
    public const string UpdatedRecipeDescription = "UpdatedTestRecipeDescription";
    public const string UpdatedRecipeInstructions = "UpdatedTestRecipeInstructions";
    public const int UpdatedRecipeServings = 6;
    public const int UpdatedRecipeCookingTime = 35;
    public const int UpdatedRecipePrepTime = 25;
    public static readonly List<string> UpdatedRecipeImageUrls = ["Updated Image 1", "Updated Image 2"];

    public static readonly List<IngredientCreateDto> UpdatedRecipeIngredients =
    [
        new() { Name = "UpdatedIngredient1", Quantity = new QuantityCreateDto { Amount = 3.0, Unit = "cup" } },
        new() { Name = "UpdatedIngredient2", Quantity = new QuantityCreateDto { Amount = 2.5, Unit = "tablespoon" } }
    ];
}