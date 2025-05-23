using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pracadyplomowa.Models.Enums;
using pracadyplomowa.Models.Enums.EffectOptions;

namespace pracadyplomowa.Models.ComplexTypes.Effects
{
    [Owned]
    public class ResistanceEffectType
    {
        public ResistanceEffect ResistanceEffect { get; set; }
        public DamageType ResistanceEffect_DamageType { get; set; }
        public ResistanceEffectType(ResistanceEffectType cloned){
            this.ResistanceEffect = cloned.ResistanceEffect;
            this.ResistanceEffect_DamageType = cloned.ResistanceEffect_DamageType;
        }
        public ResistanceEffectType(){
        }

        public ResistanceEffectType Clone(){
            return new ResistanceEffectType(this);
        }
    }
}