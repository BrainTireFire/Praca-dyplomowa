using System.Security.Claims;

namespace pracadyplomowa;

public interface ITokenService
{
    Task<string> CreateToken(User user);
    bool ValidateToken(string token);
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}
