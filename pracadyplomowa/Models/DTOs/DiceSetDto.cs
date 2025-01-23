using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Models.DTOs
{
    public class DiceSetDto(DiceSet diceSet)
    {
        public int d20 { get; set; } = diceSet.d20;
        public int d12 { get; set; } = diceSet.d12;
        public int d10 { get; set; } = diceSet.d10;
        public int d8 { get; set; } = diceSet.d8;
        public int d6 { get; set; } = diceSet.d6;
        public int d4 { get; set; } = diceSet.d4;
        public int d100 { get; set; } = diceSet.d100;
        public int flat { get; set; } = diceSet.flat;
    }
}