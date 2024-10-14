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
        public List<EquipmentSlot> R_Slots { get; set; } = [];

        //Relationship
        public virtual Character R_Character { get; set; } = null!;
        public int R_CharacterId { get; set; }

        public virtual Item R_Item { get; set; } = null!;
        public int R_ItemId { get; set; }
    }
}