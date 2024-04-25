using Microsoft.AspNetCore.Identity;
using pracadyplomowa.Models;

namespace pracadyplomowa;

public class User : IdentityUser<int>
{

    public ICollection<UserRole> UserRoles { get; set; }

    public ICollection<ObjectWithOwner> Objects{ get; set; }
}
