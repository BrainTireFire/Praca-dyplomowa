using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.Entities.Powers.EffectBlueprints
{
    public abstract class ValueEffectBlueprint(string name, DiceSet value, RollMoment rollMoment) : EffectBlueprint(name)
    {
        private ValueEffectBlueprint(): this("EF", 0, 0){}
        public RollMoment RollMoment { get; set; } = rollMoment;
        public DiceSet DiceSet {get; set;} = value;
        public int DiceSetId {get; set;} = value.Id;

        // public abstract EffectInstance Generate(Character? roller, Character target);
        // //     return new ValueEffectInstance(this, roller, target);
        // // }
    }
}