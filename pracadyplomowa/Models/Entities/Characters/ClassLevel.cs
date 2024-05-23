using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Powers;

namespace pracadyplomowa.Models.Entities.Characters
{
    public class ClassLevel : ObjectWithId
    {
        public int Level { get; set; }
        public DiceSet HitDie { get; set; } = null!;

        // Relationships
        public virtual ICollection<Character> R_Characters { get; set; } = [];
        public virtual ICollection<ChoiceGroup> R_ChoiceGroups { get; set;} = [];
        public virtual ICollection<ImmaterialResourceInstance> R_ImmaterialResources { get; set;} = [];

        public virtual Class R_Class { get; set; } = null!;
        public int ClassId { get; set; }
    }
}