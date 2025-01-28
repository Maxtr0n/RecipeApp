using Application.Common.Dtos;
using Application.Recipes.Create;
using Domain.Abstractions;
using Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace UnitTests.Application.Recipes.Create;

public class CreateRecipeCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly ApplicationUser _user = new() { UserName = "test@test.com", Id = Guid.NewGuid() };
    private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;


    public CreateRecipeCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _userManagerMock = new Mock<UserManager<ApplicationUser>>(
            new Mock<IUserStore<ApplicationUser>>().Object,
            new Mock<IOptions<IdentityOptions>>().Object,
            new Mock<IPasswordHasher<ApplicationUser>>().Object,
            Array.Empty<IUserValidator<ApplicationUser>>(),
            Array.Empty<IPasswordValidator<ApplicationUser>>(),
            new Mock<ILookupNormalizer>().Object,
            new Mock<IdentityErrorDescriber>().Object,
            new Mock<IServiceProvider>().Object,
            new Mock<ILogger<UserManager<ApplicationUser>>>().Object);

        _userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(_user);
        _userManagerMock.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(_user);
        _userManagerMock.Setup(userManager => userManager.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
            .Returns(Task.FromResult(IdentityResult.Success));
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

        var command = new CreateRecipeCommand(dto, _user);
        var handler =
            new CreateRecipeCommandHandler(_userManagerMock.Object, _unitOfWorkMock.Object);

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