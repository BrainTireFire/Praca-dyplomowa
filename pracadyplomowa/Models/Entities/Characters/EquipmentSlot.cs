using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.Entities.Characters
{
    public class EquipmentSlot : ObjectWithId
    {
        public string Name { get; set; }
        public SlotType Type { get; set; }
    }
}