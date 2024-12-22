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
        public DiceSet MaximumPreparedSpellsFormula { get; set; } = 0;
        public Ability? SpellcastingAbility { get; set; }

        //Relationships
        public List<Power> R_AccessiblePowers { get; set; } = [];
        public List<PowerSelection> R_PowerSelections { get; set; } = [];
        public List<ClassLevel> R_ClassLevels { get; set; } = [];

        public List<Power> R_UsedForUpcastingOfPowers {get; set;} = [];
    }
}