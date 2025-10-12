using Domain.Entities;
using FluentAssertions;

namespace UnitTests.Domain.Entities;

public class RecipeTests
{
    [Fact]
    public void Constructor_Should_CreateRecipe_WhenParametersAreValid()
    {
        // Arrange

        // Act
        var recipe = new Recipe(
            Constants.RecipeTitle,
            Constants.RecipeIngredients,
            Constants.RecipeInstructions,
            Constants.RecipePrepTime,
            Constants.RecipeCookingTime,
            Constants.RecipeServings,
            Constants.UserId,
            Constants.RecipeDescription,
            Constants.RecipeImageUrls
        );

        // Assert
        recipe.Should().NotBeNull();
        recipe.Title.Should().Be(Constants.RecipeTitle);
        recipe.Ingredients.Should().BeEquivalentTo(Constants.RecipeIngredients);
        recipe.Instructions.Should().Be(Constants.RecipeInstructions);
        recipe.Description.Should().Be(Constants.RecipeDescription);
        recipe.CookingTimeInMinutes.Should().Be(Constants.RecipeCookingTime);
        recipe.PreparationTimeInMinutes.Should().Be(Constants.RecipePrepTime);
        recipe.Servings.Should().Be(Constants.RecipeServings);
        recipe.ImageUrls.Should().Be(Constants.RecipeImageUrls);
        recipe.AuthorId.Should().Be(Constants.UserId);
    }

    [Fact]
    public void Constructor_Should_ThrowArgumentNullException_WhenTitleIsNull()
    {
        // Arrange
        var act = () =>
        {
            var recipe = new Recipe(
                null!,
                Constants.RecipeIngredients,
                Constants.RecipeInstructions,
                Constants.RecipePrepTime,
                Constants.RecipeCookingTime,
                Constants.RecipeServings,
                Constants.UserId,
                Constants.RecipeDescription,
                Constants.RecipeImageUrls);
        };

        // Act & Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("title");
    }

    [Fact]
    public void Constructor_Should_ThrowArgumentException_WhenTitleIsEmpty()
    {
        // Arrange
        var act = () =>
        {
            var recipe = new Recipe(
                string.Empty,
                Constants.RecipeIngredients,
                Constants.RecipeInstructions,
                Constants.RecipePrepTime,
                Constants.RecipeCookingTime,
                Constants.RecipeServings,
                Constants.UserId,
                Constants.RecipeDescription,
                Constants.RecipeImageUrls);
        };

        // Act & Assert
        act.Should().Throw<ArgumentException>().WithParameterName("title");
    }
}