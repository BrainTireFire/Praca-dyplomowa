using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace pracadyplomowa;

public class AccountRepository : IAccountRepository
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    public AccountRepository(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
    }

    public async Task<IdentityResult> AddUserToRoleAsync(User user, string role)
    {
        return await _userManager.AddToRoleAsync(user, role);
    }

    public async Task<User> GetUserByEmail(string emailAddress)
    {
        return await _userManager.Users.SingleOrDefaultAsync(x => x.Email == emailAddress);
    }
    
    public async Task<User> GetUserById(int userId)
    {
        return await _userManager.Users.SingleOrDefaultAsync(x => x.Id == userId);
    }

    public async Task<User> GetUserByUsername(string username)
    {
        return await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == username.ToLower());
    }

    public async Task<LoginResult> LoginUserAsync(string username, string password)
    {
        var user = await _userManager.FindByNameAsync(username.ToLower());
        if (user == null)
        {
            return new LoginResult { ErrorMessage = "User not found." };
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
        if (!result.Succeeded)
        {
            return new LoginResult { ErrorMessage = "Invalid password." };
        }

        return new LoginResult { User = user };

        //for cookie authentication
        // var user = await _userManager.FindByNameAsync(username.ToLower());
        // if (user == null)
        // {
        //     return SignInResult.Failed;
        // }

        // return await _signInManager.PasswordSignInAsync(username, password, false, false);
    }

    public async Task<(IdentityResult Result, User User)> RegisterUserAsync(RegisterDto registerDto, string password)
    {
        var user = _mapper.Map<User>(registerDto);
        user.UserName = registerDto.Username.ToLower();

        var existingUser = await _userManager.FindByNameAsync(user.UserName);
        if (existingUser != null)
        {
            return (IdentityResult.Failed(new IdentityError { Description = "Username already exists" }), null);
        }

        var result = await _userManager.CreateAsync(user, password);
        return (result, user);
    }
}
