using Application.Common;
using Application.Common.Abstractions.CQRS;
using Application.Common.Dtos;
using Application.Common.Extensions;
using Application.Common.Mappings;
using Domain.Abstractions;
using Domain.Entities;
using SharedKernel;

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
            return Result.Failure<RecipeReadDto>(new ApplicationError(
             ErrorCodes.RecipeNotFound,
             ErrorMessages.RecipeNotFound(request.Id)));
        }

        if (recipeToUpdate.AuthorId != request.UserId)
        {
            return Result.Failure<RecipeReadDto>(ApplicationError.Forbidden());
        }

        recipeToUpdate.Update(request.RecipeUpdateDto.Title,
            request.RecipeUpdateDto.Ingredients.MapToEntities(),
            request.RecipeUpdateDto.Instructions,
            request.RecipeUpdateDto.PreparationTimeInMinutes,
            request.RecipeUpdateDto.CookingTimeInMinutes,
            request.RecipeUpdateDto.Servings,
            request.RecipeUpdateDto.Description,
            request.RecipeUpdateDto.ImageUrls.JoinStrings());

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(recipeToUpdate.MapToReadDto());
    }
}