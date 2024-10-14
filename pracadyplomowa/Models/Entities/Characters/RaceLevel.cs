using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Powers;

namespace pracadyplomowa.Models.Entities.Characters
{
    public class RaceLevel : ObjectWithId
    {

        public int Level { get; set; }

        // Relationships
        public virtual required Race R_Race { get; set; }
        public int R_RaceId { get; set; }

        public virtual ICollection<ChoiceGroup> R_ChoiceGroups { get; set; } = [];
        public virtual ICollection<ImmaterialResourceAmount> R_ImmaterialResourceAmounts { get; set; } = [];
    }
}