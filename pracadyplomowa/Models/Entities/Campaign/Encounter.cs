using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Models.Entities.Campaign
{
    public class Encounter
    {
        //Properties
        public int EncounterInTheCampaignId { get; set; }
        public string Name { get; set; } = null!;

        //Relationship
        public virtual Campaign? R_Campaign { get; set; }
        public int? CampaignId { get; set; }
        public virtual Board R_Board{ get; set; } = null!;
        public int BoardId { get; set; }
        public virtual ICollection<ParticipanceData> R_Participances{ get; set; } = [];
    }
}