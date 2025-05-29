using Application.Common.Abstractions.CQRS;
using Application.Common.Dtos;
using Ardalis.Result;
using Domain.Entities;

namespace Application.Recipes.Create;

public record CreateRecipeCommand(RecipeCreateDto RecipeCreateDto, ApplicationUser User)
    : ICommand<Result<RecipeReadDto>>;