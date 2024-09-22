using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.Entities.Powers.EffectBlueprints
{
    public class ValueEffectBlueprint(string name, DiceSet value, RollMoment rollMoment) : EffectBlueprint(name)
    {
        private ValueEffectBlueprint(): this("EF", 0, 0){}
        public RollMoment RollMoment { get; set; } = rollMoment;
        public DiceSet DiceSet {get; set;} = value;

        public override EffectInstance Generate(Character roller, Character target){
            return new ValueEffectInstance(this, roller, target);
        }
    }
}