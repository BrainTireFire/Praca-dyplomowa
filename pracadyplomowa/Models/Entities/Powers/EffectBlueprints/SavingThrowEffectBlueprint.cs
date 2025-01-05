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
    public class SavingThrowEffectBlueprint : ValueEffectBlueprint
    {
        private SavingThrowEffectBlueprint() : this("EF", 0, 0, 0){}
        public SavingThrowEffectBlueprint(string name, DiceSet value, RollMoment rollMoment, SavingThrowEffect effect, Ability? ability) : this(name, value, rollMoment, effect){
            SavingThrowEffectType.SavingThrowEffect_Ability = ability;
            // SavingThrowEffectType.SavingThrowEffect_Condition = condition;
            // SavingThrowEffectType.SavingThrowEffect_Nature = nature;
        }
        public SavingThrowEffectBlueprint(string name, DiceSet value, RollMoment rollMoment, SavingThrowEffect effect) : base(name, value,rollMoment){
            SavingThrowEffectType.SavingThrowEffect = effect;
        }
        public SavingThrowEffectType SavingThrowEffectType{ get; set; } = new SavingThrowEffectType();
        //methods
        public override EffectInstance Generate(Character? roller, Character target){
            return new SavingThrowEffectInstance(this, roller, target);
        }
    }
}