using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Models.Entities.Characters
{
    public class Race : ObjectWithId
    {
        public Race(string name)
        {
            Name = name;
        }

        public string Name { get; set; } = null!;

        //Relationship
        public virtual ICollection<Character> R_Characters { get; set; } = [];
        public virtual ICollection<RaceLevel> R_RaceLevels { get; set; } = [];
        public virtual ICollection<EquipmentSlot> R_EquipmentSlots { get; set; } = [];
    }
}