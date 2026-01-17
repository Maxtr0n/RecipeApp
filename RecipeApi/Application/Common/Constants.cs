namespace Application.Common;
public static class ErrorCodes
{
    public const string RecipeNotFound = "Recipe.NotFound";
    public const string RecipeCouldNotBeCreated = "Recipe.CreationFailed";
    public const string RecipeUserNotFound = "Recipe.UserNotFound";
    public const string RecipeUserDiffersFromAuthenticatedUser = "Recipe.UserMismatch";
}

public static class ErrorMessages
{
    public static string RecipeNotFound(Guid id) =>
        $"The Recipe with the given ID '{id}' was not found.";
    public const string RecipeCouldNotBeCreated = "Could not create recipe.";

    public const string RecipeUserNotFound =
        "The provided user was not found, and the recipe cannot be created without a registered user.";

    public const string RecipeUserDiffersFromAuthenticatedUser =
        "The provided userId for the recipe does not correspond to the Id of the authenticated user.";
}

