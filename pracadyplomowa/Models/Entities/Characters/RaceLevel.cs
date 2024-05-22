using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Models.Entities.Characters
{
    public class RaceLevel : ObjectWithId
    {
        public int Level { get; set; }

        // Relationships
        public virtual Race R_LevelsForRace { get; set; }
    }
}