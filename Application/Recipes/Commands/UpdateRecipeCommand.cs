using Application.Common.Abstractions.CQRS;
using Application.Common.Dtos;
using SharedKernel;

namespace Application.Recipes.Commands;

public record UpdateRecipeCommand(Guid Id, RecipeUpdateDto RecipeUpdateDto) : ICommand<Result<RecipeReadDto>>;