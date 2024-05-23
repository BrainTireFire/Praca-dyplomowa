using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Powers;

namespace pracadyplomowa.Models.Entities.Items
{
    public class ItemCostRequirement
    {
        public int Value { get; set; }

        //Relationships
        public int PowerId { get; set; }
        public Power R_Power { get; set; } = null!;

        public ItemFamily R_ItemFamily { get; set; } = null!;
        public int ItemFamilyId {get; set;}
    }
}