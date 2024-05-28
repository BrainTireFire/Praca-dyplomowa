using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Models.Entities.Campaign
{
    public class Shop : ObjectWithId
    {
        //Properties
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string Description { get; set; } = null!;

        //Relationship
        public virtual Campaign R_Campaign { get; set; } = null!;
        public int R_CampaignId { get; set; }
        public virtual ICollection<ShopItem> R_Items { get; set; } = null!;
    }
}