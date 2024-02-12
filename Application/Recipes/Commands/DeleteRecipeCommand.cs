using Application.Common.Abstractions.CQRS;
using Ardalis.Result;

namespace Application.Recipes.Commands;

public record DeleteRecipeCommand(Guid Id) : ICommand<Result>;