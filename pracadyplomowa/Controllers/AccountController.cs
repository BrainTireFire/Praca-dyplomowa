using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace pracadyplomowa;

public class AccountController : BaseApiController
{
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService, IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if (await UserExists(registerDto.Username))
        {
            return BadRequest("Username already exists on this username: " + registerDto.Username);
        }

        var user = _mapper.Map<User>(registerDto);

        user.UserName = registerDto.Username.ToLower();

        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        var roleResult = await _userManager.AddToRoleAsync(user, "User");

        if (!roleResult.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return new UserDto
        {
            Username = user.UserName,
            Token = await _tokenService.CreateToken(user)
        };
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

        if (user == null)
        {
            return Unauthorized("Username or password is incorrect.");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

        if (!result.Succeeded)
        {
            return Unauthorized();
        }

        return new UserDto
        {
            Username = user.UserName,
            Token = await _tokenService.CreateToken(user)
        };
    }

    private async Task<bool> UserExists(string username)
    {
        return await _userManager.Users.AnyAsync(user => user.UserName == username.ToLower());
    }
}
