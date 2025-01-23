using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.DTOs
{
    public class PowerDataForResolutionDto
    {
        public int PowerId { get; set; }
        public string PowerName { get; set; } = null!;
        public string ResourceName {get; set;} = null!;
        public Dictionary<int, Dictionary<int, List<PowerEffectDto>>> PowerEffects { get; set; } = new ();
        public class PowerEffectDto {
            public int PowerEffectId {get; set;}
            public string PowerEffectName {get; set;} = null!;
            public string PowerEffectDescription {get; set;} = null!;
        }
    }
}