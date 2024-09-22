using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Models.Entities.Powers.EffectBlueprints
{
    public class ArmorClassEffectBlueprint(string name, DiceSet value) : ValueEffectBlueprint(name, value)
    {
        private ArmorClassEffectBlueprint(): this("EF", 0){}

        public override EffectInstance Generate(Character roller){
            return new ArmorClassEffectInstance(this, roller);
        }
    }
}