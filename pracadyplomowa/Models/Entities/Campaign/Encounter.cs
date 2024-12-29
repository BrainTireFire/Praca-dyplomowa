using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Models.Entities.Campaign
{
    public class Encounter : ObjectWithOwner
    {
        //Properties
        public string Name { get; set; } = null!;

        //Relationship
        public virtual Campaign? R_Campaign { get; set; }
        public int? R_CampaignId { get; set; }
        public virtual Board R_Board { get; set; } = null!;
        public int R_BoardId { get; set; }
        public virtual ICollection<ParticipanceData> R_Participances { get; set; } = [];
        
        public void AddParticipance(Character character)
        {
            var participanceData = new ParticipanceData
            {
                R_Character = character,
                R_Encounter = this
            };
            
            R_Participances.Add(participanceData);
        }
    }
}