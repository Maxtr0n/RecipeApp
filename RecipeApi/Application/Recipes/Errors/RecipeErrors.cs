using Application.Common;
using SharedKernel;

namespace Application.Recipes.Errors;

public static class RecipeErrors
{
    public static ApplicationError NotFound(Guid id) =>
        new(ErrorCodes.RecipeNotFound, ErrorMessages.RecipeNotFound(id));
}