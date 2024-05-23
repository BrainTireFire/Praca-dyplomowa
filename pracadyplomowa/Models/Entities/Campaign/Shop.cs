using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Models.Entities.Campaign
{
    public class Shop : ObjectWithId
    {
        //Properties
        public string Name { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        
        //Relationship
        public virtual Campaign R_ShopInCampaign { get; set; }
    }
}