using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.ComplexTypes.Effects;

namespace pracadyplomowa.Models.Entities.Powers.EffectBlueprints
{
    public class StatusEffectBlueprint(Enums.Condition statusEffect) : EffectBlueprint
    {
        public StatusEffectType StatusEffectType{ get; set; } = new StatusEffectType(statusEffect);
    }
}