using System.Security.Claims;

namespace pracadyplomowa;

public static class ClaimsPrincipleExtensions
{
    public static string GetUsername(this ClaimsPrincipal user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }
        
        var claim = user.FindFirst(ClaimTypes.GivenName);
        return claim?.Value ?? throw new InvalidOperationException("GivenName claim not found");
    }
    
    public static int GetUserId(this ClaimsPrincipal user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }
        
        var claim = user.FindFirst(ClaimTypes.NameIdentifier);
        if (claim == null || !int.TryParse(claim.Value, out var userId))
        {
            throw new InvalidOperationException("NameIdentifier claim not found or is not a valid integer");
        }

        return userId;
    }
}