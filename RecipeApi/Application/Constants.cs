namespace Application;

public static class Constants
{
    public static class ErrorMessages
    {
        public const string RecipeNotFoundErrorMessage = "The Recipe with the given ID was not found.";
        public const string RecipeCouldNotBeCreatedErrorMessage = "Could not create recipe.";

        public const string RecipeUserNotFound =
            "The provided user was not found, and the recipe cannot be created without a registered user.";

        public const string RecipeUserDiffersFromAuthenticatedUser =
            "The provided userId for the recipe does not correspond to the Id of the authenticated user.";
    }
}