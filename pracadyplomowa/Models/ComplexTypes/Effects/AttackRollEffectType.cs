#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Enums.EffectOptions;

namespace pracadyplomowa.Models.ComplexTypes.Effects
{
    [ComplexType]
    public class AttackRollEffectType(AttackRollEffect_Range attackRollEffect_Range, AttackRollEffect_Source attackRollEffect_Source, AttackRollEffect_Type attackRollEffect_Type)
    {
        private readonly AttackRollEffect_Range? _AttackRollEffect_Range = attackRollEffect_Range;
        private readonly AttackRollEffect_Source? _AttackRollEffect_Source = attackRollEffect_Source;
        private readonly AttackRollEffect_Type? _AttackRollEffect_Type = attackRollEffect_Type;
        // [NotMapped]
        public AttackRollEffect_Range AttackRollEffect_Range => (AttackRollEffect_Range)_AttackRollEffect_Range;
        // [NotMapped]
        public AttackRollEffect_Source AttackRollEffect_Source => (AttackRollEffect_Source)_AttackRollEffect_Source;
        // [NotMapped]
        public AttackRollEffect_Type AttackRollEffect_Type => (AttackRollEffect_Type)_AttackRollEffect_Type; 
    }
}