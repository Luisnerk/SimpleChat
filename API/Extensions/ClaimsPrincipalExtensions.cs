
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace API.Extensions;
public static class ClaimsPrincipalExtensions
{
    public static int GetUserId(this ClaimsPrincipal user)
    {
        var userIdString = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        return int.Parse(userIdString);
    }
}