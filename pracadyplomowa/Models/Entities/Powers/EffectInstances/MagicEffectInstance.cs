using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Models.Entities.Powers.EffectBlueprints
{
    public class MagicEffectInstance : ValueEffectInstance
    {
        private MagicEffectInstance() : base("EF", 0){}
        public MagicEffectInstance(string name, DiceSet diceSet) : base(name, diceSet){}
        public MagicEffectInstance(MagicEffectBlueprint magicEffectBlueprint, Character roller, Character target) : base(magicEffectBlueprint, roller, target){
        }
        public MagicEffectInstance(MagicEffectInstance effectInstance) : base(effectInstance){
        }
        public override EffectInstance Clone(){
            return new MagicEffectInstance(this);
        }
    }
}