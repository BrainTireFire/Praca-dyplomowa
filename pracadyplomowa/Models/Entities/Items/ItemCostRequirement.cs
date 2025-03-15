using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Powers;

namespace pracadyplomowa.Models.Entities.Items
{
    public class ItemCostRequirement : ObjectWithId
    {
        // public int GoldPieces { get; set; }
        // public int SilverPieces { get; set; }
        // public int CopperPieces { get; set; }
        public CoinSack Worth {get; set;}

        //Relationships
        public virtual Power R_Power { get; set; } = null!;
        public int PowerId { get; set; }

        public virtual ItemFamily R_ItemFamily { get; set; } = null!;
        public int R_ItemFamilyId { get; set; }
    }
}