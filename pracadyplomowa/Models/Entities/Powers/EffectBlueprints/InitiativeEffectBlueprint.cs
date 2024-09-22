using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Models.Entities.Powers.EffectBlueprints
{
    public class InitiativeEffectBlueprint(string name, DiceSet value) : ValueEffectBlueprint(name, value)
    {
        private InitiativeEffectBlueprint(): this("EF", 0){}
    }
}