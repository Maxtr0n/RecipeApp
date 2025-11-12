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

        // Assert First Recipe
        firstResult.IsSuccess.Should().BeTrue();
        firstResult.Value.Should().NotBeNull();
        firstResult.Value.Id.Should().Be(recipeIdList[0]);
        firstResult.Value.Title.Should().Be(Constants.RecipeTitle);
        
        // Check ingredients
        firstResult.Value.Ingredients.Should().NotBeNull();
        firstResult.Value.Ingredients.Should().NotBeEmpty();
        firstResult.Value.Ingredients.Should().BeEquivalentTo(Constants.RecipeIngredientReadDtos);
        
        // Check servings, cooking time, and preparation time
        firstResult.Value.Servings.Should().BeGreaterThan(0);
        firstResult.Value.CookingTimeInMinutes.Should().BeGreaterThanOrEqualTo(0);
        firstResult.Value.PreparationTimeInMinutes.Should().BeGreaterThanOrEqualTo(0);
        
        // Check required fields
        firstResult.Value.Instructions.Should().NotBeNullOrWhiteSpace();
        firstResult.Value.AuthorId.Should().NotBeNullOrWhiteSpace();
        
        // Check timestamps
        firstResult.Value.CreatedAtUtc.Should().NotBe(default);
        firstResult.Value.UpdatedAtUtc.Should().NotBe(default);
        firstResult.Value.CreatedAtUtc.Should().Be(firstResult.Value.UpdatedAtUtc);

        // Check specific values
        firstResult.Value.Description.Should().Be(Constants.RecipeDescription);
        firstResult.Value.Instructions.Should().Be(Constants.RecipeInstructions);
        firstResult.Value.Servings.Should().Be(Constants.RecipeServings);
        firstResult.Value.CookingTimeInMinutes.Should().Be(Constants.RecipeCookingTime);
        firstResult.Value.PreparationTimeInMinutes.Should().Be(Constants.RecipePrepTime);
        firstResult.Value.Images.Should().BeEquivalentTo(Constants.RecipeImageUrlDtos);

        // Assert Second Recipe
        secondResult.IsSuccess.Should().BeTrue();
        secondResult.Value.Should().NotBeNull();
        secondResult.Value.Id.Should().Be(recipeIdList[1]);
        secondResult.Value.Title.Should().Be(Constants.RecipeTitle);
        
        // Check ingredients for second recipe
        secondResult.Value.Ingredients.Should().NotBeNull();
        secondResult.Value.Ingredients.Should().NotBeEmpty();
        secondResult.Value.Ingredients.Should().BeEquivalentTo(Constants.RecipeIngredientReadDtos);
        
        // Check servings, cooking time, and preparation time for second recipe
        secondResult.Value.Servings.Should().BeGreaterThan(0);
        secondResult.Value.CookingTimeInMinutes.Should().BeGreaterThanOrEqualTo(0);
        secondResult.Value.PreparationTimeInMinutes.Should().BeGreaterThanOrEqualTo(0);
        
        // Check required fields for second recipe
        secondResult.Value.Instructions.Should().NotBeNullOrWhiteSpace();
        secondResult.Value.AuthorId.Should().NotBeNullOrWhiteSpace();
        
        // Check timestamps for second recipe
        secondResult.Value.CreatedAtUtc.Should().NotBe(default);
        secondResult.Value.UpdatedAtUtc.Should().NotBe(default);
        secondResult.Value.CreatedAtUtc.Should().Be(secondResult.Value.UpdatedAtUtc);

        // Check specific values for second recipe
        secondResult.Value.Description.Should().Be(Constants.RecipeDescription);
        secondResult.Value.Instructions.Should().Be(Constants.RecipeInstructions);
        secondResult.Value.Servings.Should().Be(Constants.RecipeServings);
        secondResult.Value.CookingTimeInMinutes.Should().Be(Constants.RecipeCookingTime);
        secondResult.Value.PreparationTimeInMinutes.Should().Be(Constants.RecipePrepTime);
        secondResult.Value.Images.Should().BeEquivalentTo(Constants.RecipeImageUrlDtos);
    }
}