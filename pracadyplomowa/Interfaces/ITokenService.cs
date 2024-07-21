using System.Security.Claims;

namespace pracadyplomowa;

public interface ITokenService
{
    Task<string> CreateToken(User user);
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}
