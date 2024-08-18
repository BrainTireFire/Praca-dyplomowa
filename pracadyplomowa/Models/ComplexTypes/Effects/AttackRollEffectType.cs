using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Enums.EffectOptions;

namespace pracadyplomowa.Models.ComplexTypes.Effects
{
    [Owned]
    public class AttackRollEffectType
    {
        public AttackRollEffect_Range AttackRollEffect_Range { get; set; }
        public AttackRollEffect_Source AttackRollEffect_Source { get; set; }
        public AttackRollEffect_Type AttackRollEffect_Type { get; set; }
    }
}