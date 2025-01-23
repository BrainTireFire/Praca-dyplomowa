using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.DTOs
{
    public class PowerDataAndConditionalEffectsDto
    {
        public PowerDataForResolutionDto PowerData { get; set; } = new();
        public ConditionalEffectsSetForManyTargetsDto ConditionalEffects { get; set; } = new();
    }
}