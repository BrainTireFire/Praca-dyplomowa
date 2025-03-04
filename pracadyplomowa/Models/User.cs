using Microsoft.AspNetCore.Identity;
using pracadyplomowa.Models.Entities;
using pracadyplomowa.Models.Entities.Campaign;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa;

public class User : IdentityUser<int>
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public virtual ICollection<UserRole> UserRoles { get; set; }
    public virtual ICollection<ObjectWithOwner> R_Objects { get; set; } = [];

    public virtual ICollection<Character> R_UserHasCharacters { get; set; } = [];
    public virtual ICollection<Campaign> R_UserAttendsAsPlayerToCamgains { get; set; } = [];

    public void UpdateEmail(string newEmail)
    {
        if (string.IsNullOrWhiteSpace(newEmail))
            throw new ArgumentException("Email cannot be empty.");

        Email = newEmail;
        NormalizedEmail = newEmail.ToUpper();
    }
    
    public void UpdateUsername(string newUsername)
    {
        if (string.IsNullOrWhiteSpace(newUsername))
            throw new ArgumentException("Username cannot be empty.");

        UserName = newUsername;
        NormalizedUserName = newUsername.ToUpper();
    }
    
    public void UpdatePassword(string newPassword, IPasswordHasher<User> passwordHasher)
    {
        if (string.IsNullOrWhiteSpace(newPassword))
            throw new ArgumentException("Password cannot be empty.");

        PasswordHash = passwordHasher.HashPassword(this, newPassword);
    }
}