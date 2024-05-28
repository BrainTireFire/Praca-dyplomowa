using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Models.Entities.Campaign
{
    public class Board : ObjectWithOwner
    {
        //Properties
        public string Name { get; set; } = null!;

        //Relationship
        //Skoro ma ownera dziedziczonego to chyba relacja z Userem jest niepotrzebna?
        public virtual Encounter? R_Encounter { get; set; }
        public virtual ICollection<Field> R_ConsistsOfFields { get; set; } = [];
    }
}