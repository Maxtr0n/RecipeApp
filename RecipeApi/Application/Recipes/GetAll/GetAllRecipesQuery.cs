using Application.Common.Abstractions.CQRS;
using Application.Common.Dtos;
using SharedKernel;

namespace Application.Recipes.GetAll;
public record GetAllRecipesQuery() : IQuery<Result<List<RecipeReadDto>>>;
