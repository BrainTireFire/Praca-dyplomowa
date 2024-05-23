using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Models.Entities.Characters
{
    public class Race : ObjectWithId
    {
        public string Name { get; set; }

        //Relationship
        public virtual ICollection<Character> R_RaceBelongsToCharacter { get; set; } = [];
        public virtual ICollection<RaceLevel> R_RaceConsisstsOfLevels { get; set; } = [];
        public virtual ICollection<EquipmentSlot> R_RaceHasEquipmentSlots { get; set; } = [];
    }
}