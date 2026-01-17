namespace SharedKernel;

public sealed record ApplicationError(string Code, string Message)
{
    public static ApplicationError Forbidden() =>
    new("Common.Forbidden", "You do not have permission to perform this action.");

    public static ApplicationError Unauthorized() =>
        new("Common.Unauthorized", "You are not authorized to perform this action.");
}