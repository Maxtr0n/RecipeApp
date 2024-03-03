using Application.Common.Abstractions.CQRS;
using Application.Common.Abstractions.Repositories;
using Application.Common.Dtos;
using Application.Common.Extensions;
using Ardalis.Result;
using AutoMapper;
using Domain.Entities;
using Domain.Specifications;

namespace Application.Recipes.Update;

public class UpdateRecipeCommandHandler(IMapper mapper, IRepository<Recipe> repository) : ICommandHandler<UpdateRecipeCommand, Result<RecipeReadDto>>
{
    public async Task<Result<RecipeReadDto>> Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipeToUpdate = await repository.SingleOrDefaultAsync(new RecipeByIdSpec(request.Id), cancellationToken);

        if (recipeToUpdate == null)
        {
            return Result.NotFound(Constants.ErrorMessages.RECIPE_NOT_FOUND_ERROR_MESSAGE);
        }

        recipeToUpdate.Update(request.RecipeUpdateDto.Title,
            request.RecipeUpdateDto.Ingredients.JoinListToString(),
            request.RecipeUpdateDto.Description,
            request.RecipeUpdateDto.Images.JoinListToString(),
            request.RecipeUpdateDto.Author);

        await repository.UpdateAsync(recipeToUpdate, cancellationToken);

        await repository.SaveChangesAsync(cancellationToken);

        return mapper.Map<RecipeReadDto>(recipeToUpdate)!;
    }
}

