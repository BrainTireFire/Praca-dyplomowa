#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.ComplexTypes.Effects
{
    [ComplexType]
    public class StatusEffectType(Condition statusEffect)
    {
        private readonly Condition? _StatusEffect = statusEffect;

        public Condition? StatusEffect =>  (Condition) _StatusEffect;
    }
}