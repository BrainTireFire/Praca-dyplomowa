using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.ComplexTypes.Effects;

namespace pracadyplomowa.Models.Entities.Powers.EffectBlueprints
{
    public class SizeEffectBlueprint(Enums.EffectOptions.SizeEffect sizeEffect, Enums.Size sizeEffect_SizeToSet, int sizeBonus) : EffectBlueprint
    {
        public SizeEffectType SizeEffectType{ get; set; } = new SizeEffectType(sizeEffect, sizeEffect_SizeToSet, sizeBonus);
    }
}