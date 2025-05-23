using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;

namespace pracadyplomowa.Models.Entities.Powers.EffectInstances
{
    public class OffHandAttackEffectInstance : EffectInstance
    {
        protected OffHandAttackEffectInstance() : base("EF"){}
        public OffHandAttackEffectInstance(string name) : base(name){}
        public OffHandAttackEffectInstance(OffHandAttackEffectBlueprint offHandAttackEffectBlueprint, Character target) : base(offHandAttackEffectBlueprint, target){}
        public OffHandAttackEffectInstance(OffHandAttackEffectInstance effectInstance) : base(effectInstance){
        }
        public override EffectInstance Clone(){
            return new OffHandAttackEffectInstance(this);
        }
    }
}