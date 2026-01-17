using Application.Common;
using Application.Common.Abstractions.CQRS;
using Application.Common.Dtos;
using Application.Common.Mappings;
using Domain.Abstractions;
using Domain.Entities;
using SharedKernel;

namespace Application.Recipes.GetById;

public class GetRecipeByIdQueryHandler(IGenericRepository<Recipe> recipeRepository)
    : IQueryHandler<GetRecipeByIdQuery, Result<RecipeReadDto>>
{
    public async Task<Result<RecipeReadDto>> Handle(GetRecipeByIdQuery request, CancellationToken cancellationToken)
    {
        var recipe =
            await recipeRepository.GetByIdAsync(request.Id);

        if (recipe == null)
        {
            return Result.Failure<RecipeReadDto>(new ApplicationError(
                ErrorCodes.RecipeNotFound, 
                ErrorMessages.RecipeNotFound(request.Id)));
        }

        return Result.Success(recipe.MapToReadDto());
    }
}