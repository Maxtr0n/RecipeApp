using Application.Common.Abstractions.CQRS;
using Ardalis.Result;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.Recipes.DeleteAll;

public class DeleteAllRecipesCommandHandler(IGenericRepository<Recipe> recipeRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteAllRecipesCommand, Result>
{
    public async Task<Result> Handle(DeleteAllRecipesCommand request, CancellationToken cancellationToken)
    {
        await recipeRepository.DeleteAllAsync();
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}