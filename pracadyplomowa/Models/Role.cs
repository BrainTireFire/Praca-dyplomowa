using Microsoft.AspNetCore.Identity;

namespace pracadyplomowa;

public class Role : IdentityRole<int>
{
    public virtual ICollection<UserRole> UserRoles { get; set; }
}
