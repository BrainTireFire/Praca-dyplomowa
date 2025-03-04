using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers.EffectInstances;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.Entities.Powers.EffectBlueprints
{
    public class InitiativeEffectBlueprint(string name, DiceSet value, RollMoment rollMoment) : ValueEffectBlueprint(name, value, rollMoment)
    {
        protected InitiativeEffectBlueprint(): this("EF", 0, 0){}
        //methods
        public override EffectInstance Generate(Character? roller, Character target){
            return new InitiativeEffectInstance(this, roller, target);
        }
    }
}