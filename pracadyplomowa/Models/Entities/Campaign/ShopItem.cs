using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Items;

namespace pracadyplomowa.Models.Entities.Campaign
{
    public class ShopItem
    {
        public int Quantity { get; set; }

        //Relationship
        public virtual Item R_ShopHasItem { get; set; }
    }
}