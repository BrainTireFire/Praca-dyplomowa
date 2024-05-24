using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Models.Entities.Campaign
{
    public class Campaign : ObjectWithOwner
    {
        public string Name { get; set; } = null!;

        //Relationship
        public virtual ICollection<Character> R_CampaignHasCharacters { get; set; } = [];
        public virtual ICollection<Encounter> R_CampaignHasEncounters { get; set; } = [];
        public virtual ICollection<Shop> R_CampaignHasShops { get; set; } = [];
        public virtual ICollection<User> R_UsersAttendsCampaigns { get; set; } = [];
        public virtual ICollection<ActionLog> R_Log { get; set; } = [];
    }
}