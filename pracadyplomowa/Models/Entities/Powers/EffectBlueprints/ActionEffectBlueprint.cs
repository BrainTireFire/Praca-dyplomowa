using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.ComplexTypes.Effects;

namespace pracadyplomowa.Models.Entities.Powers.EffectBlueprints
{
    public class ActionEffectBlueprint(Enums.EffectOptions.ActionEffect actionEffect) : ValueEffectBlueprint
    {
        public ActionEffectType ActionEffectType{ get; set; } = new ActionEffectType(actionEffect);
    }
}