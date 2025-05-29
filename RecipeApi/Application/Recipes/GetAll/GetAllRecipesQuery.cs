using Application.Common.Abstractions.CQRS;
using Application.Common.Dtos;
using Ardalis.Result;

namespace Application.Recipes.GetAll;
public record GetAllRecipesQuery() : IQuery<Result<List<RecipeReadDto>>>;
