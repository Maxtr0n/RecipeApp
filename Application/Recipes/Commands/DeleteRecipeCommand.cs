using Application.Common.Abstractions.CQRS;
using SharedKernel;

namespace Application.Recipes.Commands;

public record DeleteRecipeCommand(Guid Id) : ICommand<Result>;