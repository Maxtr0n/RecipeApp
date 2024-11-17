using Application.Common.Abstractions.CQRS;
using Application.Common.Abstractions.Repositories;
using Application.Common.Dtos;
using Application.Common.Mappings;
using Ardalis.Result;
using Domain.Entities;

namespace Application.Recipes.GetAll;

public class GetAllRecipesQueryHandler(IRepository<Recipe> repository)
    : IQueryHandler<GetAllRecipesQuery, Result<List<RecipeReadDto>>>
{
    //TODO: use result class from ardalis?
    public async Task<Result<List<RecipeReadDto>>> Handle(GetAllRecipesQuery request,
        CancellationToken cancellationToken)
    {
        var recipes = await repository.ListAsync(cancellationToken);

        return recipes.MapToReadDtos();
    }
}