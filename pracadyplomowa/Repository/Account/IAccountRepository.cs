﻿using Microsoft.AspNetCore.Identity;

namespace pracadyplomowa;

public interface IAccountRepository
{
    Task<User> GetUserByUsername(string username);
    Task<User> GetUserByEmail(string emailAddress);
    Task<User> GetUserById(int userId);
    Task<User> GetUserWithAttendCampaignById(int userId);
    Task<IList<string>> GetUserRoles(User user);
    Task<(IdentityResult Result, User User)> RegisterUserAsync(RegisterDto registerDto, string password);
    Task<LoginResult> LoginUserAsync(string username, string password);
    Task<IdentityResult> AddUserToRoleAsync(User user, string role);
}
