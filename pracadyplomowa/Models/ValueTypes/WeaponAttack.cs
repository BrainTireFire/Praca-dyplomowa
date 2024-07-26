using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.ValueTypes
{
    public class WeaponAttack
    {
        public int Id { get; set; }
        public bool Main { get; set; } = false;

        public List<Damage> Damages { get; set; } = [];

        public int Range { get; set; } = 0;
    
        public class Damage {
            public DamageType DamageType{ get; set; }
            public DiceSet DamageValue { get; set; } = null!;
        }

    }
}