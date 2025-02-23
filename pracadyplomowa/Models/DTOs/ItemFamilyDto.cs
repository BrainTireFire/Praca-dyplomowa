using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.DTOs
{
    public class ItemFamilyDto {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ItemType ItemType { get; set; }
        public string OwnerName { get; set; } = null!;
        public bool Editable { get; set; }
    }

    public class ItemFamilyDtoInsert {
        public string Name { get; set; } = null!;
        public ItemType ItemType { get; set; }
    }

    public class ItemFamilyDtoUpdate {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ItemType ItemType { get; set; }
    }
}