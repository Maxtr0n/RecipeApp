using Application.Common.Abstractions.CQRS;
using Application.Common.Dtos;
using Application.Common.Mappings;
using Ardalis.Result;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Recipes.GetAll;

public class GetAllRecipesQueryHandler(IGenericRepository<Recipe> recipeRepository, ILogger<GetAllRecipesQueryHandler> logger)
    : IQueryHandler<GetAllRecipesQuery, Result<List<RecipeReadDto>>>
{
    public async Task<Result<List<RecipeReadDto>>> Handle(GetAllRecipesQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all recipes");
        
        var recipes = await recipeRepository.GetAllAsync();

        return recipes.MapToReadDtos();
    }
}