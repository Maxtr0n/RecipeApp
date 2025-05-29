using Application.Common.Abstractions.CQRS;
using Ardalis.Result;

namespace Application.Recipes.Delete;

public record DeleteRecipeCommand(Guid Id) : ICommand<Result>;