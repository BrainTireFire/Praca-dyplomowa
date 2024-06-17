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
    public class SavingThrowEffectType
    {
        public SavingThrowEffect SavingThrowEffect { get; set; }
        public DiceSet SavingThrowEffect_Value { get; set; }
        public Ability SavingThrowEffect_Ability { get; set; }
    }
}