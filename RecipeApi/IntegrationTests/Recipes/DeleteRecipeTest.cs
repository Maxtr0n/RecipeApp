using Application.Recipes.Delete;
using FluentAssertions;

namespace IntegrationTests.Recipes;

public class DeleteRecipeTest(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task Delete_Should_Remove_Recipe_From_Database()
    {
        // Arrange
        var recipeId = await CreateRecipeForTesting();

        var deleteCommand = new DeleteRecipeCommand(recipeId);

        // Act
        var result = await Sender.Send(deleteCommand);

        // Assert
        result.IsSuccess.Should().BeTrue();
        DbContext.Recipes.Should().HaveCount(0);
    }
}