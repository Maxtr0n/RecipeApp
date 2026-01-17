using Application.Common.Abstractions.CQRS;
using SharedKernel;

namespace Application.Recipes.DeleteAll;

public record DeleteAllRecipesCommand : ICommand<Result>;