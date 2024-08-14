using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.ComplexTypes.Effects;

namespace pracadyplomowa.Models.Entities.Powers.EffectBlueprints
{
    public class MovementEffectBlueprint(Enums.EffectOptions.MovementEffect movementEffect) : ValueEffectBlueprint
    {
        public MovementEffectType MovementEffectType{ get; set; } = new MovementEffectType(movementEffect);
    }
}