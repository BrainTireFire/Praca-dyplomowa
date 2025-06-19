using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Items;

namespace pracadyplomowa.Models.Entities.Campaign
{
    public class ShopItem : ObjectWithId
    {
        //Properties
        public CoinSack Price { get; set; } = new();

        //Relationship
        public virtual Item R_ShopHasItem { get; set; } = null!;
        public int R_ShopHasItemId { get; set; }
        public virtual Shop R_ItemInShop { get; set; } = null!;
        public int R_ItemInShopId { get; set; }
    }
}