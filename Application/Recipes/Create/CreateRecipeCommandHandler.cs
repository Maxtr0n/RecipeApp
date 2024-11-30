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
        var userById = await userManager.FindByIdAsync(request.RecipeCreateDto.AuthorId.ToString());

        if (userById == null)
        {
            return Result.NotFound(Constants.ErrorMessages.RecipeUserNotFound);
        }

        if (userById.UserName != request.userName)
        {
            return Result.Unauthorized(Constants.ErrorMessages.RecipeUserDiffersFromAuthenticatedUser);
        }

        var recipe = request.RecipeCreateDto.MapToEntity();

        var result = await repository.AddAsync(recipe, cancellationToken);

        await repository.SaveChangesAsync(cancellationToken);

        return result.MapToReadDto();
    }
}