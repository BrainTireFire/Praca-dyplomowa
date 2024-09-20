using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using pracadyplomowa.Const;
using pracadyplomowa.Errors;
using pracadyplomowa.Token.Services;

namespace pracadyplomowa;

public class TokenController : BaseApiController
{
    private readonly ITokenService _tokenService;
    private readonly IAccountRepository _accountRepository;

    public TokenController(ITokenService tokenService, IAccountRepository accountRepository)
    {
        _tokenService = tokenService;
        _accountRepository = accountRepository;
    }
    
    [HttpPost("refresh-token")]
    public async Task<ActionResult> RefreshToken()
    {
        var refreshToken = Request.Cookies[ConstVariables.REFRESH_COOKIE_NAME];

        if (string.IsNullOrEmpty(refreshToken))
        {
            return Unauthorized(new ApiResponse(401, "No refresh token provided"));
        }

        try
        {
            // Refresh the tokens
            var (newAccessToken, newRefreshToken) = await _tokenService.RefreshToken(refreshToken);

            // Set new tokens in cookies
            _tokenService.SetTokenCookie(newAccessToken, HttpContext);
            _tokenService.SetRefreshTokenCookie(newRefreshToken, HttpContext);
            
            return Ok("Tokens refreshed successfully");
        }
        catch (SecurityTokenException ex)
        {
            // Log exception details for internal monitoring, avoid exposing internal details to users
            return Unauthorized(new ApiResponse(401, "Invalid or expired refresh token"));
        }
    }
}