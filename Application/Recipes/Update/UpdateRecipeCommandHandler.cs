using Application.Common.Abstractions.CQRS;
using Application.Common.Abstractions.Repositories;
using Application.Common.Dtos;
using Application.Common.Extensions;
using Application.Common.Mappings;
using Ardalis.Result;
using Domain.Entities;
using Domain.Specifications;

namespace Application.Recipes.Update;

public class UpdateRecipeCommandHandler(IRepository<Recipe> repository)
    : ICommandHandler<UpdateRecipeCommand, Result<RecipeReadDto>>
{
    public async Task<Result<RecipeReadDto>> Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipeToUpdate = await repository.SingleOrDefaultAsync(new RecipeByIdSpec(request.Id), cancellationToken);

        if (recipeToUpdate == null)
        {
            return Result.NotFound(Constants.ErrorMessages.RECIPE_NOT_FOUND_ERROR_MESSAGE);
        }

        recipeToUpdate.Update(request.RecipeUpdateDto.Title,
            request.RecipeUpdateDto.Ingredients.JoinStrings(),
            request.RecipeUpdateDto.Description,
            request.RecipeUpdateDto.Images.JoinStrings());

        await repository.UpdateAsync(recipeToUpdate, cancellationToken);

        await repository.SaveChangesAsync(cancellationToken);

        return recipeToUpdate.MapToReadDto();
    }
}