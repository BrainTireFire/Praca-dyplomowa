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
    public class ProficiencyEffectType
    {
        public ProficiencyEffect ProficiencyEffect { get; set; }
        public ItemType ItemType { get; set;}
        public ProficiencyEffectType(ProficiencyEffectType cloned){
            this.ProficiencyEffect = cloned.ProficiencyEffect;
            this.ItemType = cloned.ItemType;
        }
        public ProficiencyEffectType(){
        }

        public ProficiencyEffectType Clone(){
            return new ProficiencyEffectType(this);
        }
    }
}