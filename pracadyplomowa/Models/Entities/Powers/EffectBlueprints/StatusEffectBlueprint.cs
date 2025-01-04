using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Models.Entities.Powers.EffectBlueprints
{
    public class StatusEffectBlueprint(string name) : EffectBlueprint(name)
    {
        private StatusEffectBlueprint() : this("EF"){}
        public StatusEffectType StatusEffectType{ get; set; } = new StatusEffectType();
        //methods
        public override EffectInstance Generate(Character? roller, Character target){
            return new StatusEffectInstance(this, target);
        }
    }
}