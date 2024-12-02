using Application.Common.Abstractions.CQRS;
using Application.Common.Dtos;
using Ardalis.Result;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.Recipes.GetById;

public class GetRecipeByIdQueryHandler(IGenericRepository<Recipe> genericRepository)
    : IQueryHandler<GetRecipeByIdQuery, Result<RecipeReadDto>>
{
    public async Task<Result<RecipeReadDto>> Handle(GetRecipeByIdQuery request, CancellationToken cancellationToken)
    {
        var recipe =
            await genericRepository.SingleOrDefaultAsync(new RecipeByIdReadOnlySpec(request.Id), cancellationToken);

        if (recipe == null)
        {
            return Result.NotFound(Constants.ErrorMessages.RecipeNotFoundErrorMessage);
        }

        return recipe.MapToReadDto();
    }
}