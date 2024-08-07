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
        public AttackRollEffect_Range? AttackRollEffect_Range { get => _AttackRollEffect_Range == null ? _AttackRollEffect_Range : throw new ArgumentNullException(); }
        public AttackRollEffect_Source? AttackRollEffect_Source { get => _AttackRollEffect_Source == null ? _AttackRollEffect_Source : throw new ArgumentNullException(); }
        public AttackRollEffect_Type? AttackRollEffect_Type { get => _AttackRollEffect_Type == null ? _AttackRollEffect_Type : throw new ArgumentNullException(); }
    }
}