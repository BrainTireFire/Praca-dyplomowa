using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;

namespace pracadyplomowa.Models.Entities.Powers.EffectInstances
{
    public class InitiativeEffectInstance : ValueEffectInstance
    {
        protected InitiativeEffectInstance() : base("EF", 0){}
        public InitiativeEffectInstance(string name) : base(name, 0){}
        public InitiativeEffectInstance(InitiativeEffectBlueprint initiativeEffectBlueprint, Character? roller, Character target) : base(initiativeEffectBlueprint, roller, target){
        }
        public InitiativeEffectInstance(InitiativeEffectInstance effectInstance) : base(effectInstance){
        }
        public override EffectInstance Clone(){
            return new InitiativeEffectInstance(this);
        }
    }
}