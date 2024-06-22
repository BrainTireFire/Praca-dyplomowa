using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Powers;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.Entities.Items
{
    public class Weapon : Item
    {
        public int Range { get; set; }
        public bool LoadedRange { get; set; }
        public bool Finesse { get; set; }
        public WeaponWeight WeaponWeight { get; set; }
        
        //Relationship
        public virtual ICollection<Power> R_PowersCastedOnHit { get; set; } = [];
    }
}