using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using pracadyplomowa.Const;
using pracadyplomowa.Errors;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Models.DTOs.Account;
using pracadyplomowa.Token.Services;

namespace pracadyplomowa;

public class AccountController : BaseApiController
{
    private readonly ITokenService _tokenService;
    private readonly IAccountRepository _accountRepository;

    public AccountController(ITokenService tokenService, IAccountRepository accountRepository)
    {
        _tokenService = tokenService;
        _accountRepository = accountRepository;
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        var userExists = await _accountRepository.GetUserByUsername(registerDto.Username);

        if (userExists != null)
        {
            return new BadRequestObjectResult(new ApiValidationErrorResponse
            {
                Errors = new []{"User with this username already exists."}
            });
        }

        var (result, user) = await _accountRepository.RegisterUserAsync(registerDto, registerDto.Password);

        if (!result.Succeeded)
        {
            return BadRequest(new ApiResponse(400));
        }

        if (user == null)
        {
            return BadRequest(new ApiResponse(400));
        }

        var roleResult = await _accountRepository.AddUserToRoleAsync(user, "User");

        if (!roleResult.Succeeded)
        {
            return BadRequest(new ApiResponse(400));
        }

        var token = await _tokenService.CreateToken(user);
        var refreshToken = await _tokenService.CreateRefreshToken(user, true);

        _tokenService.SetTokenCookie(token, HttpContext);
        _tokenService.SetRefreshTokenCookie(refreshToken, HttpContext);

        return Ok(new UserDto
        {
            Username = user.UserName
        });
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _accountRepository.GetUserByUsername(loginDto.Username);

        if (user == null)
        {
            return Unauthorized(new ApiResponse(401));
        }

        if (user.IsDeleted)
        {
            return Unauthorized(new ApiResponse(401));
        }

        var result = await _accountRepository.LoginUserAsync(loginDto.Username, loginDto.Password);

        if (result.ErrorMessage != null)
        {
            return Unauthorized(new ApiResponse(401));
        }
        
        var token = await _tokenService.CreateToken(user);
        var refreshToken = await _tokenService.CreateRefreshToken(user, true);

        _tokenService.SetTokenCookie(token, HttpContext);
        _tokenService.SetRefreshTokenCookie(refreshToken, HttpContext);

        return Ok(new UserDto
        {
            Username = user.UserName
        });
    }
    
    [Authorize]
    [HttpPost("logout")]
    public async Task<ActionResult> Logout()
    {
        var refreshToken = Request.Cookies[ConstVariables.REFRESH_COOKIE_NAME];

        if (string.IsNullOrEmpty(refreshToken))
        {
            return Unauthorized(new ApiResponse(401, "No refresh token provided"));
        }

        try
        {
            // Revoke the refresh token
            await _tokenService.RevokeToken(refreshToken);

            // Clear cookies
            Response.Cookies.Delete(ConstVariables.COOKIE_NAME);
            Response.Cookies.Delete(ConstVariables.REFRESH_COOKIE_NAME);

            return NoContent();
        }
        catch (SecurityTokenException ex)
        {
            // Log exception details
            return Unauthorized(new ApiResponse(401, "Invalid or expired refresh token"));
        }
    }

    [Authorize]
    [HttpGet("current-user")]
    public async Task<ActionResult<ValidateAuthDto>> GetCurrentUserAsync()
    {
        var token = Request.Cookies[ConstVariables.COOKIE_NAME];
        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized(new ApiResponse(401, "No token found"));
        }

        var username = User.GetUsername();
        var userId = User.GetUserId();
        
        var user = await _accountRepository.GetUserById(userId);
        if (user == null || !user.UserName.Equals(username))
        {
            return Unauthorized(new ApiResponse(401, "User not found or inactive"));
        }
        
        var roles = await _accountRepository.GetUserRoles(user);
        
        return Ok(new ValidateAuthDto
        {
            IsAuthenticated = true,
            Roles = roles,
            Username = user.UserName,
            Email = user.Email
        });
    }
}
