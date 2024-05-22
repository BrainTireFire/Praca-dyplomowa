using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Items;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.Entities.Characters
{
    public class EquipData : ObjectWithId
    {
        public bool IsEquipped { get; set; }
        public SlotType Type { get; set; }

        //Relationship
        public virtual Character R_BackupOfCharacter { get; set; }

        public virtual ICollection<Item> R_EquippedItems { get; set; } = [];
    }
}