using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Powers;

namespace pracadyplomowa.Models.Entities.Items
{
    public class Weapon : Item
    {
        
        public bool Finesse { get; set; }
        public bool Heavy { get; set; }
        public bool Light { get; set; }

        //Relationship

        public virtual ICollection<Power> R_PowersCastedOnHit { get; set; } = [];

        
    }
}