using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pracadyplomowa.Models.Enums.EffectOptions;

namespace pracadyplomowa.Models.ComplexTypes.Effects
{
    [Owned]
    public class LanguageEffectType
    {
        
        public LanguageEffectType(LanguageEffectType cloned){
        }
        public LanguageEffectType(){
        }

        public LanguageEffectType Clone(){
            return new LanguageEffectType(this);
        }
    }
}