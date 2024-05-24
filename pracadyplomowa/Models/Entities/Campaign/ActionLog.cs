using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Models.Entities.Campaign
{
    public class ActionLog : ObjectWithId
    {
        //Properties
        public int EncounterId { get; set; }
        public string? Content;

        //Relationship
        public virtual Campaign R_Campaign { get; set; } = null!;
        public virtual int CampaignId { get; set; }
    }
}