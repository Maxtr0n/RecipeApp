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
            Title = Constants.RecipeTitle,
            Instructions = Constants.RecipeInstructions,
            Ingredients = Constants.RecipeIngredientDtos,
            Description = Constants.RecipeDescription,
            PreparationTimeInMinutes = Constants.RecipePrepTime,
            CookingTimeInMinutes = Constants.RecipeCookingTime,
            Servings = Constants.RecipeServings,
            ImageUrls = Constants.RecipeImageUrlDtos
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
}