using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using pracadyplomowa.Const;
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
            return BadRequest("Username already exists on this username: " + registerDto.Username);
        }

        var (result, user) = await _accountRepository.RegisterUserAsync(registerDto, registerDto.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        if (user == null)
        {
            return BadRequest("Something went wrong with creating an account. Please try again later.");
        }

        var roleResult = await _accountRepository.AddUserToRoleAsync(user, "User");

        if (!roleResult.Succeeded)
        {
            return BadRequest(roleResult.Errors);
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
            return Unauthorized();
        }

        var result = await _accountRepository.LoginUserAsync(loginDto.Username, loginDto.Password);

        if (result.ErrorMessage != null)
        {
            return Unauthorized("Invalid username or password");
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

    
    [HttpPost("validate-token")]
    public async Task<ActionResult<ValidateAuthDto>> ValidateUserAuthentication()
    {
        var token = Request.Cookies["JwtCookie"];
        if (string.IsNullOrEmpty(token))
        {
            return BadRequest("Token is required");
        }

        try
        {
            // Validate and extract principal from the JWT token
            var principal = _tokenService.GetPrincipalFromExpiredToken(token);
            if (principal == null)
            {
                return Unauthorized("Invalid token");
            }
            
            // Console.WriteLine($"principal.Claims: {string.Join(", ", 
            //     principal.Claims.Select(c => $"{c.Type}: {c.Value}"))}");
            
            // Extract claims from principal
            var userIdClaim = principal.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            var usernameClaim = principal.Claims.FirstOrDefault(c => c.Type == "username")?.Value;
            
            // Check if essential claims are missing or empty
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized("Invalid token: Missing user ID claim");
            }
            if (string.IsNullOrEmpty(usernameClaim))
            {
                return Unauthorized("Invalid token: Missing username claim");
            }
            
            var user = await _accountRepository.GetUserById(int.Parse(userIdClaim));
            if (user == null || !user.UserName.Equals(usernameClaim))
            {
                return Unauthorized("User not found or inactive");
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
            // Log and return unauthorized for token-related exceptions
            // Console.WriteLine($"Token validation error: {ex.Message}");
            return Unauthorized("Invalid token");
        }
        catch (Exception ex)
        {
            // Log and return internal server error for other exceptions
            // Console.WriteLine($"An error occurred: {ex.Message}");
            return StatusCode(500, "Internal server error");
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
