using Application.Common.Abstractions.CQRS;
using Application.Common.Abstractions.Repositories;
using Application.Common.Dtos;
using Application.Recipes.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Specifications;
using SharedKernel;

namespace Application.Recipes.CommandHandlers;

public class UpdateRecipeCommandHandler(IMapper mapper, IRepository<Recipe> repository) : ICommandHandler<UpdateRecipeCommand, Result<RecipeReadDto>>
{
    public async Task<Result<RecipeReadDto>> Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipeToUpdate = await repository.SingleOrDefaultAsync(new RecipeByIdSpec(request.Id), cancellationToken);

        if (recipeToUpdate == null)
        {
            return Result.NotFound(Constants.RECIPE_NOT_FOUND_ERROR_MESSAGE);
        }

        recipeToUpdate.Update(request.RecipeUpdateDto.Title,
            request.RecipeUpdateDto.Ingredients,
            request.RecipeUpdateDto.Description,
            request.RecipeUpdateDto.Images,
            request.RecipeUpdateDto.Author);

        await repository.UpdateAsync(recipeToUpdate);

        await repository.SaveChangesAsync(cancellationToken);

        return mapper.Map<RecipeReadDto>(recipeToUpdate)!;
    }
}

