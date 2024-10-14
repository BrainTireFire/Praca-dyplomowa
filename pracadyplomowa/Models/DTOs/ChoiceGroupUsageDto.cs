using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Models.DTOs
{
    public class ChoiceGroupUsageDto
    {
        public int Id { get; set; }
        public List<int> EffectIds { get; set; } = [];
        public List<int> PowerIds { get; set; } = [];
    }
}