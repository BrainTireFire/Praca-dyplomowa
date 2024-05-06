using Microsoft.AspNetCore.Identity;
using pracadyplomowa.Models.Entities;

namespace pracadyplomowa;

public class User : IdentityUser<int>
{

    public ICollection<UserRole> UserRoles { get; set; }

    public ICollection<ObjectWithOwner> Objects{ get; set; }
}
