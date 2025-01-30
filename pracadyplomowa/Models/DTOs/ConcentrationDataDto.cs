using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Models.DTOs
{
    public class ConcentrationDataDto
    {
        public int? EffectGroupId { get; set; }
        public string? EffectGroupName { get; set; }
        public int? EffectGroupDurationLeft { get; set; }
    }
}