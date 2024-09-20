using System.Security.Claims;

namespace pracadyplomowa.Token.Services;

public interface ITokenService
{
    Task<string> CreateToken(User user);
    Task<string> CreateRefreshToken(User user, bool populateExpiryTime = true);
    Task<(string AccessToken, string RefreshToken)> RefreshToken(string refreshToken);
    Task RevokeToken(string refreshToken);
    void SetRefreshTokenCookie(string refreshToken, HttpContext context);
    void SetTokenCookie(string token, HttpContext context);
}
