using Application.Common.Abstractions.CQRS;
using Application.Common.Dtos;
using Application.Common.Extensions;
using Ardalis.Result;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.Recipes.Update;

public class UpdateRecipeCommandHandler(IGenericRepository<Recipe> genericRepository)
    : ICommandHandler<UpdateRecipeCommand, Result<RecipeReadDto>>
{
    public async Task<Result<RecipeReadDto>> Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipeToUpdate =
            await genericRepository.SingleOrDefaultAsync(new RecipeByIdSpec(request.Id), cancellationToken);

        if (recipeToUpdate == null)
        {
            return Result.NotFound(Constants.ErrorMessages.RecipeNotFoundErrorMessage);
        }

        recipeToUpdate.Update(request.RecipeUpdateDto.Title,
            request.RecipeUpdateDto.Ingredients.JoinStrings(),
            request.RecipeUpdateDto.Description,
            request.RecipeUpdateDto.Images.JoinStrings());

        await genericRepository.UpdateAsync(recipeToUpdate, cancellationToken);

        await genericRepository.SaveChangesAsync(cancellationToken);

        return recipeToUpdate.MapToReadDto();
    }
}