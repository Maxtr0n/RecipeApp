using Application.Common.Abstractions.CQRS;
using Application.Common.Dtos;
using Ardalis.Result;

namespace Application.Recipes.Queries;
public record GetRecipeByIdQuery(Guid Id) : IQuery<Result<RecipeReadDto>>;
