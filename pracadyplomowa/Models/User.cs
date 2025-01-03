﻿using Microsoft.AspNetCore.Identity;
using pracadyplomowa.Models.Entities;
using pracadyplomowa.Models.Entities.Campaign;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa;

public class User : IdentityUser<int>
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public ICollection<UserRole> UserRoles { get; set; }
    public ICollection<ObjectWithOwner> R_Objects { get; set; } = [];

    public virtual ICollection<Character> R_UserHasCharacters { get; set; } = [];
    public virtual ICollection<Campaign> R_UserAttendsAsPlayerToCamgains { get; set; } = [];
}
