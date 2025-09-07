using Domain.Entities;
using FluentAssertions;

namespace UnitTests.Domain.Entities;

public class RecipeTests
{
    private const string ValidTitle = "Recipe Title";
    private const string ValidIngredients = "Ingredient1;Ingredient2";
    private const string ValidDescription = "Recipe Description";
    private const string ValidImages = "https://testurl.com/image1;https://testurl.com/image2";
    private readonly string _validAuthorId = Guid.NewGuid().ToString();

    [Fact]
    public void Constructor_Should_CreateRecipe_WhenParametersAreValid()
    {
        // Arrange

        // Act
        var recipe = new Recipe(
            ValidTitle,
            ValidIngredients,
            ValidDescription,
            ValidImages,
            _validAuthorId
        );

        // Assert
        recipe.Should().NotBeNull();
        recipe.Title.Should().Be(ValidTitle);
        recipe.Ingredients.Should().Be(ValidIngredients);
        recipe.Description.Should().Be(ValidDescription);
        recipe.Images.Should().Be(ValidImages);
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
                ValidDescription,
                ValidImages,
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
                ValidDescription,
                ValidImages,
                _validAuthorId);
        };

        // Act & Assert
        act.Should().Throw<ArgumentException>().WithParameterName("title");
    }
}