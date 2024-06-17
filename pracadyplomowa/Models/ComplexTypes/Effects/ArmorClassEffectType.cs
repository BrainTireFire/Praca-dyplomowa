using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Models.ComplexTypes.Effects
{
    [ComplexType]
    public class ArmorClassEffectType
    {
        public DiceSet ArmorClassEffect_Value { get; set; }
    }
}