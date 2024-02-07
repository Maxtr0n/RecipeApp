using Application.Common.Abstractions.CQRS;
using Application.Common.Abstractions.Repositories;
using Application.Common.Dtos;
using Application.Recipes.Queries;
using AutoMapper;
using Domain.Entities;

namespace Application.Recipes.QueryHandlers;

public class GetAllRecipesQueryHandler(IMapper mapper, IRepository<Recipe> repository) : IQueryHandler<GetAllRecipesQuery, List<RecipeReadDto>>
{
    //TODO: use result class from ardalis?
    public async Task<List<RecipeReadDto>> Handle(GetAllRecipesQuery request, CancellationToken cancellationToken)
    {
        var recipes = mapper.Map<List<RecipeReadDto>>(await repository.ListAsync(cancellationToken)) ?? [];

        return recipes;
    }
}
