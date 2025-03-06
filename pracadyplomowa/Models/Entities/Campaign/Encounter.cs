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
        
        public bool IsActive { get; set; } = false;
        
        public ParticipanceData AddParticipance(Character character)
        {
            var participanceData = new ParticipanceData
            {
                R_Character = character,
                R_Encounter = this
            };
            
            if (!R_Participances.Any(pd => pd.R_Character.Id == participanceData.R_Character.Id && pd.R_Encounter.Id == participanceData.R_Encounter.Id))
            {
                R_Participances.Add(participanceData);
            }
            
            return participanceData;
        }
        
        public void RemoveParticipanceByCharacterId(int characterId)
        {
            var participanceData = R_Participances.FirstOrDefault(pd => pd.R_Character.Id == characterId);
            if (participanceData != null)
            {
                R_Participances.Remove(participanceData);
            }
        }
    }
}