using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Models.Entities.Powers.EffectBlueprints
{
    public class MovementCostEffectBlueprint(string name) : EffectBlueprint(name)
    {
        public MovementCostEffectType MovementCostEffectType{ get; set; } = new MovementCostEffectType();
        private MovementCostEffectBlueprint(): this("EF"){}
        //methods
        public override EffectInstance Generate(Character? roller, Character target){
            return new MovementCostEffectInstance(this, target);
        }
    }
}