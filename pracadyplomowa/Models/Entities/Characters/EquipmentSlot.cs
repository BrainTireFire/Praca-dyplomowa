using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Items;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.Entities.Characters
{
    public class EquipmentSlot : ObjectWithId
    {
        public string Name { get; set; }
        public SlotType Type { get; set; }

        //Relationship
        public virtual ICollection<Race> R_SlotForRaces { get; set; } = [];
        public virtual ICollection<Item> R_SlotCanEquipItem { get; set; } = [];
    }
}