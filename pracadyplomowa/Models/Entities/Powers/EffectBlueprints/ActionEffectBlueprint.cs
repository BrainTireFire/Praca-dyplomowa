using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Enums;
using pracadyplomowa.Models.Enums.EffectOptions;

namespace pracadyplomowa.Models.Entities.Powers.EffectBlueprints
{
    public class ActionEffectBlueprint : ValueEffectBlueprint
    {
        public ActionEffectType ActionEffectType{ get; set; } = new ActionEffectType();
        private ActionEffectBlueprint() : this("EF", 0, 0, 0){}
        public ActionEffectBlueprint(string name, DiceSet value, RollMoment rollMoment, ActionEffect effectType) : base(name, value, rollMoment){
            ActionEffectType.ActionEffect = effectType;
        }

        public override EffectInstance Generate(Character roller, Character target){
            return new ActionEffectInstance(this, roller, target);
        }
    }
}