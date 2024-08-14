using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.ComplexTypes.Effects;

namespace pracadyplomowa.Models.Entities.Powers.EffectBlueprints
{
    public class SavingThrowEffectBlueprint(Enums.EffectOptions.SavingThrowEffect savingThrowEffect, Enums.Ability savingThrowEffect_Ability) : ValueEffectBlueprint
    {
        public SavingThrowEffectType SavingThrowEffectType{ get; set; } = new SavingThrowEffectType(savingThrowEffect, savingThrowEffect_Ability);
    }
}