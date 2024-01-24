using Microsoft.AspNetCore.Identity;

namespace pracadyplomowa;

public class User : IdentityUser<int>
{

    public ICollection<UserRole> UserRoles { get; set; }
}
