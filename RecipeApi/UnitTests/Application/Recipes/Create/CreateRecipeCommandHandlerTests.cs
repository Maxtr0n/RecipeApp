using Application.Common.Dtos;
using Application.Recipes.Create;
using Domain.Abstractions;
using Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTests.Application.Recipes.Create;

public class CreateRecipeCommandHandlerTests
{
    private readonly Mock<IGenericRepository<Recipe>> _recipeRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<ILogger<CreateRecipeCommandHandler>> _loggerMock;
    public CreateRecipeCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _recipeRepositoryMock = new Mock<IGenericRepository<Recipe>>();
        _loggerMock = new Mock<ILogger<CreateRecipeCommandHandler>>();
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccessResult_WhenRecipeWasCreated()
    {
        // Arrange
        var dto = new RecipeCreateDto
        {
            Title = "Recipe Title",
            Ingredients = ["Ingredient1", "Ingredient2"],
            Images = [],
            Description = "Recipe Description"
        };

        var command = new CreateRecipeCommand(dto);
        var handler =
            new CreateRecipeCommandHandler(
                _recipeRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _loggerMock.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Id.Should().NotBeEmpty();
        result.Value.Title.Should().Be(dto.Title);
        result.Value.Ingredients.Should().Contain(dto.Ingredients);
        result.Value.Description.Should().Be(dto.Description);
    }
}