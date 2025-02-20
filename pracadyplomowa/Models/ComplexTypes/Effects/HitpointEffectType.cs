using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Enums.EffectOptions;

namespace pracadyplomowa.Models.ComplexTypes.Effects
{
    [Owned]
    public class HitpointEffectType
    {
        public HitpointEffect HitpointEffect { get; set; }
        public HitpointEffectType(HitpointEffectType cloned){
            this.HitpointEffect = cloned.HitpointEffect;
        }
        public HitpointEffectType(){
        }

        public HitpointEffectType Clone(){
            return new HitpointEffectType(this);
        }
    }
}