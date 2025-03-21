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
    public class SizeEffectType
    {
        public SizeEffect SizeEffect { get; set; }
        public Size SizeEffect_SizeToSet { get; set; }
        public SizeEffectType(SizeEffectType cloned){
            this.SizeEffect = cloned.SizeEffect;
            this.SizeEffect_SizeToSet = cloned.SizeEffect_SizeToSet;
        }
        public SizeEffectType(){
        }

        public SizeEffectType Clone(){
            return new SizeEffectType(this);
        }
    }
}