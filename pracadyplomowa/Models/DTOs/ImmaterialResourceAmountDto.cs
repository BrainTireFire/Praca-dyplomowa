using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Models.DTOs
{
    public class ImmaterialResourceAmountDto {
        public int BlueprintId { get; set; }
        public string Name { get; set; } = null!;
        public int Count { get; set; }
        public int Level {get; set;}
    }
}