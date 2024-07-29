
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using pracadyplomowa.Const;

namespace pracadyplomowa;

public class TokenService : ITokenService
{
    private const int ACCESS_TOKEN_EXPIRY_TIME = 30;
    private const int REFRESH_TOKEN_EXPIRY_TIME = 7;
    
    private readonly SymmetricSecurityKey _key;
    private readonly UserManager<User> _userManager;

    public TokenService(IConfiguration config, UserManager<User> userManager)
    {
        _userManager = userManager;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
    }
    
    public async Task<string> CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.GivenName, user.UserName)
        };

        var roles = await _userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(ACCESS_TOKEN_EXPIRY_TIME),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
    
    public async Task<string> CreateRefreshToken(User user, bool populateExpiryTime = true)
    {
        var refreshToken = GenerateRefreshToken();
        user.RefreshToken = refreshToken;
        
        if (populateExpiryTime)
        {
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(REFRESH_TOKEN_EXPIRY_TIME);
        }
        
        await _userManager.UpdateAsync(user);
        return refreshToken;
    }

    public async Task<(string AccessToken, string RefreshToken)> RefreshToken(string refreshToken)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshToken == refreshToken);

        if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            throw new SecurityTokenException("Invalid or expired refresh token");
        }

        var newAccessToken = await CreateToken(user);
        var newRefreshToken = GenerateRefreshToken(); 
        
        // Update the user's refresh token and expiry time in the database
        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(REFRESH_TOKEN_EXPIRY_TIME);
        await _userManager.UpdateAsync(user);

        return (newAccessToken, newRefreshToken);
    }
    
    public void SetTokenCookie(string token, HttpContext context)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = false, // Set to true in production
            IsEssential = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddMinutes(ACCESS_TOKEN_EXPIRY_TIME),
            Path = "/" // Ensure cookie is available for the entire domain
        };

        context.Response.Cookies.Append(ConstVariables.COOKIE_NAME, token, cookieOptions);
    }
    
    public void SetRefreshTokenCookie(string refreshToken, HttpContext context)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = false, // Set to true in production
            IsEssential = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(REFRESH_TOKEN_EXPIRY_TIME),
            Path = "/"
        };

        context.Response.Cookies.Append(ConstVariables.REFRESH_COOKIE_NAME, refreshToken, cookieOptions);
    }
    
    public async Task RevokeToken(string refreshToken)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshToken == refreshToken);

        if (user != null)
        {
            // Invalidate the refresh token
            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(-2);
            await _userManager.UpdateAsync(user);
        }
    }
    
    // private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    // {
    //     var tokenHandler = new JwtSecurityTokenHandler();
    //     var principal = tokenHandler.ValidateToken(token, GetTokenValidationParameters(), out var securityToken);
    //
    //     if (securityToken is not JwtSecurityToken jwtSecurityToken ||
    //         !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase))
    //     {
    //         throw new SecurityTokenException("Invalid token");
    //     }
    //
    //     return principal;
    // }
    
    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
    
    // private TokenValidationParameters GetTokenValidationParameters()
    // {
    //     return new TokenValidationParameters
    //     {
    //         ValidateIssuerSigningKey = true,
    //         IssuerSigningKey = _key,
    //         ValidateIssuer = false,
    //         ValidateAudience = false,
    //         ValidateLifetime = false,
    //         ClockSkew = TimeSpan.Zero
    //     };
    // }
}
