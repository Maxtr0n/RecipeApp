using Application.Common.Abstractions.CQRS;
using Application.Common.Abstractions.Repositories;
using Application.Common.Dtos;
using Application.Common.Extensions;
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
        var recipe = request.User.AddRecipe(request.RecipeCreateDto.Title,
            request.RecipeCreateDto.Ingredients.JoinStrings(),
            request.RecipeCreateDto.Description, request.RecipeCreateDto.Images.JoinStrings());

        await repository.SaveChangesAsync(cancellationToken);

        return recipe.MapToReadDto();
    }
}