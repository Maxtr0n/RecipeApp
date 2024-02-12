using Application.Common.Abstractions.CQRS;
using Application.Common.Abstractions.Repositories;
using Application.Recipes.Commands;
using Ardalis.Result;
using AutoMapper;
using Domain.Entities;
using Domain.Specifications;

namespace Application.Recipes.CommandHandlers;

public class DeleteRecipeCommandHandler(IMapper mapper, IRepository<Recipe> repository) : ICommandHandler<DeleteRecipeCommand, Result>
{
    public async Task<Result> Handle(DeleteRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipeToDelete = await repository.SingleOrDefaultAsync(new RecipeByIdSpec(request.Id), cancellationToken);

        if (recipeToDelete == null)
        {
            return Result.NotFound($"The Recipe with the given Id: {request.Id} was not found.");
        }

        await repository.DeleteAsync(recipeToDelete, cancellationToken);

        await repository.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
