using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Powers;

namespace pracadyplomowa.Models.Entities.Characters
{
    public class ClassLevel(int Level) : ObjectWithId
    {
        public int Level { get; set; } = Level;
        public DiceSet HitDie { get; set; } = new DiceSet();
        public int HitPoints { get; set; }

        // Relationships
        public virtual ICollection<Character> R_Characters { get; set; } = [];
        public virtual List<ChoiceGroup> R_ChoiceGroups { get; set; } = [];

        public virtual Class R_Class { get; set; } = null!;
        public int R_ClassId { get; set; }
    }
}