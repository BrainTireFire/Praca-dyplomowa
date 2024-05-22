using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Models.Entities.Campaign
{
    public class ParticipanceData
    {
        //Properties
        public int EncounterId { get; set; }
        public int CharacterOccupiesFieldId {get; set; }
        public int InitiativeOrder { get; set; }
        public bool IsSurprised { get; set; }
        public int NumberOfActionsTaken { get; set; }
        public int NumberOfBonusActionsTaken { get; set; }
        public int NumberOfAttacksTaken { get; set; }
        public int DistanceTraveled { get; set; }
        
        //Relationship
        public virtual Encounter R_Encounter{ get; set; }
        public virtual Field R_CharacterOccupiesField { get; set; }
        public virtual ICollection<Character> R_CharactersParticipateInEncounter { get; set; } = [];
    }
}