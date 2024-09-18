using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Enums.EffectOptions;

namespace pracadyplomowa.Models.Entities.Powers.EffectBlueprints
{
    public class AttackPerAttackActionEffectBlueprint : ValueEffectBlueprint
    {
        public AttackPerAttackActionEffectBlueprint(string name, DiceSet value, AttackPerActionEffect effectType) : base(name, value){
            AttackPerAttackActionEffectType.AttackPerActionEffect = effectType;
        }
        public AttackPerAttackActionEffectType AttackPerAttackActionEffectType{get; set; } = new AttackPerAttackActionEffectType();
    }
}