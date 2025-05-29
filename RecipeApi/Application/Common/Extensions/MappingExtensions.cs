namespace Application.Common.Extensions;

public static class MappingExtensions
{
    public static string JoinStrings(this IEnumerable<string> listToJoin)
    {
        return string.Join(";", listToJoin);
    }

    public static IEnumerable<string> SplitStrings(this string? stringToSplit)
    {
        return stringToSplit == null
            ? []
            : stringToSplit.Split(';', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
    }
}