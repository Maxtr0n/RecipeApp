using Application.Common.Abstractions.CQRS;
using Application.Common.Dtos;

namespace Application.Recipes.Queries;
public record GetAllRecipesQuery() : IQuery<List<RecipeReadDto>>;
