using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.ComplexTypes.Effects;

namespace pracadyplomowa.Models.Entities.Powers.EffectBlueprints
{
    public class AttackRollEffectBlueprint(Enums.EffectOptions.AttackRollEffect_Range attackRollEffect_Range, Enums.EffectOptions.AttackRollEffect_Source attackRollEffect_Source, Enums.EffectOptions.AttackRollEffect_Type attackRollEffect_Type) : ValueEffectBlueprint
    {
        public AttackRollEffectType AttackRollEffectType{ get; set; } = new AttackRollEffectType(attackRollEffect_Range, attackRollEffect_Source, attackRollEffect_Type);
    }
}