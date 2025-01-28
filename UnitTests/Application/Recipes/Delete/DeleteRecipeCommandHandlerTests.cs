using Application.Recipes.Delete;
using Domain.Abstractions;
using Domain.Entities;
using FluentAssertions;
using Moq;

namespace UnitTests.Application.Recipes.Delete;

public class DeleteRecipeCommandHandlerTests
{
    private readonly Mock<IGenericRepository<Recipe>> _recipeRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public DeleteRecipeCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _recipeRepositoryMock = new Mock<IGenericRepository<Recipe>>();
    }

    [Fact]
    public async Task Handle_Should_Return_Success_If_Recipe_Is_Deleted()
    {
        // Arrange
        var id = Guid.NewGuid();

        var command = new DeleteRecipeCommand(id);
        var handler = new DeleteRecipeCommandHandler(_recipeRepositoryMock.Object, _unitOfWorkMock.Object);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.IsSuccess.Should().BeTrue();
    }
}