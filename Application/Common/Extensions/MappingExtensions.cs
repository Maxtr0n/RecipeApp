namespace Application.Common.Extensions;
public static class MappingExtensions
{
    public static string JoinListToString(this IEnumerable<string> listToJoin)
    {
        return string.Join(";", listToJoin);
    }

    public static IEnumerable<string> SplitStringToListOfStrings(this string stringToSplit)
    {
        return stringToSplit.Split(';', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
    }
}
