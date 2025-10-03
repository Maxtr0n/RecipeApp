using Application.Common.Abstractions.CQRS;
using Application.Common.Dtos;
using Application.Common.Extensions;
using Application.Common.Mappings;
using Ardalis.Result;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.Recipes.Update;

public class UpdateRecipeCommandHandler(IGenericRepository<Recipe> recipeRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateRecipeCommand, Result<RecipeReadDto>>
{
    public async Task<Result<RecipeReadDto>> Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipeToUpdate =
            await recipeRepository.GetByIdAsync(request.Id);

        if (recipeToUpdate == null)
        {
            return Result.NotFound(Constants.ErrorMessages.RecipeNotFoundErrorMessage);
        }

        if (recipeToUpdate.AuthorId != request.UserId)
        {
            return Result.Forbidden();
        }

        recipeToUpdate.Update(request.RecipeUpdateDto.Title,
            request.RecipeUpdateDto.Ingredients.JoinStrings(),
            request.RecipeUpdateDto.Description,
            request.RecipeUpdateDto.Images.JoinStrings());

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return recipeToUpdate.MapToReadDto();
    }
}