using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Models.Entities.Powers.EffectBlueprints
{
    public class ArmorClassEffectBlueprint : ValueEffectBlueprint
    {
        public ArmorClassEffectBlueprint(string name, DiceSet value) : base(name, value){

        }
    }
}