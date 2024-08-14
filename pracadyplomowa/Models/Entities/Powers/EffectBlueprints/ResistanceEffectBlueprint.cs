using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.ComplexTypes.Effects;

namespace pracadyplomowa.Models.Entities.Powers.EffectBlueprints
{
    public class ResistanceEffectBlueprint(Enums.EffectOptions.ResistanceEffect resistanceEffect, Enums.DamageType resistanceEffect_DamageType) : EffectBlueprint
    {
        public ResistanceEffectType ResistanceEffectType{ get; set;} = new ResistanceEffectType(resistanceEffect, resistanceEffect_DamageType);
    }
}