using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Enums;
using pracadyplomowa.Models.Enums.EffectOptions;

namespace pracadyplomowa.Models.ComplexTypes.Effects
{
    [ComplexType]
    public class ResistanceEffectType
    {
        public ResistanceEffect ResistanceEffect { get; set; }
        public DamageType ResistanceEffect_DamageType { get; set; }
    }
}