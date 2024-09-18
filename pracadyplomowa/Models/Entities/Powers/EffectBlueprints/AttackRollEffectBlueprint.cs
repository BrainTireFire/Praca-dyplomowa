using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Enums.EffectOptions;

namespace pracadyplomowa.Models.Entities.Powers.EffectBlueprints
{
    public class AttackRollEffectBlueprint : ValueEffectBlueprint
    {
        public AttackRollEffectBlueprint(string name, DiceSet value, AttackRollEffect_Type effectType, AttackRollEffect_Source effectSource, AttackRollEffect_Range effectRange) : base(name, value){
            AttackRollEffectType.AttackRollEffect_Type = effectType;
            AttackRollEffectType.AttackRollEffect_Source = effectSource;
            AttackRollEffectType.AttackRollEffect_Range = effectRange;
        }

        public AttackRollEffectType AttackRollEffectType{ get; set; } = new AttackRollEffectType();
    }
}