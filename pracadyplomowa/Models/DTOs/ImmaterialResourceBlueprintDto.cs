using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Models.DTOs
{
    public class ImmaterialResourceBlueprintDto {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }

    public class ImmaterialResourceBlueprintWithOwnerDto {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string OwnerName { get; set; } = null!;
        public int? OwnerId { get; set; } = null!;
        public bool Editable { get; set; } = false!;
    }
    public class ImmaterialResourceBlueprintDto_ForInsert {
        public string Name { get; set; } = null!;
    }
}