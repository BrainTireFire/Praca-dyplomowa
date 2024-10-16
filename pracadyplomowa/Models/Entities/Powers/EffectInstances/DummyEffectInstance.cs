using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;

namespace pracadyplomowa.Models.Entities.Powers.EffectInstances
{
    public class DummyEffectInstance : EffectInstance
    {
        private DummyEffectInstance() : base("EF"){}
        public DummyEffectInstance(string name) : base(name){}
        [System.Diagnostics.CodeAnalysis.SetsRequiredMembersAttribute]
        public DummyEffectInstance(DummyEffectBlueprint dummyEffectBlueprint, Character target) : base(dummyEffectBlueprint, target){
        }
        public DummyEffectInstance(DummyEffectInstance effectInstance) : base(effectInstance){
        }
        public override EffectInstance Clone(){
            return new DummyEffectInstance(this);
        }
    }
}