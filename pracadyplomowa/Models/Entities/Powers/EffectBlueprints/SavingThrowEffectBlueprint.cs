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
        private SavingThrowEffectBlueprint() : this("EF", 0, 0){}
        public SavingThrowEffectBlueprint(string name, DiceSet value, SavingThrowEffect effect, Ability? ability, Condition? condition, AttackNature? nature) : this(name, value, effect){
            SavingThrowEffectType.SavingThrowEffect_Ability = ability;
            SavingThrowEffectType.SavingThrowEffect_Condition = condition;
            SavingThrowEffectType.SavingThrowEffect_Nature = nature;
        }
        public SavingThrowEffectBlueprint(string name, DiceSet value, SavingThrowEffect effect) : base(name, value){
            SavingThrowEffectType.SavingThrowEffect = effect;
        }
        public SavingThrowEffectType SavingThrowEffectType{ get; set; } = new SavingThrowEffectType();
    }
}