using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.Entities.Powers.EffectBlueprints
{
    public class AbilityEffectBlueprint(string name, DiceSet value, RollMoment rollMoment) : ValueEffectBlueprint(name, value, rollMoment)
    {
        public AbilityEffectType AbilityEffectType{ get; set; } = new AbilityEffectType();
        protected AbilityEffectBlueprint() : this("EF", 0, 0){}

        public override EffectInstance Generate(Character? roller, Character target){
            return new AbilityEffectInstance(this, roller, target);
        }
    }
}