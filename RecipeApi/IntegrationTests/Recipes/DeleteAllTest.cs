using Application.Recipes.DeleteAll;
using FluentAssertions;

namespace IntegrationTests.Recipes;

public class DeleteAllTest : BaseIntegrationTest
{
    public DeleteAllTest(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Delete_All_Should_Remove_All_Recipes_From_Database()
    {
        // Arrange
        await CreateRecipesForTesting();

        var deleteAllCommand = new DeleteAllRecipesCommand();

        // Act
        var result = await Sender.Send(deleteAllCommand);

        // Assert
        result.IsSuccess.Should().BeTrue();
        DbContext.Recipes.Should().HaveCount(0);
    }
}