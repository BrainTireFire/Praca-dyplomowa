using Microsoft.AspNetCore.Identity;
using pracadyplomowa.Models.Entities;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa;

public class User : IdentityUser<int>
{
    public ICollection<UserRole> UserRoles { get; set; }

    public ICollection<ObjectWithOwner> Objects { get; set; }
    public virtual ICollection<Character> Characters { get; set; } = [];
}
