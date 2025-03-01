using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.DTOs
{
    public class WeaponAttackIncomingDataDto
    {
        public WeaponAttackConditionalEffectsDto WeaponAttackConditionalEffects { get; set; }
        public List<PowerDto> Powers { get; set; }

        public WeaponAttackIncomingDataDto()
        {
            WeaponAttackConditionalEffects = new WeaponAttackConditionalEffectsDto();
            Powers = new List<PowerDto>();
        }
    }

    public class WeaponAttackConditionalEffectsDto
    {
        public List<int> CasterConditionalEffects { get; set; }
        public List<int> TargetConditionalEffects { get; set; }

        public WeaponAttackConditionalEffectsDto()
        {
            CasterConditionalEffects = new List<int>();
            TargetConditionalEffects = new List<int>();
        }
    }

    public class PowerDto
    {
        public int PowerId { get; set; }
        public PowerConditionalEffectsDto PowerConditionalEffects { get; set; }

        public PowerDto()
        {
            PowerConditionalEffects = new PowerConditionalEffectsDto();
        }
    }

    public class PowerConditionalEffectsDto
    {
        public List<int> CasterConditionalEffects { get; set; }
        public List<int> TargetConditionalEffects { get; set; }

        public PowerConditionalEffectsDto()
        {
            CasterConditionalEffects = new List<int>();
            TargetConditionalEffects = new List<int>();
        }
    }

    // public class ConditionalEffectDto
    // {
    //     public int EffectId { get; set; }
    //     public bool Selected { get; set; }
    // }

}