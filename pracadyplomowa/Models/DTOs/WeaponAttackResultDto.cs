using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.DTOs
{
    public class WeaponAttackResultDto {
        public HitType AttackRollResult {get; set;}
        public List<PowerUsageResultDto> PowerResult {get; set;} = [];
        public int TotalDamage { get; set; }
        public int HitpointsLeft { get; set; }

        public class PowerUsageResultDto {
            public string PowerName { get; set; } = null!;
            public bool Success { get; set; }
        }
    }
}