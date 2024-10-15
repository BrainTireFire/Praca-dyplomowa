using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;

namespace pracadyplomowa.Models.Entities.Powers.EffectInstances
{
    public class HealingEffectInstance : ValueEffectInstance
    {
        private HealingEffectInstance() : base("EF", 0){}
        public HealingEffectInstance(string name) : base(name, 0){}
        public HealingEffectInstance(HealingEffectBlueprint initiativeEffectBlueprint, Character roller, Character target) : base(initiativeEffectBlueprint, roller, target){
        }
        public HealingEffectInstance(HealingEffectInstance effectInstance) : base(effectInstance){
        }
        public override EffectInstance Clone(){
            return new HealingEffectInstance(this);
        }
    }
}