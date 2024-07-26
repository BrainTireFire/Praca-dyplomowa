using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Models.ValueTypes
{
    public class HitDice
    {
        public DiceSet Total { get; set; } = null!;
        public DiceSet Left { get; set; } = null!;
    }
}