using Application.Common.Abstractions.CQRS;
using Application.Common.Dtos;
using Application.Common.Extensions;
using Application.Common.Mappings;
using Ardalis.Result;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Recipes.Create;

public class CreateRecipeCommandHandler(
    IGenericRepository<Recipe> recipeRepository,
    IUnitOfWork unitOfWork,
    ILogger<CreateRecipeCommandHandler> logger)
    : ICommandHandler<CreateRecipeCommand, Result<RecipeReadDto>>
{
    public async Task<Result<RecipeReadDto>> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating recipe");
        // var recipe = new Recipe(request.RecipeCreateDto.Title,
        //     request.RecipeCreateDto.Ingredients.JoinStrings(), request.RecipeCreateDto.Description,
        //     request.RecipeCreateDto.Images.JoinStrings());
        //
        // await recipeRepository.AddAsync(recipe);
        //
        // await unitOfWork.SaveChangesAsync(cancellationToken);

        // return recipe.MapToReadDto();
        
        logger.LogInformation("Recipe created successfully");

        return new RecipeReadDto();
    }
}