using Application.Common.Abstractions.CQRS;
using Application.Common.Dtos;
using Application.Common.Extensions;
using Application.Common.Mappings;
using Ardalis.Result;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Recipes.Create;

public class CreateRecipeCommandHandler(
    UserManager<ApplicationUser> userManager,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateRecipeCommand, Result<RecipeReadDto>>
{
    public async Task<Result<RecipeReadDto>> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.User.Id.ToString());

        if (user == null)
        {
            return Result<RecipeReadDto>.NotFound($"User with id {request.User.Id} does not exist");
        }

        var recipe = user.AddRecipe(request.RecipeCreateDto.Title,
            request.RecipeCreateDto.Ingredients.JoinStrings(),
            request.RecipeCreateDto.Description, request.RecipeCreateDto.Images.JoinStrings());

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return recipe.MapToReadDto();
    }
}