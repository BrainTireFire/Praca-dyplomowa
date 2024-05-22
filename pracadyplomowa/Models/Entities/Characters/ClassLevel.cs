using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Models.Entities.Characters
{
    public class ClassLevel : ObjectWithId
    {
        public int Level { get; set; }
        public DiceSet HitDie { get; set; }

        // Relationships
        public virtual ICollection<Character> R_ClassLevelInCharacters { get; set; } = [];
        public virtual ICollection<ChoiceGroup> R_ChoiceGroups { get; set;} = [];
    }
}