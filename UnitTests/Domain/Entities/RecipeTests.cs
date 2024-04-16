using Domain.Entities;
using FluentAssertions;

namespace UnitTests.Domain.Entities;
public class RecipeTests
{
    private const string VALID_TITLE = "Recipe Title";
    private ApplicationUser VALID_AUTHOR = new();
    private const string VALID_INGREDIENTS = "Ingredient1;Ingredient2";
    private const string VALID_DESCRIPTION = "Recipe Description";
    private const string VALID_IMAGES = "https://testurl.com/image1;https://testurl.com/image2";
    private readonly Guid VALID_ID = Guid.NewGuid();

    [Fact]
    public void Constructor_Should_CreateRecipe_WhenParametersAreValid()
    {
        // Arrange

        // Act
        var recipe = new Recipe(
            VALID_ID,
            VALID_TITLE,
            VALID_INGREDIENTS,
            VALID_DESCRIPTION,
            VALID_IMAGES,
            VALID_AUTHOR
            );

        // Assert
        recipe.Should().NotBeNull();
        recipe.Id.Should().Be(VALID_ID);
        recipe.Title.Should().Be(VALID_TITLE);
        recipe.Ingredients.Should().Be(VALID_INGREDIENTS);
        recipe.Description.Should().Be(VALID_DESCRIPTION);
        recipe.Images.Should().Be(VALID_IMAGES);
        recipe.Author.Should().Be(VALID_AUTHOR);
    }

    [Fact]
    public void Constructor_Should_ThrowArgumentNullException_WhenTitleIsNull()
    {
        // Arrange
        Action act = () => new Recipe(VALID_ID,
            null,
            VALID_INGREDIENTS,
            VALID_DESCRIPTION,
            VALID_IMAGES,
            VALID_AUTHOR);

        // Act & Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("title");
    }

    [Fact]
    public void Constructor_Should_ThrowArgumentException_WhenTitleIsEmpty()
    {
        // Arrange
        Action act = () => new Recipe(VALID_ID,
            string.Empty,
            VALID_INGREDIENTS,
            VALID_DESCRIPTION,
            VALID_IMAGES,
            VALID_AUTHOR);

        // Act & Assert
        act.Should().Throw<ArgumentException>().WithParameterName("title");
    }
}
