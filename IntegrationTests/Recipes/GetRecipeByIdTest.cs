using Application.Recipes.GetById;
using FluentAssertions;

namespace IntegrationTests.Recipes;

public class GetRecipeByIdTest(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task GetById_Should_Return_Correct_Recipe_From_Database()
    {
        // Arrange
        var recipeIds = await CreateUserAndTwoRecipesForTesting();
        var getFirstRecipeByIdQuery = new GetRecipeByIdQuery(recipeIds[0]);
        var getSecondRecipeByIdQuery = new GetRecipeByIdQuery(recipeIds[1]);

        // Act
        var firstResult = await Sender.Send(getFirstRecipeByIdQuery);
        var secondResult = await Sender.Send(getSecondRecipeByIdQuery);

        // Assert
        firstResult.IsSuccess.Should().BeTrue();
        firstResult.Value.Title.Should().Be("My Recipe");
        firstResult.Value.Id.Should().Be(recipeIds[0]);

        secondResult.IsSuccess.Should().BeTrue();
        secondResult.Value.Title.Should().Be("My Recipe 2");
        secondResult.Value.Id.Should().Be(recipeIds[1]);
    }
}