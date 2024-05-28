using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Models.Entities.Powers
{
    public class ImmaterialResourceAmount : ObjectWithId
    {
        public int Count { get; set; }
        public int Level { get; set; }

        //Relationships
        public virtual ImmaterialResourceBlueprint R_Blueprint { get; set; } = null!;
        public int R_BlueprintId { get; set; }
        public virtual ICollection<RaceLevel> R_RaceLevels { get; set; } = [];
        public virtual ICollection<ClassLevel> R_ClassLevels { get; set; } = [];
    }
}