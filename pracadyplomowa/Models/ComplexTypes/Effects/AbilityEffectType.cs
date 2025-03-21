using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Enums;
using pracadyplomowa.Models.Enums.EffectOptions;

namespace pracadyplomowa.Models.ComplexTypes.Effects
{
    [Owned]
    public class AbilityEffectType
    {
        public AbilityEffect AbilityEffect { get; set; }
        public Ability AbilityEffect_Ability { get; set; }

        public AbilityEffectType(AbilityEffectType cloned){
            this.AbilityEffect = cloned.AbilityEffect;
            this.AbilityEffect_Ability = cloned.AbilityEffect_Ability;
        }
        public AbilityEffectType(){
        }

        public AbilityEffectType Clone(){
            return new AbilityEffectType(this);
        }
    }
}