using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Models.Entities.Characters
{
    public class Character : ObjectWithOwner
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DiceSet UsedHitDice { get; set; }= new DiceSet();
    }
}