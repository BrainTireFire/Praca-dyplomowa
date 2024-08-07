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
    public class MovementEffectType(MovementEffect movementEffect)
    {
        private readonly MovementEffect? _MovementEffect = movementEffect;
        public MovementEffect? MovementEffect { get => _MovementEffect == null ? _MovementEffect : throw new ArgumentNullException(); }
    }
}