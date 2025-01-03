using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Models.Entities.Campaign
{
    public class ParticipanceData : ObjectWithId
    {
        //Properties
        public int InitiativeOrder { get; set; }
        public bool IsSurprised { get; set; }
        public int NumberOfActionsTaken { get; set; }
        public int NumberOfBonusActionsTaken { get; set; }
        public int NumberOfAttacksTaken { get; set; }
        public int DistanceTraveled { get; set; }

        //Relationship
        public virtual Encounter R_Encounter { get; set; } = null!;
        public int R_EncounterId { get; set; }
        public virtual ICollection<Field> R_OccupiedFields { get; set; } = [];
        public virtual Character R_Character { get; set; } = null!;
        public int R_CharacterId { get; set; }

        public bool IsAdjacentToParticipant(ParticipanceData participance){
            foreach(var field in R_OccupiedFields){
                foreach(var otherField in participance.R_OccupiedFields){
                    if(field.IsAdjacentToField(otherField)){
                        return true;
                    }
                }
            }
            return false;
        }
    }
}