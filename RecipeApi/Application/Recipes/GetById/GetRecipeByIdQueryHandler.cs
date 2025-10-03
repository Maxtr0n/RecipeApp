using Application.Common.Abstractions.CQRS;
using Application.Common.Dtos;
using Application.Common.Mappings;
using Ardalis.Result;
using Domain.Abstractions;
using Domain.Entities;

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
            return Result.NotFound(ErrorMessages.RecipeNotFoundErrorMessage);
        }

        return recipe.MapToReadDto();
    }
}