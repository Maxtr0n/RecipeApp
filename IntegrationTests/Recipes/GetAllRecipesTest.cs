using Application.Recipes.GetAll;
using FluentAssertions;

namespace IntegrationTests.Recipes;

public class GetAllRecipesTest(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task GetAll_Should_Return_All_Recipes_From_Database()
    {
        // Arrange
        var recipeIds = await CreateUserAndTwoRecipesForTesting();

        var getALlQuery = new GetAllRecipesQuery();

        // Act
        var result = await Sender.Send(getALlQuery);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Count.Should().Be(2);
        result.Value.Exists(r => r.Title == "My Recipe").Should().BeTrue();
        result.Value.Exists(r => r.Id == recipeIds[0]).Should().BeTrue();
        result.Value.Exists(r => r.Title == "My Recipe 2").Should().BeTrue();
        result.Value.Exists(r => r.Id == recipeIds[1]).Should().BeTrue();
    }
}