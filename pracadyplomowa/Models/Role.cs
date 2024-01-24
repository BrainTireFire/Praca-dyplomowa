using Microsoft.AspNetCore.Identity;

namespace pracadyplomowa;

public class Role : IdentityRole<int>
{
    public ICollection<UserRole> UserRoles { get; set; }
}
