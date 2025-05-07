using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Models.DTOs
{
    public class DiceSetDto
    {
        public int d20 { get; set; }
        public int d12 { get; set; }
        public int d10 { get; set; }
        public int d8 { get; set; }
        public int d6 { get; set; }
        public int d4 { get; set; }
        public int d100 { get; set; }
        public int flat { get; set; }

        // Parameterless constructor
        public DiceSetDto()
        {
        }

        // Constructor that takes a DiceSet
        public DiceSetDto(DiceSet diceSet)
        {
            d20 = diceSet.d20;
            d12 = diceSet.d12;
            d10 = diceSet.d10;
            d8 = diceSet.d8;
            d6 = diceSet.d6;
            d4 = diceSet.d4;
            d100 = diceSet.d100;
            flat = diceSet.flat;
        }
    }
}