using Application.Common.Abstractions.CQRS;
using Application.Common.Abstractions.Repositories;
using Ardalis.Result;
using Domain.Entities;
using Domain.Specifications;

namespace Application.Recipes.Delete;

public class DeleteRecipeCommandHandler(IRepository<Recipe> repository) : ICommandHandler<DeleteRecipeCommand, Result>
{
    public async Task<Result> Handle(DeleteRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipeToDelete = await repository.SingleOrDefaultAsync(new RecipeByIdSpec(request.Id), cancellationToken);

        if (recipeToDelete == null)
        {
            return Result.NotFound(Constants.ErrorMessages.RecipeNotFoundErrorMessage);
        }

        await repository.DeleteAsync(recipeToDelete, cancellationToken);

        await repository.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}