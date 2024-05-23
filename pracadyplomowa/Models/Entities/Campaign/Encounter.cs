using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Models.Entities.Campaign
{
    public class Encounter
    {
        //Properties
        public int BoardId { get; set; }
        public int EncounterInTheCampaignId { get; set; }
        public string Name { get; set; }

        //Relationship
        public virtual Campaign R_EncounterInTheCampaign { get; set; }
        public virtual Board R_Board{ get; set; }
        public virtual ICollection<ParticipanceData> R_Participances{ get; set; }
        public virtual ICollection<ActionLog> R_Log { get; set; }
    }
}