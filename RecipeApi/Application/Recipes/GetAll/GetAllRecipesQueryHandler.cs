using Application.Common.Abstractions.CQRS;
using Application.Common.Dtos;
using Application.Common.Mappings;
using Ardalis.Result;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.Recipes.GetAll;

public class GetAllRecipesQueryHandler(IGenericRepository<Recipe> recipeRepository)
    : IQueryHandler<GetAllRecipesQuery, Result<List<RecipeReadDto>>>
{
    public async Task<Result<List<RecipeReadDto>>> Handle(GetAllRecipesQuery request,
        CancellationToken cancellationToken)
    {
        var recipes = await recipeRepository.GetAllAsync();

        return recipes.MapToReadDtos();
    }
}