using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.DTOs
{
    public class ConditionalEffectsSetDto // for checking if 
    {
        public List<ConditionalEffectDto> CasterConditionalEffects { get; set; } = [];
        public List<ConditionalEffectDto> TargetConditionalEffects { get; set; } = [];
        public class ConditionalEffectDto {
            public int EffectId { get; set; }
            public string EffectName { get; set; } = null!;
            public string EffectDescription { get; set; } = null!;
            public bool Selected { get; set; } = false;
        }
        // public class TargetDto {
        //     public string TargetName { get; set; } = null!;
        //     public List<ConditionalEffectDto> TargetConditionalEffects { get; set; } = [];
        // }
    }

    public class ApprovedConditionalEffectsDto
    {
        public List<int> CasterConditionalEffects { get; set; } = [];
        public List<int> TargetConditionalEffects { get; set; } = [];
    }

    public class ConditionalEffectsSetForManyTargetsDto
    {
        public List<ConditionalEffectDto> CasterConditionalEffects { get; set; } = [];
        public Dictionary<int, List<ConditionalEffectDto>> TargetConditionalEffects { get; set; } = [];
        public class ConditionalEffectDto {
            public int EffectId { get; set; }
            public string EffectName { get; set; } = null!;
            public string EffectDescription { get; set; } = null!;
            public bool Selected { get; set; } = false;
        }
    }
}