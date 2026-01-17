using Application.Common.Abstractions.CQRS;
using SharedKernel;

namespace Application.Recipes.Delete;

public record DeleteRecipeCommand(Guid Id) : ICommand<Result>;