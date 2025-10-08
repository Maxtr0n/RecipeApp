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
    private readonly Mock<ILogger<CreateRecipeCommandHandler>> _loggerMock;
    private readonly Mock<IGenericRepository<Recipe>> _recipeRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

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
            ImageUrls = [],
            Instructions = "Recipe Description"
        };

        var command = new CreateRecipeCommand(dto, Constants.UserId);
        var handler =
            new CreateRecipeCommandHandler(
                _recipeRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _loggerMock.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Id.Should().NotBeEmpty();
        result.Value.Title.Should().Be(dto.Title);
        result.Value.Ingredients.Should().Contain(dto.Ingredients);
        result.Value.Instructions.Should().Be(dto.Instructions);
        result.Value.Ingredients.Count.Should().Be(2);
        result.Value.Ingredients[0].Should().Be("Ingredient1");
        result.Value.Ingredients[1].Should().Be("Ingredient2");
        result.Value.Description.Should().Be(dto.Description);
        result.Value.CookingTimeInMinutes.Should().Be(dto.CookingTimeInMinutes);
        result.Value.PreparationTimeInMinutes.Should().Be(dto.PreparationTimeInMinutes);
        result.Value.Servings.Should().Be(dto.Servings);
    }
}