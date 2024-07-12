
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace pracadyplomowa;

public class TokenService : ITokenService
{
    private const string SECURITY_ALGORITHM = "HS512";
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
            new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
            new Claim("id", user.Id.ToString()),
            new Claim("username", user.UserName)
        };

        var roles = await _userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
    
    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
    
        // Configure token validation parameters
        var validationParameters = GetTokenValidationParameters();

        SecurityToken securityToken;
        try
        {
            // Validate token
            var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);
        
            // Ensure the token is a JWT security token
            if (!(securityToken is JwtSecurityToken jwtSecurityToken))
            {
                throw new SecurityTokenException("Invalid token");
            }

            // Validate token algorithm
            if (!jwtSecurityToken.Header.Alg.Equals(SECURITY_ALGORITHM, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token algorithm");
            }

            return principal;
        }
        catch (Exception ex)
        {
            throw new SecurityTokenException("Invalid token", ex);
        }
    }
    
    private TokenValidationParameters GetTokenValidationParameters()
    {
        return new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = _key,
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };
    }
}
