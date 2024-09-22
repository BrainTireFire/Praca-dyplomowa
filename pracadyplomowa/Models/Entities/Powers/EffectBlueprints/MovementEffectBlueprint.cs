using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.Entities.Powers.EffectBlueprints
{
    public class MovementEffectBlueprint(string name, DiceSet value, RollMoment rollMoment) : ValueEffectBlueprint(name, value, rollMoment)
    {
        private MovementEffectBlueprint(): this("EF", 0, 0){}
        public MovementEffectType MovementEffectType{ get; set; } = new MovementEffectType();
        //methods
        public override EffectInstance Generate(Character roller, Character target){
            return new MovementEffectInstance(this, roller, target);
        }
    }
}