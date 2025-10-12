using Application;
using Application.Recipes.GetById;
using Ardalis.Result;
using Domain.Abstractions;
using Domain.Entities;
using FluentAssertions;
using Moq;

namespace UnitTests.Application.Recipes.GetById;

public class GetRecipeByIdQueryHandlerTests
{
    private readonly Recipe _recipe;
    private readonly Mock<IGenericRepository<Recipe>> _recipeRepositoryMock;

    public GetRecipeByIdQueryHandlerTests()
    {
        _recipeRepositoryMock = new Mock<IGenericRepository<Recipe>>();

        _recipe = new Recipe(
            Constants.RecipeTitle,
            Constants.RecipeIngredients,
            Constants.RecipeInstructions,
            Constants.RecipePrepTime,
            Constants.RecipeCookingTime,
            Constants.RecipeServings,
            Constants.UserId,
            Constants.RecipeDescription,
            Constants.RecipeImageUrls);
    }

    [Fact]
    public async Task Handle_Should_ReturnRecipe_When_Recipe_Exists()
    {
        //Arrange
        _recipeRepositoryMock.Setup(x => x.GetByIdAsync(_recipe.Id)).ReturnsAsync(_recipe);
        var command = new GetRecipeByIdQuery(_recipe.Id);
        var commandHandler = new GetRecipeByIdQueryHandler(_recipeRepositoryMock.Object);

        //Act
        var result = await commandHandler.Handle(command, CancellationToken.None);

        //Assert
        result.IsSuccess.Should().BeTrue();
        var recipeResult = result.Value;
        recipeResult.Should().NotBeNull();
        recipeResult.Title.Should().Be(Constants.RecipeTitle);
        recipeResult.Description.Should().Be(Constants.RecipeDescription);
        recipeResult.Instructions.Should().Be(Constants.RecipeInstructions);
        recipeResult.Servings.Should().Be(Constants.RecipeServings);
        recipeResult.CookingTimeInMinutes.Should().Be(Constants.RecipeCookingTime);
        recipeResult.PreparationTimeInMinutes.Should().Be(Constants.RecipePrepTime);
        recipeResult.Images.Should().BeEquivalentTo(Constants.RecipeImageUrlDtos);
        recipeResult.Ingredients.Should().HaveCount(Constants.RecipeIngredientDtos.Count);
        recipeResult.Ingredients[0].Name.Should().Be(Constants.RecipeIngredientDtos[0].Name);
        recipeResult.Ingredients[0].Quantity.Amount.Should().Be(Constants.RecipeIngredientDtos[0].Quantity.Amount);
        recipeResult.Ingredients[0].Quantity.Unit.Should().Be(Constants.RecipeIngredientDtos[0].Quantity.Unit);
    }

    [Fact]
    public async Task Handle_Should_Return_NotFound_Error_When_Recipe_Does_Not_Exist()
    {
        //Arrange
        _recipeRepositoryMock.Setup(x => x.GetByIdAsync(_recipe.Id)).ReturnsAsync((Recipe)null!);
        var command = new GetRecipeByIdQuery(_recipe.Id);
        var commandHandler = new GetRecipeByIdQueryHandler(_recipeRepositoryMock.Object);

        //Act
        var result = await commandHandler.Handle(command, CancellationToken.None);

        //Assert
        result.IsSuccess.Should().BeFalse();
        result.Status.Should().Be(ResultStatus.NotFound);
        result.Errors.Count().Should().Be(1);
        result.Errors.First().Should().Be(ErrorMessages.RecipeNotFoundErrorMessage);
    }
}