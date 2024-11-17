using Application.Common.Abstractions.CQRS;
using Application.Common.Abstractions.Repositories;
using Application.Common.Dtos;
using Application.Common.Mappings;
using Ardalis.Result;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Recipes.Create;

public class CreateRecipeCommandHandler(IRepository<Recipe> repository, UserManager<ApplicationUser> userManager)
    : ICommandHandler<CreateRecipeCommand, Result<RecipeReadDto>>
{
    public async Task<Result<RecipeReadDto>> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.RecipeCreateDto.AuthorId.ToString());

        if (user == null)
        {
            return Result.NotFound(Constants.ErrorMessages.RECIPE_USER_NOT_FOUND);
        }

        var recipe = request.RecipeCreateDto.MapToEntity();

        var result = await repository.AddAsync(recipe, cancellationToken);

        if (result is null)
        {
            return Result.CriticalError(Constants.ErrorMessages.RECIPE_COULD_NOT_CREATE_ERROR_MESSAGE);
        }

        await repository.SaveChangesAsync(cancellationToken);

        return result.MapToReadDto();
    }
}