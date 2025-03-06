using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Enums;
using pracadyplomowa.Models.Enums.EffectOptions;

namespace pracadyplomowa.Models.Entities.Powers.EffectBlueprints
{
    public class DamageEffectBlueprint(string name, DiceSet value, RollMoment rollMoment) : ValueEffectBlueprint(name, value, rollMoment)
    {
        public DamageEffectBlueprint(string name, DiceSet value, RollMoment rollMoment, DamageEffect damageEffect, DamageType? damageType) : this(name, value, rollMoment){
            DamageEffectType.DamageEffect = damageEffect;
            DamageEffectType.DamageEffect_DamageType = damageType;
        }
        public DamageEffectType DamageEffectType{ get; set;} = new DamageEffectType();
        protected DamageEffectBlueprint(): this("EF", 0, 0){}
        //methods
        public override EffectInstance Generate(Character? roller, Character target){
            EffectInstance instance = new DamageEffectInstance(this, roller, target);
            return instance;
        }
    }
}