using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.DTOs
{
    public class ImmaterialResourceBlueprintWithOwnerDto {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string OwnerName { get; set; } = null!;
        public int? OwnerId { get; set; } = null!;
        public bool Editable { get; set; } = false!;
        public RefreshType RefreshesOn { get; set; } = RefreshType.TurnStart;
    }
}