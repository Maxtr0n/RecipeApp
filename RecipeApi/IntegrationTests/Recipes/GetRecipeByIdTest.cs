using Application.Recipes.GetById;
using FluentAssertions;

namespace IntegrationTests.Recipes;

public class GetRecipeByIdTest(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task GetById_Should_Return_Correct_Recipe_From_Database()
    {
        // Arrange
        var recipeIds = await CreateRecipesForTesting();
        var recipeIdList = recipeIds.ToList();
        var getFirstRecipeByIdQuery = new GetRecipeByIdQuery(recipeIdList[0]);
        var getSecondRecipeByIdQuery = new GetRecipeByIdQuery(recipeIdList[1]);

        // Act
        var firstResult = await Sender.Send(getFirstRecipeByIdQuery);
        var secondResult = await Sender.Send(getSecondRecipeByIdQuery);

        // Assert
        firstResult.IsSuccess.Should().BeTrue();
        firstResult.Value.Title.Should().Be(Constants.RecipeTitle);
        firstResult.Value.Id.Should().Be(recipeIdList[0]);

        secondResult.IsSuccess.Should().BeTrue();
        secondResult.Value.Title.Should().Be(Constants.RecipeTitle);
        secondResult.Value.Id.Should().Be(recipeIdList[1]);
    }
}