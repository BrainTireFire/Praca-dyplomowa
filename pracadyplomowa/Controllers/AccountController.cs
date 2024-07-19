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
        
        SetTokenCookie(token);

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

        var result = await _accountRepository.LoginUserAsync(loginDto.Username, loginDto.Password);

        if (result.ErrorMessage != null)
        {
            return Unauthorized(new ApiResponse(401));
        }
        
        var token = await _tokenService.CreateToken(user);

        SetTokenCookie(token);

        return Ok(new UserDto
        {
            Username = user.UserName
        });
    }
    
    [HttpPost("logout")]
    public ActionResult Logout()
    { 
        Response.Cookies.Delete(ConstVariables.COOKIE_NAME);

        return NoContent(); 
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

        try
        {
            // Validate and extract principal from the JWT token
            var principal = _tokenService.GetPrincipalFromExpiredToken(token);
            if (principal == null)
            {
                return Unauthorized(new ApiResponse(401, "Invalid token"));
            }
            
            // Console.WriteLine($"principal.Claims: {string.Join(", ", 
            //     principal.Claims.Select(c => $"{c.Type}: {c.Value}"))}");
            
            // Extract claims from principal
            var userIdClaim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var usernameClaim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value;
            
            // Check if essential claims are missing or empty
            if (string.IsNullOrEmpty(userIdClaim) || string.IsNullOrEmpty(usernameClaim))
            {
                return Unauthorized(new ApiResponse(401, "Invalid token claims"));
            }
            
            var user = await _accountRepository.GetUserById(int.Parse(userIdClaim));
            if (user == null || !user.UserName.Equals(usernameClaim))
            {
                return Unauthorized(new ApiResponse(401, "User not found or inactive"));
            }

            // Retrieve user roles from repository
            var roles = await _accountRepository.GetUserRoles(user);

            // Return successful response with authentication result
            return Ok(new ValidateAuthDto
            {
                IsAuthenticated = true,
                Roles = roles,
                Username = user.UserName,
                Email = user.Email
            });
        }
        catch (SecurityTokenException ex)
        {
            return Unauthorized(new ApiResponse(401, "Invalid token"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse(500, "Internal server error"));
        }
    }

    private void SetTokenCookie(string token)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = false, // Set to true in production
            SameSite = SameSiteMode.Lax, // Set to SameSiteMode.Strict in production
            Expires = DateTime.UtcNow.AddDays(1)
        };

        Response.Cookies.Append(ConstVariables.COOKIE_NAME, token, cookieOptions);
    }
}
