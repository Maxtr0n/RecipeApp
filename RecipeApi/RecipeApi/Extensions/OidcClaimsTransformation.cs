using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Text.Json;

namespace RecipeApi.Extensions;

public class OidcClaimsTransformation : IClaimsTransformation
{
    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var identity = (ClaimsIdentity)principal.Identity!;
        var resourceAccess = principal.FindFirst("resource_access")?.Value;

        if (string.IsNullOrEmpty(resourceAccess))
        {
            return Task.FromResult(principal);
        }

        var parsed = JsonDocument.Parse(resourceAccess);
        if (!parsed.RootElement.TryGetProperty("recipe-api", out var clientRoles) ||
            !clientRoles.TryGetProperty("roles", out var roles))
        {
            return Task.FromResult(principal);
        }

        foreach (var role in roles.EnumerateArray())
        {
            identity.AddClaim(new Claim(ClaimTypes.Role, role.GetString()!));
        }

        return Task.FromResult(principal);
    }
}