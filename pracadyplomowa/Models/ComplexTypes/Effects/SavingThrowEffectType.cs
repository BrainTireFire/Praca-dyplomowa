#nullable disable

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
        public SavingThrowEffect SavingThrowEffect =>  (SavingThrowEffect)_SavingThrowEffect;
        public Ability SavingThrowEffect_Ability =>  (Ability)_SavingThrowEffect_Ability;
    }
}