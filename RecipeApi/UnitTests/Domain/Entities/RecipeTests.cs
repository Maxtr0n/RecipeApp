using Domain.Entities;
using FluentAssertions;

namespace UnitTests.Domain.Entities;

public class RecipeTests
{
    private const string ValidTitle = "Recipe Title";
    private const string ValidIngredients = "Ingredient1;Ingredient2";
    private const string ValidDescription = "Recipe Description";
    private const string ValidInstructions = "Recipe Instructions";
    private const int ValidCookingTimeInMinutes = 20;
    private const int ValidPreparationTimeInMinutes = 20;
    private const int ValidServings = 4;
    private const string ValidImageUrls = "https://testurl.com/image1;https://testurl.com/image2";
    private readonly string _validAuthorId = Guid.NewGuid().ToString();

    [Fact]
    public void Constructor_Should_CreateRecipe_WhenParametersAreValid()
    {
        // Arrange

        // Act
        var recipe = new Recipe(
            ValidTitle,
            ValidIngredients,
            ValidInstructions,
            ValidDescription,
            ValidCookingTimeInMinutes,
            ValidPreparationTimeInMinutes,
            ValidServings,
            ValidImageUrls,
            _validAuthorId
        );

        // Assert
        recipe.Should().NotBeNull();
        recipe.Title.Should().Be(ValidTitle);
        recipe.Ingredients.Should().Be(ValidIngredients);
        recipe.Instructions.Should().Be(ValidInstructions);
        recipe.Description.Should().Be(ValidDescription);
        recipe.CookingTimeInMinutes.Should().Be(ValidCookingTimeInMinutes);
        recipe.PreparationTimeInMinutes.Should().Be(ValidPreparationTimeInMinutes);
        recipe.Servings.Should().Be(ValidServings);
        recipe.ImageUrls.Should().Be(ValidImageUrls);
        recipe.AuthorId.Should().Be(_validAuthorId);
    }

    [Fact]
    public void Constructor_Should_ThrowArgumentNullException_WhenTitleIsNull()
    {
        // Arrange
        var act = () =>
        {
            var recipe = new Recipe(
                null!,
                ValidIngredients,
                ValidInstructions,
                ValidDescription,
                ValidCookingTimeInMinutes,
                ValidPreparationTimeInMinutes,
                ValidServings,
                ValidImageUrls,
                _validAuthorId);
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
                ValidIngredients,
                ValidInstructions,
                ValidDescription,
                ValidCookingTimeInMinutes,
                ValidPreparationTimeInMinutes,
                ValidServings,
                ValidImageUrls,
                _validAuthorId);
        };

        // Act & Assert
        act.Should().Throw<ArgumentException>().WithParameterName("title");
    }
}