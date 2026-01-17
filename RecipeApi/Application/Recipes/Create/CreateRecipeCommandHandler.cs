using Application.Common.Abstractions.CQRS;
using Application.Common.Dtos;
using Application.Common.Mappings;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using SharedKernel;

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

        var recipe = request.RecipeCreateDto.MapToEntity(request.UserId);

        await recipeRepository.AddAsync(recipe);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Recipe created successfully");

        return Result.Success(recipe.MapToReadDto());
    }
}