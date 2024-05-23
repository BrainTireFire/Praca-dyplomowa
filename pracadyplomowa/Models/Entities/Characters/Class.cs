using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Powers;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.Entities.Characters
{
    public class Class : ObjectWithId
    {
        //Properties
        public string Name { get; set; } = null!;
        public string? MaximumPreparedSpellsFormula { get; set; }
        public Ability? SpellcastingAbility { get; set; }

        //Relationships
        public ICollection<Power> R_AccessiblePowers { get; set; } = [];
        public ICollection<ClassLevel> R_ClassLevels { get; set; } = [];
    }
}