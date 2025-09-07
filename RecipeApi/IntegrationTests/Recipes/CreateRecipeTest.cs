using Application.Common.Dtos;
using Application.Recipes.Create;
using Domain.Entities;
using FluentAssertions;

namespace IntegrationTests.Recipes;

public class CreateRecipeTest(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task Create_Should_Add_Recipe_To_Database()
    {
        // Arrange
        var dto = new RecipeCreateDto
        {
            Title = "My Recipe",
            Description = "My Recipe Description",
            Ingredients = ["Ingredient 1, Ingredient 2, Ingredient 3"],
            Images = []
        };

        var command = new CreateRecipeCommand(dto);

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        DbContext.Recipes.Should().HaveCount(1);
        var recipe = DbContext.Recipes.FirstOrDefault(r => r.Id == result.Value.Id);
        recipe.Should().NotBeNull();
        recipe.Title.Should().Be(dto.Title);
        recipe.Description.Should().Be(dto.Description);
    }
}