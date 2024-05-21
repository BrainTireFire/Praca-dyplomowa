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

        public virtual Item Item { get; set; }
    }
}