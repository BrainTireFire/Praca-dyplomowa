using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Models.ComplexTypes.Effects
{
    [Owned]
    public class MagicItemEffectType
    {
        public MagicItemEffectType(MagicItemEffectType cloned){
        }
        public MagicItemEffectType(){
        }

        public MagicItemEffectType Clone(){
            return new MagicItemEffectType(this);
        }
    }
}