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
    public class AbilityEffectType(AbilityEffect abilityEffect, Ability abilityEffect_Ability)
    {
        private readonly AbilityEffect? _AbilityEffect = abilityEffect;
        private readonly Ability? _AbilityEffect_Ability = abilityEffect_Ability;

        public AbilityEffect? AbilityEffect { get => _AbilityEffect != null ? (AbilityEffect)_AbilityEffect : throw new ArgumentNullException();}
        public Ability? AbilityEffect_Ability { get => _AbilityEffect_Ability != null ? (Ability)_AbilityEffect_Ability : throw new ArgumentNullException(); }

    }
}