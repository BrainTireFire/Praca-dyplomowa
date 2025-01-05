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
        
        public void UpdateCharacter(Character newCharacter)
        {
            if (newCharacter == null)
            {
                throw new ArgumentNullException(nameof(newCharacter), "Character cannot be null.");
            }

            // Remove from the old character's collection if any
            if (R_Character != null)
            {
                R_Character.R_CharactersParticipatesInEncounters.Remove(this);
            }

            // Update the character
            R_Character = newCharacter;
            R_CharacterId = newCharacter.Id;

            // Add to the new character's collection
            if (!newCharacter.R_CharactersParticipatesInEncounters.Contains(this))
            {
                newCharacter.R_CharactersParticipatesInEncounters.Add(this);
            }
        }
    }
}