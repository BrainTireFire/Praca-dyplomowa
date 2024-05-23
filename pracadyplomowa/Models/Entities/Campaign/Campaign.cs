using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Models.Entities.Campaign
{
    public class Campaign : ObjectWithOwner
    {
        public string Name { get; set; }

        //Relationship
        public virtual User R_UserOwnsCampaign { get; set; }
        public virtual ICollection<Character> R_CampaigHasCharacters { get; set; } = [];
        public virtual ICollection<Encounter> R_CampainHasEncounters { get; set; } = [];
        public virtual ICollection<Shop> R_CampaingHasShops { get; set; } = [];
        public virtual ICollection<User> R_UsersAttendsToCampaings { get; set; } = [];
    }
}