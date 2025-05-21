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
    public class SavingThrowEffectType
    {
        public SavingThrowEffect SavingThrowEffect { get; set; }
        public Ability? SavingThrowEffect_Ability { get; set; } //only this ability type. null if all
        // public Condition? SavingThrowEffect_Condition { get; set; } //applies against this condition only. null if no restriction
        // public AttackNature? SavingThrowEffect_Nature { get; set; } //applies only against attack of this nature. null if all
        public SavingThrowEffectType(SavingThrowEffectType cloned){
            this.SavingThrowEffect = cloned.SavingThrowEffect;
            this.SavingThrowEffect_Ability = cloned.SavingThrowEffect_Ability;
            // this.SavingThrowEffect_Condition = cloned.SavingThrowEffect_Condition;
            // this.SavingThrowEffect_Nature = cloned.SavingThrowEffect_Nature;
        }
        public SavingThrowEffectType(){
        }

        public SavingThrowEffectType Clone(){
            return new SavingThrowEffectType(this);
        }
    }
}