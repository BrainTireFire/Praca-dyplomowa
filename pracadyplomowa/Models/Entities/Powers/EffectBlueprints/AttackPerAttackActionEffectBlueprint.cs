using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.ComplexTypes.Effects;

namespace pracadyplomowa.Models.Entities.Powers.EffectBlueprints
{
    public class AttackPerAttackActionEffectBlueprint : ValueEffectBlueprint
    {
        public AttackPerAttackActionEffectType AttackPerAttackActionEffectType{get; set; } = null!;
    }
}