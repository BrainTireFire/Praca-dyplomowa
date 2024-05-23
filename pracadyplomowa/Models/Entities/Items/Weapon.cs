using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Powers;

namespace pracadyplomowa.Models.Entities.Items
{
    public class Weapon : Item
    {
        //Ids and keys
        public int ItemIsWeaponId { get; set; }
        
        public bool Finesse { get; set; }
        public bool Heavy { get; set; }
        public bool Light { get; set; }

        //Relationship
        public virtual Item R_ItemIsWeapon { get; set; }

        public virtual Power R_PowerCastedOnHit { get; set; }

        
    }
}