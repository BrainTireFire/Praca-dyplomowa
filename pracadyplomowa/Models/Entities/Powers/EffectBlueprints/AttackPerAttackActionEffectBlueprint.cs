using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Enums;
using pracadyplomowa.Models.Enums.EffectOptions;

namespace pracadyplomowa.Models.Entities.Powers.EffectBlueprints
{
    public class AttackPerAttackActionEffectBlueprint : ValueEffectBlueprint
    {
        //props
        public AttackPerAttackActionEffectType AttackPerAttackActionEffectType{get; set; } = new AttackPerAttackActionEffectType();
        //constructors
        private AttackPerAttackActionEffectBlueprint(): this("EF", 0, 0, 0){}
        public AttackPerAttackActionEffectBlueprint(string name, DiceSet value, RollMoment rollMoment, AttackPerActionEffect effectType) : base(name, value, rollMoment){
            AttackPerAttackActionEffectType.AttackPerActionEffect = effectType;
        }
        //methods
        public override EffectInstance Generate(Character roller, Character target){
            return new AttackPerAttackActionEffectInstance(this, roller, target);
        }
    }
}