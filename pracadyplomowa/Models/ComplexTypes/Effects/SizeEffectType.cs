using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Enums;
using pracadyplomowa.Models.Enums.EffectOptions;

namespace pracadyplomowa.Models.ComplexTypes.Effects
{
    [ComplexType]
    public class SizeEffectType
    {
        public SizeEffect SizeEffect { get; set; }
        public Size SizeEffect_SizeToSet { get; set; }

        public int SizeBonus { get; set; }
    }
}