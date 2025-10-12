using Application.Common.Abstractions.CQRS;
using Ardalis.Result;

namespace Application.Recipes.DeleteAll;

public record DeleteAllRecipesCommand : ICommand<Result>;