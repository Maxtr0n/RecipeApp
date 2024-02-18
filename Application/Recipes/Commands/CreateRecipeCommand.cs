using Application.Common.Abstractions.CQRS;
using Application.Common.Dtos;
using SharedKernel;

namespace Application.Recipes.Commands;
public record CreateRecipeCommand(RecipeCreateDto RecipeCreateDto) : ICommand<Result<RecipeReadDto>>;