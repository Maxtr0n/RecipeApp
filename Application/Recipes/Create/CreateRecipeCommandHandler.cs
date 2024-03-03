using Application.Common.Abstractions.CQRS;
using Application.Common.Abstractions.Repositories;
using Application.Common.Dtos;
using Ardalis.Result;
using AutoMapper;
using Domain.Entities;

namespace Application.Recipes.Create;

public class CreateRecipeCommandHandler(IMapper mapper, IRepository<Recipe> repository) : ICommandHandler<CreateRecipeCommand, Result<RecipeReadDto>>
{
    public async Task<Result<RecipeReadDto>> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipe = mapper.Map<Recipe>(request.RecipeCreateDto);

        var result = await repository.AddAsync(recipe, cancellationToken);

        if (result is null)
        {
            return Result.CriticalError(Constants.ErrorMessages.RECIPE_COULD_NOT_CREATE_ERROR_MESSAGE);
        }

        await repository.SaveChangesAsync(cancellationToken);

        return mapper.Map<RecipeReadDto>(result);
    }
}
