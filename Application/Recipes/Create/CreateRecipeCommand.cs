using Application.Common.Abstractions.CQRS;
using Application.Common.Dtos;
using Ardalis.Result;

namespace Application.Recipes.Create;

public record CreateRecipeCommand(RecipeCreateDto RecipeCreateDto, string userName) : ICommand<Result<RecipeReadDto>>;