using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers.EffectInstances;

namespace pracadyplomowa.Models.Entities.Powers.EffectBlueprints
{
    public class DummyEffectBlueprint(string name) : EffectBlueprint(name)
    {
        public override EffectInstance Generate(Character? roller, Character target){
            return new DummyEffectInstance(this, target);
        }
    }
}