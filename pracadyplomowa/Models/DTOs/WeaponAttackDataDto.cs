using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.DTOs
{
    public class WeaponAttackDataDto // for checking if 
    {
        public string AttackerName {get; set;} = "";
        public string TargetName {get; set;} = "";
        public WeaponDamageAndPowersDto WeaponDamageAndPowers { get; set; } = new();
        public ConditionalEffectsSetDto ConditionalEffects { get; set; } = new();
    }
}