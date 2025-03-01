using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.DTOs
{
    public class ImmaterialResourceBlueprintDto {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public RefreshType RefreshesOn { get; set; } = RefreshType.TurnStart;
    }
}