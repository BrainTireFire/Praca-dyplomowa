using Microsoft.AspNetCore.Mvc;

namespace pracadyplomowa.Services.User;

public interface IUserService
{
    Task<ActionResult?> UpdateUserEmailAsync(int userId, string newEmail);
    Task<ActionResult?> UpdateUsernameAsync(int userId, string newUsername);
    Task<ActionResult?> UpdatePasswordAsync(int userId, string newPassword);
}