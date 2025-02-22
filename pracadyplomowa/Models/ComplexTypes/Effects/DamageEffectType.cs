using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Enums;
using pracadyplomowa.Models.Enums.EffectOptions;

namespace pracadyplomowa.Models.ComplexTypes.Effects
{
    [Owned]
    public class DamageEffectType
    {
        public DamageEffect DamageEffect { get; set; }
        public DamageType? DamageEffect_DamageType { get; set; }

        public DamageEffectType(DamageEffectType cloned){
            this.DamageEffect = cloned.DamageEffect;
            this.DamageEffect_DamageType = cloned.DamageEffect_DamageType;
        }
        public DamageEffectType(){
        }

        public DamageEffectType Clone(){
            return new DamageEffectType(this);
        }
    }
}