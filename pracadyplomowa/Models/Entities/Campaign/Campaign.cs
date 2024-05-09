using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Models.Entities.Campaign
{
    public class Campaign : ObjectWithOwner
    {
        public virtual User UserOwner { get; set; }

        public virtual ICollection<Character> Characters { get; set; } = [];
        public virtual ICollection<Encounter> Encounters { get; set; } = [];
        public virtual ICollection<Shop> Shops { get; set; } = [];
        public virtual ICollection<User> UsersAttenders { get; set; } = [];
    }
}