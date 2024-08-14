using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.ComplexTypes.Effects;

namespace pracadyplomowa.Models.Entities.Powers.EffectBlueprints
{
    public class AbilityEffectBlueprint(Enums.EffectOptions.AbilityEffect abilityEffect, Enums.Ability abilityEffect_Ability) : ValueEffectBlueprint
    {
        public AbilityEffectType AbilityEffectType{ get; set; } = new AbilityEffectType(abilityEffect, abilityEffect_Ability);
    }
}