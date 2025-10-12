using Application.Common.Dtos;
using Application.Common.Extensions;
using Application.Recipes.Update;
using FluentAssertions;

namespace IntegrationTests.Recipes;

public class UpdateRecipeTest(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task Update_Should_Update_Recipe_In_Database()
    {
        // Arrange
        var recipeId = await CreateRecipeForTesting();

        var updateRecipeDto = new RecipeUpdateDto
        {
            Title = Constants.UpdatedRecipeTitle,
            Instructions = Constants.UpdatedRecipeInstructions,
            Ingredients = Constants.UpdatedRecipeIngredients,
            Description = Constants.UpdatedRecipeDescription,
            PreparationTimeInMinutes = Constants.UpdatedRecipePrepTime,
            CookingTimeInMinutes = Constants.UpdatedRecipeCookingTime,
            Servings = Constants.UpdatedRecipeServings,
            ImageUrls = Constants.UpdatedRecipeImageUrls
        };

        var updateCommand = new UpdateRecipeCommand(recipeId, updateRecipeDto, Constants.UserId);

        // Act
        var result = await Sender.Send(updateCommand);

        // Assert
        result.IsSuccess.Should().BeTrue();
        DbContext.Recipes.Should().HaveCount(1);
        var recipe = DbContext.Recipes.FirstOrDefault(r => r.Id == recipeId);
        recipe.Should().NotBeNull();
        recipe.Title.Should().Be(Constants.UpdatedRecipeTitle);
        recipe.Description.Should().Be(Constants.UpdatedRecipeDescription);
        recipe.Instructions.Should().Be(Constants.UpdatedRecipeInstructions);
        recipe.Servings.Should().Be(Constants.UpdatedRecipeServings);
        recipe.CookingTimeInMinutes.Should().Be(Constants.UpdatedRecipeCookingTime);
        recipe.PreparationTimeInMinutes.Should().Be(Constants.UpdatedRecipePrepTime);
        recipe.ImageUrls.Should().BeEquivalentTo(Constants.UpdatedRecipeImageUrls.JoinStrings());
        recipe.Ingredients.Should().HaveCount(Constants.UpdatedRecipeIngredients.Count);
        recipe.Ingredients.ElementAt(0).Name.Should().Be(Constants.UpdatedRecipeIngredients[0].Name);
        recipe.Ingredients.ElementAt(0).Quantity.Amount.Should()
            .Be(Constants.UpdatedRecipeIngredients[0].Quantity.Amount);
        recipe.Ingredients.ElementAt(0).Quantity.Unit.Should().Be(Constants.UpdatedRecipeIngredients[0].Quantity.Unit);
        recipe.Ingredients.ElementAt(1).Name.Should().Be(Constants.UpdatedRecipeIngredients[1].Name);
        recipe.Ingredients.ElementAt(1).Quantity.Amount.Should()
            .Be(Constants.UpdatedRecipeIngredients[1].Quantity.Amount);
        recipe.Ingredients.ElementAt(1).Quantity.Unit.Should().Be(Constants.UpdatedRecipeIngredients[1].Quantity.Unit);
    }
}