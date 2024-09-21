using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Models.Entities.Powers.EffectBlueprints
{
    public class MagicEffectBlueprint(string name, DiceSet value) : ValueEffectBlueprint(name, value)
    {
        private MagicEffectBlueprint(): this("EF", 0){}
    }
}