using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Models.Entities.Powers.EffectBlueprints
{
    public class ValueEffectBlueprint : EffectBlueprint
    {
        public DiceSet diceSet {get; set;} = null!;
    }
}