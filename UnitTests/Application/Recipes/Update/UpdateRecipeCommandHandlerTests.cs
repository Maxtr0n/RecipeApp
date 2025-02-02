using Application;
using Application.Common.Dtos;
using Application.Recipes.GetById;
using Application.Recipes.Update;
using Ardalis.Result;
using Domain.Abstractions;
using Domain.Entities;
using FluentAssertions;
using Moq;

namespace UnitTests.Application.Recipes.Update;

public class UpdateRecipeCommandHandlerTests
{
    private readonly Recipe _recipe;
    private readonly Mock<IGenericRepository<Recipe>> _recipeRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public UpdateRecipeCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _recipeRepositoryMock = new Mock<IGenericRepository<Recipe>>();

        var user = new ApplicationUser { Id = Guid.NewGuid() };
        _recipe = new Recipe("Test Recipe Title", "Salt;Pepper;", "Description", null, user.Id);
    }

    [Fact]
    public async Task Handle_Should_UpdateRecipe_When_Recipe_Exists()
    {
        //Arrange
        _recipeRepositoryMock.Setup(x => x.GetByIdAsync(_recipe.Id)).ReturnsAsync(_recipe);

        var dto = new RecipeUpdateDto
        {
            Title = "New Title",
            Description = "New Description",
            Ingredients = ["Ingredient1;Ingredient2;Ingredient3"]
        };

        var command = new UpdateRecipeCommand(_recipe.Id, dto);
        var commandHandler = new UpdateRecipeCommandHandler(_recipeRepositoryMock.Object, _unitOfWorkMock.Object);

        //Act
        var result = await commandHandler.Handle(command, CancellationToken.None);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(_recipe.Id);
        result.Value.Title.Should().Be("New Title");
        result.Value.Description.Should().Be("New Description");
        result.Value.Ingredients.Count.Should().Be(3);
        result.Value.Ingredients[0].Should().Be("Ingredient1");
        result.Value.Ingredients[1].Should().Be("Ingredient2");
        result.Value.Ingredients[2].Should().Be("Ingredient3");
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