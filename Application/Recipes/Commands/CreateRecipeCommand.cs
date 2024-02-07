using Application.Common.Abstractions.CQRS;
using Application.Common.Dtos;

namespace Application.Recipes.Commands;
public record CreateRecipeCommand(RecipeCreateDto RecipeCreateDto) : ICommand<RecipeReadDto>;