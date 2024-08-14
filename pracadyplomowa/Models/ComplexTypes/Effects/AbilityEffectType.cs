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
    public class AbilityEffectType(AbilityEffect abilityEffect, Ability abilityEffect_Ability)
    {
        private readonly AbilityEffect? _AbilityEffect = abilityEffect;
        private readonly Ability? _AbilityEffect_Ability = abilityEffect_Ability;

        // private AbilityEffect? AbilityEffect_prop { get {return _AbilityEffect;} }
        // private Ability? AbilityEffect_Ability_prop { get {return AbilityEffect_Ability;} }

        // [NotMapped]
        public AbilityEffect AbilityEffect => (AbilityEffect)_AbilityEffect;
        // [NotMapped]
        public Ability AbilityEffect_Ability => (Ability)_AbilityEffect_Ability;
    }
}