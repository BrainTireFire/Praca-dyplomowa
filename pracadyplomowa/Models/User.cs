using Microsoft.AspNetCore.Identity;
using pracadyplomowa.Models.Entities;
using pracadyplomowa.Models.Entities.Campaign;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa;

public class User : IdentityUser<int>
{
    public ICollection<UserRole> UserRoles { get; set; }
    public ICollection<ObjectWithOwner> R_Objects { get; set; } = [];
    
    public virtual ICollection<Character> R_UserHasCharacters { get; set; } = [];
    public virtual ICollection<Campaign> R_UserAttendsAsPlayerToCamgains { get; set; } = [];
    public virtual ICollection<Campaign> R_UserOwnsCampaigns { get; set; } = [];
}
