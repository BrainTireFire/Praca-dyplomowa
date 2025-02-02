using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Models.Entities.Campaign
{
    public class ActionLog : ObjectWithId
    {
        //Properties
        public DateTime Time { get; set; } = DateTime.Now;
        public string? Source { get; set; }
        public string? Content { get; set; }
        public int EncounterId { get; set; }
        
        //Relationship
        public virtual Campaign R_Campaign { get; set; } = null!;
        public virtual int R_CampaignId { get; set; }
    }
}