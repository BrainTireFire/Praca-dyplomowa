using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Enums;
using pracadyplomowa.Models.Enums.EffectOptions;

namespace pracadyplomowa.Models.ComplexTypes.Effects
{
    [ComplexType]
    public class SavingThrowEffectType(SavingThrowEffect savingThrowEffect, Ability savingThrowEffect_Ability)
    {
        private readonly SavingThrowEffect? _SavingThrowEffect = savingThrowEffect;
        private readonly Ability? _SavingThrowEffect_Ability = savingThrowEffect_Ability;
        public SavingThrowEffect? SavingThrowEffect { get => _SavingThrowEffect == null ? _SavingThrowEffect : throw new ArgumentNullException(); }
        public Ability? SavingThrowEffect_Ability { get => _SavingThrowEffect_Ability == null ? _SavingThrowEffect_Ability : throw new ArgumentNullException(); }
    }
}