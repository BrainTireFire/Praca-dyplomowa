#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Enums;
using pracadyplomowa.Models.Enums.EffectOptions;

namespace pracadyplomowa.Models.ComplexTypes.Effects
{
    [ComplexType]
    public class AbilityEffectType(AbilityEffect AbilityEffect, Ability AbilityEffect_Ability)
    {
        private AbilityEffect? _AbilityEffect = AbilityEffect;
        private Ability? _AbilityEffect_Ability = AbilityEffect_Ability;

        // private AbilityEffect? AbilityEffect_prop { get {return _AbilityEffect;} }
        // private Ability? AbilityEffect_Ability_prop { get {return AbilityEffect_Ability;} }

        // [NotMapped]
        public AbilityEffect AbilityEffect {get {return (AbilityEffect)_AbilityEffect;} private set { _AbilityEffect = value; } }
        // [NotMapped]
        public Ability AbilityEffect_Ability {get {return (Ability)_AbilityEffect_Ability;} private set { _AbilityEffect_Ability = value; } }
    }
}