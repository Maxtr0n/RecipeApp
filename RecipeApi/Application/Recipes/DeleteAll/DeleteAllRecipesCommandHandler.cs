using Application.Common.Abstractions.CQRS;
using Domain.Abstractions;
using Domain.Entities;
using SharedKernel;

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