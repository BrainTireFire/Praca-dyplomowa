using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Models.Entities.Characters
{
    public class Race : ObjectWithId
    {
        public string Name { get; set; }

        public virtual ICollection<Character> Characters { get; set; } = [];
        public virtual ICollection<RaceLevel> RaceLevels { get; set; } = [];
        public virtual ICollection<EquipmentSlot> EquipmentSlots { get; set; } = [];
    }
}