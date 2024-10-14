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
        public string Name { get; set; } = null!;
        public SlotType Type { get; set; }
        public List<EquipData> Usages { get; set; } = [];

        //Relationship
        public virtual ICollection<Race> R_Races { get; set; } = [];
        public virtual ICollection<Item> R_Items { get; set; } = [];
    }
}