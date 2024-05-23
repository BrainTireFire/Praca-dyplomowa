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
        //Ids and keys
        
        public bool IsEquipped { get; set; }
        public SlotType Type { get; set; }

        //Relationship
        public virtual Character R_Character { get; set; } = null!;
        public int CharacterId { get; set; }

        public virtual Item R_Item { get; set; } = null!;
        public int ItemId { get; set; }
    }
}