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
    public class AttackPerAttackActionEffectType
    {
        public AttackPerActionEffect AttackPerActionEffect { get; set; }
        public DiceSet AttackPerActionEffect_Value { get; set; }
    }
}