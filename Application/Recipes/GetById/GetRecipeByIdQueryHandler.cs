using Application.Common.Abstractions.CQRS;
using Application.Common.Abstractions.Repositories;
using Application.Common.Dtos;
using Application.Common.Mappings;
using Ardalis.Result;
using Domain.Entities;
using Domain.Specifications;

namespace Application.Recipes.GetById;

public class GetRecipeByIdQueryHandler(IRepository<Recipe> repository)
    : IQueryHandler<GetRecipeByIdQuery, Result<RecipeReadDto>>
{
    public async Task<Result<RecipeReadDto>> Handle(GetRecipeByIdQuery request, CancellationToken cancellationToken)
    {
        var recipe = await repository.SingleOrDefaultAsync(new RecipeByIdReadOnlySpec(request.Id), cancellationToken);

        if (recipe == null)
        {
            return Result.NotFound(Constants.ErrorMessages.RECIPE_NOT_FOUND_ERROR_MESSAGE);
        }

        return recipe.MapToReadDto();
    }
}