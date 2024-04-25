using Application.Common.Abstractions.Repositories;
using Application.Common.Dtos;
using Application.Common.Extensions;
using Application.Recipes.Create;
using AutoMapper;
using Domain.Entities;
using FluentAssertions;
using Moq;

namespace UnitTests.Application.Recipes.Create;

public class CreateRecipeCommandHandlerTests
{
    private readonly Mock<IRepository<Recipe>> _recipeRepositoryMock;
    private readonly Mock<IMapper> _mapper;
    private readonly Guid _applicationUserId = Guid.NewGuid();

    public CreateRecipeCommandHandlerTests()
    {
        _mapper = new();
        _recipeRepositoryMock = new();
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenRecipeCannotBeCreated()
    {
        // Arrange
        var dto = new RecipeCreateDto
        {
            Title = "Recipe Title",
            ApplicationUserId = _applicationUserId,
            Ingredients = ["Ingredient1", "Ingredient2"],
            Images = [],
            Description = "Recipe Description"
        };

        var recipe = new Recipe(Guid.NewGuid(), dto.Title, dto.Ingredients.JoinStrings(), dto.Description, dto.Images.JoinStrings(), _applicationUserId);

        _mapper.Setup(x => x.Map<Recipe>(It.IsAny<RecipeCreateDto>())).Returns(recipe);
        _recipeRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Recipe>(), It.IsAny<CancellationToken>())).ReturnsAsync((Recipe)null!);

        var command = new CreateRecipeCommand(dto);
        var handler = new CreateRecipeCommandHandler(_mapper.Object, _recipeRepositoryMock.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Status.Should().Be(Ardalis.Result.ResultStatus.CriticalError);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccessResult_WhenRecipeWasCreated()
    {
        // Arrange
        var dto = new RecipeCreateDto
        {
            Title = "Recipe Title",
            ApplicationUserId = _applicationUserId,
            Ingredients = ["Ingredient1", "Ingredient2"],
            Images = [],
            Description = "Recipe Description"
        };

        var readDto = new RecipeReadDto
        {
            Id = Guid.NewGuid(),
            Title = "Recipe Title",
            ApplicationUserId = _applicationUserId,
            Ingredients = ["Ingredient1", "Ingredient2"],
            Images = [],
            Description = "Recipe Description"
        };

        var recipe = new Recipe(Guid.NewGuid(), dto.Title, dto.Ingredients.JoinStrings(), dto.Description, dto.Images.JoinStrings(), _applicationUserId);

        _mapper.Setup(x => x.Map<Recipe>(It.IsAny<RecipeCreateDto>())).Returns(recipe);
        _mapper.Setup(x => x.Map<RecipeReadDto>(It.IsAny<Recipe>())).Returns(readDto);
        _recipeRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Recipe>(), It.IsAny<CancellationToken>())).ReturnsAsync(recipe);

        var command = new CreateRecipeCommand(dto);
        var handler = new CreateRecipeCommandHandler(_mapper.Object, _recipeRepositoryMock.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Id.Should().NotBeEmpty();
        result.Value.Title.Should().Be(dto.Title);
        result.Value.ApplicationUserId.Should().Be(dto.ApplicationUserId);
        result.Value.Ingredients.Should().Contain(dto.Ingredients);
        result.Value.Description.Should().Be(dto.Description);
    }
}
