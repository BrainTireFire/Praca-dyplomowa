using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Campaign;
using pracadyplomowa.Models.Entities.Items;
using pracadyplomowa.Models.Entities.Powers;

namespace pracadyplomowa.Models.Entities.Characters
{
    public class Character : ObjectWithOwner
    {
        
        //Properties
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DiceSet UsedHitDice { get; set; } = new DiceSet();

        //Relationship
        // public virtual User R_CharacterBelongsToUser { get; set; } = null!;
        // public int CharacterBelongsToUserId { get; set; }
        public virtual Race R_CharacterBelongsToRace { get; set; } = null!;
        public int CharacterBelongsToRaceId { get; set; }
        public virtual EffectGroup? R_ConcentratesOn { get; set; }
        public int ConcentratesOnId { get; set; }
        public virtual ICollection<ParticipanceData> R_CharactersParticipatesInEncounters { get; set; } = [];
        public virtual Backpack R_CharacterHasBackpack { get; set; } = null!;
        public int CharacterHasBackpackId { get; set; }
        
        public virtual ICollection<EquipData> R_EquippedItems { get; set; } = [];
        public virtual Campaign.Campaign? R_Campaign { get; set; }
        public int CampaignId { get; set; }
        public virtual ICollection<ClassLevel> R_CharacterHasLevelsInClass { get; set; } = [];
        public virtual ICollection<Aura> R_AuraCenteredAtCharacter { get; set; } = [];
        public virtual ICollection<EffectGroup> R_AffectedBy { get; set; } = [];
        public virtual ICollection<Power> R_PowersPrepared { get; set; } = [];
        public virtual ICollection<Power> R_PowersKnown { get; set; } = [];
        public virtual Power? R_SpawnedByPower { get; set; }
        public int? SpawnedByPowerId { get; set; }
        public virtual ICollection<ImmaterialResourceInstance> R_ImmaterialResourceInstances { get; set;} = [];
    }
}