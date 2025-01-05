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
    public class AttackRollEffectBlueprint : ValueEffectBlueprint
    {
        public AttackRollEffectType AttackRollEffectType{ get; set; } = new AttackRollEffectType();
        private AttackRollEffectBlueprint(): this("EF", 0, 0, 0, 0, 0){}
        public AttackRollEffectBlueprint(string name, DiceSet value, RollMoment rollMoment, AttackRollEffect_Type effectType, AttackRollEffect_Source effectSource, AttackRollEffect_Range effectRange) : base(name, value, rollMoment){
            AttackRollEffectType.AttackRollEffect_Type = effectType;
            AttackRollEffectType.AttackRollEffect_Source = effectSource;
            AttackRollEffectType.AttackRollEffect_Range = effectRange;
        }
        //methods
        public override EffectInstance Generate(Character? roller, Character target){
            return new AttackRollEffectInstance(this, roller, target);
        }

    }
}