
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
        public virtual ICollection<EffectInstance> R_AffectedBy { get; set; } = [];
        public virtual ICollection<Power> R_PowersPrepared { get; set; } = [];
        public virtual ICollection<Power> R_PowersKnown { get; set; } = [];
        public virtual Power? R_SpawnedByPower { get; set; }
        public int? R_SpawnedByPowerId { get; set; }
        public virtual ICollection<ImmaterialResourceInstance> R_ImmaterialResourceInstances { get; set; } = [];
        public virtual ICollection<ChoiceGroupUsage> R_UsedChoiceGroups { get; set;} = [];

        public Character(){

        }
        public Character(string name, int strengthValue, int dexterityValue, int constitutionValue, int intelligenceValue, int wisdomValue, int charismaValue, ClassLevel classLevel, Race race, int ownerId){

            this.Name = name;

            this.R_CharacterHasLevelsInClass.Add(classLevel);
            this.R_CharacterBelongsToRace = race;

            AbilityEffectInstance strength = new()
            {
                Name = "Strength base",
                Description = "Strength base"
            };
            strength.AbilityEffectType.AbilityEffect = AbilityEffect.Bonus;
            strength.AbilityEffectType.AbilityEffect_Ability = Ability.STRENGTH;
            strength.DiceSet.flat = strengthValue;
            strength.SourceName = "Base";

            AbilityEffectInstance dexterity = new()
            {
                Name = "Dexterity base",
                Description = "Dexterity base"
            };
            dexterity.AbilityEffectType.AbilityEffect = AbilityEffect.Bonus;
            dexterity.AbilityEffectType.AbilityEffect_Ability = Ability.DEXTERITY;
            dexterity.DiceSet.flat = dexterityValue;
            dexterity.SourceName = "Base";

            AbilityEffectInstance constitution = new()
            {
                Name = "Constitution base",
                Description = "Constitution base"
            };
            constitution.AbilityEffectType.AbilityEffect = AbilityEffect.Bonus;
            constitution.AbilityEffectType.AbilityEffect_Ability = Ability.CONSTITUTION;
            constitution.DiceSet.flat = constitutionValue;
            constitution.SourceName = "Base";

            AbilityEffectInstance intelligence = new()
            {
                Name = "Intelligence base",
                Description = "Intelligence base"
            };
            intelligence.AbilityEffectType.AbilityEffect = AbilityEffect.Bonus;
            intelligence.AbilityEffectType.AbilityEffect_Ability = Ability.INTELLIGENCE;
            intelligence.DiceSet.flat = intelligenceValue;
            intelligence.SourceName = "Base";

            AbilityEffectInstance wisdom = new()
            {
                Name = "Wisdom base",
                Description = "Wisdom base"
            };
            wisdom.AbilityEffectType.AbilityEffect = AbilityEffect.Bonus;
            wisdom.AbilityEffectType.AbilityEffect_Ability = Ability.WISDOM;
            wisdom.DiceSet.flat = wisdomValue;
            wisdom.SourceName = "Base";

            AbilityEffectInstance charisma = new()
            {
                Name = "Charisma base",
                Description = "Charisma base"
            };
            charisma.AbilityEffectType.AbilityEffect = AbilityEffect.Bonus;
            charisma.AbilityEffectType.AbilityEffect_Ability = Ability.CHARISMA;
            charisma.DiceSet.flat = charismaValue;
            charisma.SourceName = "Base";

            EffectGroup basicStats = new()
            {
                IsConstant = true
            };
            basicStats.R_OwnedEffects.Add(strength);
            basicStats.R_OwnedEffects.Add(dexterity);
            basicStats.R_OwnedEffects.Add(constitution);
            basicStats.R_OwnedEffects.Add(intelligence);
            basicStats.R_OwnedEffects.Add(wisdom);
            basicStats.R_OwnedEffects.Add(charisma);
            strength.R_OwnedByGroup = basicStats;
            dexterity.R_OwnedByGroup = basicStats;
            constitution.R_OwnedByGroup = basicStats;
            intelligence.R_OwnedByGroup = basicStats;
            wisdom.R_OwnedByGroup = basicStats;
            charisma.R_OwnedByGroup = basicStats;

            this.R_AffectedBy = basicStats.R_OwnedEffects;

            this.R_OwnerId = ownerId;
        }
    }
}