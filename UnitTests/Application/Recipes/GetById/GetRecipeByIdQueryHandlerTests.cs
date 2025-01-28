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

        var user = new ApplicationUser();
        _recipe = new Recipe(Guid.NewGuid(), "Test Recipe Title", "Salt;Pepper;", "Description", null, user);
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
        result.Value.Id.Should().Be(_recipe.Id);
        result.Value.Title.Should().Be(_recipe.Title);
        result.Value.Description.Should().Be(_recipe.Description);
        result.Value.Ingredients.Count.Should().Be(2);
        result.Value.Ingredients[0].Should().Be("Salt");
        result.Value.Ingredients[1].Should().Be("Pepper");
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
        result.Errors.First().Should().Be(Constants.ErrorMessages.RecipeNotFoundErrorMessage);
    }
}