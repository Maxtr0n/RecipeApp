using Application.Common.Abstractions.CQRS;
using Application.Common.Dtos;
using Ardalis.Result;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.Recipes.GetAll;

public class GetAllRecipesQueryHandler(IGenericRepository<Recipe> genericRepository)
    : IQueryHandler<GetAllRecipesQuery, Result<List<RecipeReadDto>>>
{
    //TODO: use result class from ardalis?
    public async Task<Result<List<RecipeReadDto>>> Handle(GetAllRecipesQuery request,
        CancellationToken cancellationToken)
    {
        var recipes = await genericRepository.ListAsync(cancellationToken);

        return recipes.MapToReadDtos();
    }
}