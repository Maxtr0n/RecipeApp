using Application.Common.Dtos;
using Application.Recipes.Create;
using Domain.Abstractions;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>, IDisposable
{
    private readonly IServiceScope _scope;
    protected readonly RecipeDbContext DbContext;
    protected readonly ISender Sender;
    protected readonly IUnitOfWork UnitOfWork;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();

        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        DbContext = _scope.ServiceProvider.GetRequiredService<RecipeDbContext>();
        UnitOfWork = _scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
    }

    public void Dispose()
    {
        _scope.Dispose();
        DbContext?.Dispose();
    }

    protected async Task<Guid> CreateUserAndRecipeForTesting()
    {
        var createRecipeDto = new RecipeCreateDto
        {
            Title = "My Recipe",
            Description = "My Recipe Description",
            Ingredients = ["Ingredient 1, Ingredient 2, Ingredient 3"],
            Images = []
        };

        var createCommand = new CreateRecipeCommand(createRecipeDto, Constants.UserId);
        var createRecipeResult = await Sender.Send(createCommand);

        if (!createRecipeResult.IsSuccess || createRecipeResult == null)
        {
            throw new Exception("Failed to create recipe");
        }

        await UnitOfWork.SaveChangesAsync();

        return createRecipeResult.Value.Id;
    }

    protected async Task<Guid[]> CreateUserAndTwoRecipesForTesting()
    {
        var dto1 = new RecipeCreateDto
        {
            Title = "My Recipe",
            Description = "My Recipe Description",
            Ingredients = ["Ingredient 1, Ingredient 2, Ingredient 3"],
            Images = []
        };

        var dto2 = new RecipeCreateDto
        {
            Title = "My Recipe 2",
            Description = "My Recipe Description 2",
            Ingredients = ["Ingredient 1, Ingredient 2, Ingredient 3"],
            Images = []
        };

        var createCommand = new CreateRecipeCommand(dto1, Constants.UserId);
        var createRecipeResult = await Sender.Send(createCommand);

        if (!createRecipeResult.IsSuccess || createRecipeResult == null)
        {
            throw new Exception("Failed to create recipe 1");
        }

        var firstRecipeId = createRecipeResult.Value.Id;

        createCommand = new CreateRecipeCommand(dto2, Constants.UserId);
        createRecipeResult = await Sender.Send(createCommand);

        if (!createRecipeResult.IsSuccess || createRecipeResult == null)
        {
            throw new Exception("Failed to create recipe 2");
        }

        var secondRecipeId = createRecipeResult.Value.Id;

        await UnitOfWork.SaveChangesAsync();

        return [firstRecipeId, secondRecipeId];
    }
}