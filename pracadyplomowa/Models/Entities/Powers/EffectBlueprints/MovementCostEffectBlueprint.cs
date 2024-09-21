using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.ComplexTypes.Effects;

namespace pracadyplomowa.Models.Entities.Powers.EffectBlueprints
{
    public class MovementCostEffectBlueprint(string name) : EffectBlueprint(name)
    {
        private MovementCostEffectBlueprint(): this("EF"){}
        public MovementCostEffectType MovementCostEffectType{ get; set; } = new MovementCostEffectType();
    }
}