using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using pracadyplomowa.Models.DTOs;

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

        return Ok(new UserDto
        {
            Username = user.UserName,
            Token = await _tokenService.CreateToken(user)
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

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = false, // Set to true in production
            SameSite = SameSiteMode.Lax, // Set to SameSiteMode.Strict in production
            Expires = DateTime.UtcNow.AddDays(1)
        };

        Response.Cookies.Append("JwtCookie", token, cookieOptions);

        return Ok(new UserDto
        {
            Username = user.UserName,
            Token = token
        });
    }
    
    [HttpPost("validate-token")]
    public ActionResult ValidateToken()
    {
        var token = Request.Cookies["JwtCookie"];
        if (string.IsNullOrEmpty(token))
        {
            return BadRequest("Token is required");
        }
    
        var isValid = _tokenService.ValidateToken(token);
    
        if (!isValid)
        {
            return Unauthorized();
        }
    
        return Ok(new { isValid = true });
    }

    [HttpPost("validate-token2")]
    public async Task<ActionResult> ValidateToken2()
    {
        var token = Request.Cookies["JwtCookie"];
        if (string.IsNullOrEmpty(token))
        {
            return BadRequest("Token is required");
        }

        try
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(token);
            if (principal == null)
            {
                return Unauthorized("Invalid token 1");
            }
            
            // Console.WriteLine($"principal.Claims: {string.Join(", ", 
            //     principal.Claims.Select(c => $"{c.Type}: {c.Value}"))}");
            
            var userIdClaim = principal.Claims.FirstOrDefault(c => c.Type == "id");
            if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
            {
                return Unauthorized("Invalid token: Missing user ID claim");
            }          
            
            var usernameClaim = principal.Claims.FirstOrDefault(c => c.Type == "id");
            if (usernameClaim == null || string.IsNullOrEmpty(usernameClaim.Value))
            {
                return Unauthorized("Invalid token: Missing username claim");
            }
            
            var user = await _accountRepository.GetUserById(int.Parse(userIdClaim.Value));
            if (user == null && user.UserName.Equals(usernameClaim.Value))
            {
                return Unauthorized("User not found or inactive");
            }
            
            return Ok(new { isValid = true });
        }
        catch (SecurityTokenException ex)
        {
            // Log detailed error information
            Console.WriteLine($"Token validation error: {ex.Message}");
            return Unauthorized("Invalid token 4");
        }
        catch (Exception ex)
        {
            // Log other exceptions
            Console.WriteLine($"An error occurred: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }
}
