using Application.Common.Abstractions.CQRS;
using Ardalis.Result;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.Recipes.Delete;

public class DeleteRecipeCommandHandler(IGenericRepository<Recipe> genericRepository)
    : ICommandHandler<DeleteRecipeCommand, Result>
{
    public async Task<Result> Handle(DeleteRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipeToDelete =
            await genericRepository.SingleOrDefaultAsync(new RecipeByIdSpec(request.Id), cancellationToken);

        if (recipeToDelete == null)
        {
            return Result.NotFound(Constants.ErrorMessages.RecipeNotFoundErrorMessage);
        }

        await genericRepository.DeleteAsync(recipeToDelete, cancellationToken);

        await genericRepository.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}