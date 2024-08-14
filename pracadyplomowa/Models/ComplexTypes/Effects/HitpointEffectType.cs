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
    public class HitpointEffectType(HitpointEffect hitpointEffect)
    {
        private readonly HitpointEffect? _HitpointEffect = hitpointEffect;
        public HitpointEffect HitpointEffect => (HitpointEffect)_HitpointEffect;
    }
}