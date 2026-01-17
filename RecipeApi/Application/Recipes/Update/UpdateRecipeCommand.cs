using Application.Common.Abstractions.CQRS;
using Application.Common.Dtos;
using SharedKernel;

namespace Application.Recipes.Update;

public record UpdateRecipeCommand(Guid Id, RecipeUpdateDto RecipeUpdateDto, string UserId)
    : ICommand<Result<RecipeReadDto>>;