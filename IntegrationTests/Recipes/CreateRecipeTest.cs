using Application.Common.Dtos;
using Application.Recipes.Create;
using Domain.Entities;

namespace IntegrationTests.Recipes;

public class CreateRecipeTest : BaseIntegrationTest
{
    public CreateRecipeTest(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

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

        var user = new ApplicationUser();
        
        var command = new CreateRecipeCommand(dto, user);

        // Act


        // Assert
    }
}