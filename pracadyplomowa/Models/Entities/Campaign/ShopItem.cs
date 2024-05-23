using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Items;

namespace pracadyplomowa.Models.Entities.Campaign
{
    public class ShopItem
    {
        //Properties
        public int R_ShopHasItemId { get; set; }
        public int R_ItemInShopId { get; set; }
        public int Quantity { get; set; }

        //Relationship
        public virtual Item R_ShopHasItem { get; set; }
        public virtual Shop R_ItemInShop {get; set; }
    }
}