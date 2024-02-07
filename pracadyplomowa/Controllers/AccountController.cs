using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        return Ok(new UserDto
        {
            Username = user.UserName,
            Token = await _tokenService.CreateToken(user)
        });
    }
}
