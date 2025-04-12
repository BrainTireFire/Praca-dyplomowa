using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Errors;
using pracadyplomowa.Repository.User;

namespace pracadyplomowa.Services.User;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<pracadyplomowa.User> _passwordHasher;
    
    public UserService(IUserRepository userRepository, IPasswordHasher<pracadyplomowa.User> passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }
    
    public async Task<ActionResult?> UpdateUserEmailAsync(int userId, string newEmail)
    {
        if (await _userRepository.EmailExistsAsync(newEmail))
            return new BadRequestObjectResult(new ApiResponse(400, $"Email {newEmail} is already taken."));
    
        var user = _userRepository.GetById(userId);
        if (user == null)
        {
            return new NotFoundObjectResult(new ApiResponse(404, $"User with id {userId} not found."));
        }

        user.UpdateEmail(newEmail);
        _userRepository.Update(user);
        await _userRepository.SaveChanges();

        return null;
    }


    public async Task<ActionResult?> UpdateUsernameAsync(int userId, string newUsername)
    {
        if (await _userRepository.UsernameExistsAsync(newUsername))
            return new BadRequestObjectResult(new ApiResponse(400, $"Username {newUsername} is already taken."));
        
        var user = _userRepository.GetById(userId);
        if (user == null)
        {
            return new NotFoundObjectResult(new ApiResponse(404, $"User with id {userId} not found."));
        }

        user.UpdateUsername(newUsername);
        _userRepository.Update(user);
        await _userRepository.SaveChanges();
        
        return null;
    }

    public async Task<ActionResult?> UpdatePasswordAsync(int userId, string newPassword)
    {
        var user = _userRepository.GetById(userId);
        if (user == null)
        {
            return new NotFoundObjectResult(new ApiResponse(404, $"User with id {userId} not found."));
        }

        user.UpdatePassword(newPassword, _passwordHasher);
        _userRepository.Update(user);
        await _userRepository.SaveChanges();

        return null;
    }

    public async Task<ActionResult?> DeleteUserAsync(int userId)
    {
        var user = _userRepository.GetById(userId);
        if (user == null)
        {
            return new NotFoundObjectResult(new ApiResponse(404, $"User with id {userId} not found."));
        }

        user.IsDeleted = true;
        _userRepository.Update(user);
        await _userRepository.SaveChanges();
        
        return null;
    }
}