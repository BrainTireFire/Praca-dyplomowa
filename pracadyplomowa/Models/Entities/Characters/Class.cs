using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Powers;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.Entities.Characters
{
    public class Class(string name) : ObjectWithId
    {

        //Properties
        public string Name { get; set; } = name;
        // public string? MaximumPreparedSpellsFormula { get; set; }
        public virtual DiceSet MaximumPreparedSpellsFormula { get; set; } = 0;
        public Ability? SpellcastingAbility { get; set; }

        //Relationships
        public virtual List<Power> R_AccessiblePowers { get; set; } = [];
        public virtual List<PowerSelection> R_PowerSelections { get; set; } = [];
        public virtual List<ClassLevel> R_ClassLevels { get; set; } = [];

        public virtual List<Power> R_UsedForUpcastingOfPowers {get; set;} = [];
    }
}