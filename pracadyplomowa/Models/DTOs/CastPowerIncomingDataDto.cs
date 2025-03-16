using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Models.DTOs
{
    public class CastPowerIncomingDataDto
    {
        // public int? SpellSlotLevel { get; set; }
        // public int? PowerLevel { get; set; }

        public ConditionalEffects ConditionalEffects { get; set; } = new ConditionalEffects();
    }

    public class ConditionalEffects
    {
        public List<int> CasterConditionalEffects { get; set; } = new List<int>();

        public Dictionary<int, List<int>> TargetConditionalEffects { get; set; } = new Dictionary<int, List<int>>();
    }
}