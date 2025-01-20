using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.DTOs
{
    public class WeaponAttackConditionalEffectsDtos // for checking if 
    {
        public int WeaponId { get; set; }
        public string WeaponName { get; set; } = null!;
        public string WeaponDescription { get; set; } = null!;
        public List<ConditionalEffectDto> CasterConditionalEffects { get; set; } = [];
        public Dictionary<int, TargetDto> TargetsConditionalEffects { get; set; } = [];
        public class ConditionalEffectDto {
            public int EffectId { get; set; }
            public string EffectName { get; set; } = null!;
            public string EffectDescription { get; set; } = null!;
            public bool Selected { get; set; } = false;
        }
        public class TargetDto {
            public string TargetName { get; set; } = null!;
            public List<ConditionalEffectDto> TargetConditionalEffects { get; set; } = [];
        }
    }

    public class ConditionalEffectsDtos
    {
        public List<int> CasterConditionalEffects { get; set; } = [];
        public Dictionary<int, List<int>> TargetsConditionalEffects { get; set; } = [];
    }

    public class DamageTypeOnHitDto {
        public DamageType DamageType { get; set; }
        public DiceSetDto DamageValue { get; set; } = null!;
        public string DamageSource { get; set; } = null!;
    }
}