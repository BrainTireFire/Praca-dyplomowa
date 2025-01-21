using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.DTOs
{
    public class WeaponDamageAndPowersDto // for checking if 
    {
        public int WeaponId { get; set; }
        public string WeaponName { get; set; } = null!;
        public List<DamageValueDto> DamageValues { get; set; } = [];
        public List<PowersOnHitDto> PowersOnHit { get; set; } = [];

        public class DamageValueDto {
            public DamageType DamageType { get; set; }
            public DiceSetDto DamageValue { get; set; } = null!;
            public string DamageSource { get; set; } = null!;
        }
        public class PowersOnHitDto {
            public int PowerId {get; set;}
            public string PowerName { get; set; } = null!;
            public string PowerDescription { get; set; } = null!;
            public List<PowerEffectDto> PowerEffects { get; set; } = [];
            public class PowerEffectDto {
                public int PowerEffectId {get; set;}
                public string PowerEffectName {get; set;} = null!;
                public string PowerEffectDescription {get; set;} = null!;
            }
        }
    }
}