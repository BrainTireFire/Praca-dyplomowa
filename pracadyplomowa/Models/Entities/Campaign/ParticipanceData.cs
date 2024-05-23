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
        public virtual Encounter R_Encounter{ get; set; } = null!;
        public virtual ICollection<Field> R_OccupiedFields { get; set; } = [];
        public virtual Character R_Character { get; set; } = null!;
        public int CharacterId { get; set; }
    }
}