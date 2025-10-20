using Application.Recipes.GetAll;
using FluentAssertions;

namespace IntegrationTests.Recipes;

public class GetAllRecipesTest(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task GetAll_Should_Return_All_Recipes_From_Database()
    {
        // Arrange
        var recipeIds = await CreateRecipesForTesting();

        var getALlQuery = new GetAllRecipesQuery();

        // Act
        var result = await Sender.Send(getALlQuery);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Count.Should().Be(2);

        // Check that all returned recipes have expected IDs
        result.Value.Select(r => r.Id).Should().BeEquivalentTo(recipeIds);

        // Check that all returned recipes have non-empty titles
        result.Value.All(r => !string.IsNullOrWhiteSpace(r.Title)).Should().BeTrue();

        // Check that at least one recipe has the expected title
        result.Value.Any(r => r.Title == Constants.RecipeTitle).Should().BeTrue();

        // Check recipe ingredients
        result.Value.All(r => r.Ingredients != null).Should().BeTrue();
        result.Value.All(r => r.Ingredients.Count != 0).Should().BeTrue();
        result.Value.FirstOrDefault()?.Ingredients.Should().BeEquivalentTo(Constants.RecipeIngredientReadDtos);

        // Check servings, cooking time, and preparation time
        result.Value.All(r => r.Servings > 0).Should().BeTrue();
        result.Value.All(r => r.CookingTimeInMinutes >= 0).Should().BeTrue();
        result.Value.All(r => r.PreparationTimeInMinutes >= 0).Should().BeTrue();

        // Check required fields are not empty
        result.Value.All(r => !string.IsNullOrWhiteSpace(r.Instructions)).Should().BeTrue();
        result.Value.All(r => !string.IsNullOrWhiteSpace(r.AuthorId)).Should().BeTrue();

        // Check timestamps
        result.Value.All(r => r.CreatedAtUtc != default).Should().BeTrue();
        result.Value.All(r => r.UpdatedAtUtc != default).Should().BeTrue();
        result.Value.All(r => r.CreatedAtUtc <= r.UpdatedAtUtc).Should().BeTrue();

        // Check that all returned recipes are unique by ID
        result.Value.Select(r => r.Id).Distinct().Count().Should().Be(result.Value.Count);

    }
}