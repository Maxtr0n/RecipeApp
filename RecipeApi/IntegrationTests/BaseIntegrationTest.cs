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

    protected async Task<Guid> CreateRecipeForTesting()
    {
        var createRecipeDto = new RecipeCreateDto
        {
            Title = Constants.RecipeTitle,
            Instructions = Constants.RecipeInstructions,
            Ingredients = Constants.RecipeIngredients,
            Description = Constants.RecipeDescription,
            PreparationTimeInMinutes = Constants.RecipePrepTime,
            CookingTimeInMinutes = Constants.RecipeCookingTime,
            Servings = Constants.RecipeServings,
            ImageUrls = Constants.RecipeImageUrlDtos
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

    protected async Task<IEnumerable<Guid>> CreateRecipesForTesting(int numberOfRecipesToCreate = 2)
    {
        var recipeIds = new List<Guid>();

        for (var i = 0; i < numberOfRecipesToCreate; i++)
        {
            var dto = new RecipeCreateDto
            {
                Title = Constants.RecipeTitle,
                Instructions = Constants.RecipeInstructions,
                Ingredients = Constants.RecipeIngredients,
                Description = Constants.RecipeDescription,
                PreparationTimeInMinutes = Constants.RecipePrepTime,
                CookingTimeInMinutes = Constants.RecipeCookingTime,
                Servings = Constants.RecipeServings,
                ImageUrls = Constants.RecipeImageUrlDtos
            };

            var createCommand = new CreateRecipeCommand(dto, Constants.UserId);
            var createRecipeResult = await Sender.Send(createCommand);

            if (!createRecipeResult.IsSuccess || createRecipeResult == null)
            {
                throw new Exception("Failed to create recipe.");
            }

            recipeIds.Add(createRecipeResult.Value.Id);
        }

        await UnitOfWork.SaveChangesAsync();

        return recipeIds;
    }
}