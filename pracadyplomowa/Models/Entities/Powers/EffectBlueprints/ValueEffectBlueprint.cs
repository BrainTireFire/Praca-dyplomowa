using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Models.Entities.Powers.EffectBlueprints
{
    public class ValueEffectBlueprint(string name, DiceSet value) : EffectBlueprint(name)
    {
        public DiceSet DiceSet {get; set;} = value;
    }
}