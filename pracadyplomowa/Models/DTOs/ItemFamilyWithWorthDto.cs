using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Models.DTOs
{
    public class ItemCostRequirementDto {
        public int Id { get; set; }
        public int ItemFamilyId { get; set; }
        public string Name { get; set; } = null!;
        public CoinPurseDto Worth { get; set; } = null!;
    }
}