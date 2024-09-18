using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Enums;
using pracadyplomowa.Models.Enums.EffectOptions;

namespace pracadyplomowa.Models.Entities.Powers.EffectBlueprints
{
    public class ResistanceEffectBlueprint : EffectBlueprint
    {
        public ResistanceEffectType ResistanceEffectType{ get; set;} = new ResistanceEffectType();

        public ResistanceEffectBlueprint(string name, ResistanceEffect resistanceEffect, DamageType damageType) : base(name){
            ResistanceEffectType.ResistanceEffect = resistanceEffect;
            ResistanceEffectType.ResistanceEffect_DamageType = damageType;
        }
    }
}