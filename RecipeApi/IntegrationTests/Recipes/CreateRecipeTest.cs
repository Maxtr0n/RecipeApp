using Application.Common.Dtos;
using Application.Recipes.Create;
using FluentAssertions;

namespace IntegrationTests.Recipes;

public class CreateRecipeTest(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task Create_Should_Add_Recipe_To_Database()
    {
        // Arrange
        var dto = new RecipeCreateDto
        {
            Title = Constants.RecipeTitle,
            Instructions = Constants.RecipeInstructions,
            Ingredients = Constants.RecipeIngredients,
            Description = Constants.RecipeDescription,
            PreparationTimeInMinutes = Constants.RecipePrepTime,
            CookingTimeInMinutes = Constants.RecipeCookingTime,
            Servings = Constants.RecipeServings,
            ImageUrls = Constants.RecipeImageUrlDtos
        };

        var command = new CreateRecipeCommand(dto, Constants.UserId);

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        DbContext.Recipes.Should().HaveCount(1);
        var recipe = DbContext.Recipes.FirstOrDefault(r => r.Id == result.Value.Id);
        recipe.Should().NotBeNull();
        recipe.Should().NotBeNull();
        recipe.Title.Should().Be(Constants.RecipeTitle);
        recipe.Description.Should().Be(Constants.RecipeDescription);
        recipe.Instructions.Should().Be(Constants.RecipeInstructions);
        recipe.Servings.Should().Be(Constants.RecipeServings);
        recipe.CookingTimeInMinutes.Should().Be(Constants.RecipeCookingTime);
        recipe.PreparationTimeInMinutes.Should().Be(Constants.RecipePrepTime);
        recipe.ImageUrls.Should().BeEquivalentTo(Constants.RecipeImageUrls);
        recipe.Ingredients.Should().HaveCount(Constants.RecipeIngredients.Count);
        recipe.Ingredients.ElementAt(0).Name.Should().Be(Constants.RecipeIngredients[0].Name);
        recipe.Ingredients.ElementAt(0).Quantity.Amount.Should().Be(Constants.RecipeIngredients[0].Quantity.Amount);
        recipe.Ingredients.ElementAt(0).Quantity.Unit.Should().Be(Constants.RecipeIngredients[0].Quantity.Unit);
    }
}