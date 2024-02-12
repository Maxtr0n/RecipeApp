using Application.Common.Abstractions.CQRS;
using Application.Common.Abstractions.Repositories;
using Application.Common.Dtos;
using Application.Recipes.Queries;
using Ardalis.Result;
using AutoMapper;
using Domain.Entities;
using Domain.Specifications;

namespace Application.Recipes.QueryHandlers;
public class GetRecipeByIdQueryHandler(IMapper mapper, IRepository<Recipe> repository) : IQueryHandler<GetRecipeByIdQuery, Result<RecipeReadDto>>
{
    public async Task<Result<RecipeReadDto>> Handle(GetRecipeByIdQuery request, CancellationToken cancellationToken)
    {
        var recipe = mapper.Map<RecipeReadDto>(await repository.SingleOrDefaultAsync(new RecipeByIdReadOnlySpec(request.Id)));

        if (recipe == null)
        {
            // TODO throw correct exception (EntityNotFoundException)
            throw new Exception();
        }

        return recipe;
    }
}
