using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Models.ComplexTypes.Effects
{
    [ComplexType]
    public class MovementCostEffectType(int movementCost_Multiplier)
    {
        private readonly int? _MovementCost_Multiplier = movementCost_Multiplier;
        public int? MovementCost_Multiplier { get => _MovementCost_Multiplier == null ? _MovementCost_Multiplier : throw new ArgumentNullException(); }
    }
}