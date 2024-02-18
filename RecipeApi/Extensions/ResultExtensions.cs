using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace RecipeApi.Extensions;
public static class ResultExtensions
{
    public static ProblemDetails ToProblemDetails(this Result result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException("Can't convert success result to problem.");
        }

        return new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Bad Request",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Extensions = new Dictionary<string, object?>
            {
                {"errors", new[] {result.Error } }
            }
        };
    }
}
