using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.ComplexTypes.Effects
{
    [Owned]
    public class StatusEffectType
    {
        public Condition StatusEffect { get; set; }
        public StatusEffectType(StatusEffectType cloned){
            this.StatusEffect = cloned.StatusEffect;
        }
        public StatusEffectType(){
        }

        public StatusEffectType Clone(){
            return new StatusEffectType(this);
        }
    }
}