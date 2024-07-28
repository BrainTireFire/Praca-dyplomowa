
using System.Linq.Expressions;
using Microsoft.OpenApi.Expressions;
using pracadyplomowa.Models.Entities.Campaign;
using pracadyplomowa.Models.Entities.Items;
using pracadyplomowa.Models.Entities.Powers;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;
using pracadyplomowa.Models.Entities.Powers.EffectInstances;
using pracadyplomowa.Models.Enums;
using pracadyplomowa.Models.Enums.EffectOptions;

namespace pracadyplomowa.Models.Entities.Characters
{
    public class Character : ObjectWithOwner
    {

        //Properties
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int Hitpoints { get; set; }
        public DiceSet UsedHitDice { get; set; } = new DiceSet();

        public int SucceededDeathSavingThrows { get; set; }
        public int FailedDeathSavingThrows { get; set; }

        //Relationship
        public virtual Race R_CharacterBelongsToRace { get; set; } = null!;
        public int R_CharacterBelongsToRaceId { get; set; }
        public virtual EffectGroup? R_ConcentratesOn { get; set; }
        public int? R_ConcentratesOnId { get; set; }
        public virtual ICollection<ParticipanceData> R_CharactersParticipatesInEncounters { get; set; } = [];
        public virtual Backpack R_CharacterHasBackpack { get; set; } = new Backpack();
        public int R_CharacterHasBackpackId { get; set; }

        public virtual ICollection<EquipData> R_EquippedItems { get; set; } = [];
        public virtual Campaign.Campaign? R_Campaign { get; set; }
        public int? R_CampaignId { get; set; }
        public virtual ICollection<ClassLevel> R_CharacterHasLevelsInClass { get; set; } = [];
        public virtual ICollection<Aura> R_AuraCenteredAtCharacter { get; set; } = [];
        public virtual ICollection<EffectGroup> R_AffectedBy { get; set; } = [];
        public virtual ICollection<Power> R_PowersPrepared { get; set; } = [];
        public virtual ICollection<Power> R_PowersKnown { get; set; } = [];
        public virtual Power? R_SpawnedByPower { get; set; }
        public int? R_SpawnedByPowerId { get; set; }
        public virtual ICollection<ImmaterialResourceInstance> R_ImmaterialResourceInstances { get; set; } = [];

    }
}