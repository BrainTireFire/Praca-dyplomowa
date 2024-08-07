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
    public class AttackPerAttackActionEffectType(AttackPerActionEffect attackPerActionEffect)
    {
        private readonly AttackPerActionEffect? _AttackPerActionEffect = attackPerActionEffect;
        public AttackPerActionEffect? AttackPerActionEffect { get => _AttackPerActionEffect == null ? _AttackPerActionEffect : throw new ArgumentNullException(); }
    }
}