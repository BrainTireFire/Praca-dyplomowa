using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Enums.EffectOptions;

namespace pracadyplomowa.Models.Entities.Powers.EffectBlueprints
{
    public class ActionEffectBlueprint : ValueEffectBlueprint
    {
        public ActionEffectBlueprint(string name, DiceSet value, ActionEffect effectType) : base(name, value){
            ActionEffectType.ActionEffect = effectType;
        }
        public ActionEffectType ActionEffectType{ get; set; } = new ActionEffectType();
    }
}