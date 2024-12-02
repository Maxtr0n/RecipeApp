using Application.Common.Abstractions.CQRS;
using Ardalis.Result;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.Recipes.Delete;

public class DeleteRecipeCommandHandler(IGenericRepository<Recipe> recipeRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteRecipeCommand, Result>
{
    public async Task<Result> Handle(DeleteRecipeCommand request, CancellationToken cancellationToken)
    {
        await recipeRepository.DeleteByIdAsync(request.Id);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}