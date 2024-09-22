using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;

namespace pracadyplomowa.Models.Entities.Powers.EffectInstances
{
    public class OffHandAttackEffectInstance : EffectInstance
    {
        private OffHandAttackEffectInstance() : base("EF"){}
        public OffHandAttackEffectInstance(string name) : base(name){}
        public OffHandAttackEffectInstance(OffHandAttackEffectBlueprint offHandAttackEffectBlueprint) : base(offHandAttackEffectBlueprint){}
    }
}