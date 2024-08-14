using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.ComplexTypes.Effects;

namespace pracadyplomowa.Models.Entities.Powers.EffectBlueprints
{
    public class DamageEffectBlueprint(Enums.EffectOptions.DamageEffect damageEffect, Enums.DamageType damageEffect_DamageType) : ValueEffectBlueprint
    {
        public DamageEffectType DamageEffectType{ get; set;} = new DamageEffectType(damageEffect, damageEffect_DamageType);
    }
}