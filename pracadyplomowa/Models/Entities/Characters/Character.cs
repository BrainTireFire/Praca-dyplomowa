
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using Microsoft.OpenApi.Expressions;
using pracadyplomowa.Models.Entities.Campaign;
using pracadyplomowa.Models.Entities.Items;
using pracadyplomowa.Models.Entities.Powers;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;
using pracadyplomowa.Models.Entities.Powers.EffectInstances;
using pracadyplomowa.Models.Enums;
using pracadyplomowa.Models.Enums.EffectOptions;
using pracadyplomowa.Utility;

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

            AbilityEffectInstance strength = new("Strength base")
            {
                Description = "Strength base"
            };
            strength.AbilityEffectType.AbilityEffect = AbilityEffect.Bonus;
            strength.AbilityEffectType.AbilityEffect_Ability = Ability.STRENGTH;
            strength.DiceSet = strengthValue;

            AbilityEffectInstance dexterity = new("Dexterity base")
            {
                Description = "Dexterity base"
            };
            dexterity.AbilityEffectType.AbilityEffect = AbilityEffect.Bonus;
            dexterity.AbilityEffectType.AbilityEffect_Ability = Ability.DEXTERITY;
            dexterity.DiceSet = dexterityValue;

            AbilityEffectInstance constitution = new("Constitution base")
            {
                Description = "Constitution base"
            };
            constitution.AbilityEffectType.AbilityEffect = AbilityEffect.Bonus;
            constitution.AbilityEffectType.AbilityEffect_Ability = Ability.CONSTITUTION;
            constitution.DiceSet = constitutionValue;

            AbilityEffectInstance intelligence = new("Intelligence base")
            {
                Description = "Intelligence base"
            };
            intelligence.AbilityEffectType.AbilityEffect = AbilityEffect.Bonus;
            intelligence.AbilityEffectType.AbilityEffect_Ability = Ability.INTELLIGENCE;
            intelligence.DiceSet = intelligenceValue;

            AbilityEffectInstance wisdom = new("Wisdom base")
            {
                Description = "Wisdom base"
            };
            wisdom.AbilityEffectType.AbilityEffect = AbilityEffect.Bonus;
            wisdom.AbilityEffectType.AbilityEffect_Ability = Ability.WISDOM;
            wisdom.DiceSet = wisdomValue;

            AbilityEffectInstance charisma = new("Charisma base")
            {
                Description = "Charisma base"
            };
            charisma.AbilityEffectType.AbilityEffect = AbilityEffect.Bonus;
            charisma.AbilityEffectType.AbilityEffect_Ability = Ability.CHARISMA;
            charisma.DiceSet = charismaValue;

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

            List<ChoiceGroup> fullChoiceGroups = classLevel.R_ChoiceGroups.Union(
                race.R_RaceLevels.SelectMany(rl => rl.R_ChoiceGroups)
                ).Where(cg => cg.NumberToChoose == 0).ToList();
            List<ChoiceGroupUsage> usedChoiceGroups = [];
            foreach(ChoiceGroup cg in fullChoiceGroups){
                cg.Generate(this);
            }

            this.R_OwnerId = ownerId;
        }

        [NotMapped]
        public int ProficiencyBonus {
            get => this.R_CharacterHasLevelsInClass.Count / 5 + 2;
        }

        [NotMapped]
        public List<EffectInstance> ApprovedConditionalEffectInstances {get; set;} = [];

        public int AbilityValue(Ability ability){
            return this.R_AffectedBy.Where(x => x.Conditional == false).Union(this.ApprovedConditionalEffectInstances).OfType<AbilityEffectInstance>().Where(aei => 
            aei.AbilityEffectType.AbilityEffect == AbilityEffect.Bonus &&
            aei.AbilityEffectType.AbilityEffect_Ability == ability
            ).Aggregate(0, (acc, valueEffectInstance) => acc + valueEffectInstance.DiceSet);
        }

        [NotMapped]
        public int Strength {
            get => this.AbilityValue(Ability.STRENGTH);
        }
        [NotMapped]
        public int Dexterity {
            get => this.AbilityValue(Ability.DEXTERITY);
        }
        [NotMapped]
        public int Constitution {
            get => this.AbilityValue(Ability.CONSTITUTION);
        }
        [NotMapped]
        public int Intelligence {
            get => this.AbilityValue(Ability.INTELLIGENCE);
        }
        [NotMapped]
        public int Wisdom {
            get => this.AbilityValue(Ability.WISDOM);
        }
        [NotMapped]
        public int Charisma {
            get => this.AbilityValue(Ability.CHARISMA);
        }

        public static int AbilityModifier(int abilityValue){
            return (abilityValue - 10) / 2;
        }

        [NotMapped]
        public int StrengthModifier {
            get => AbilityModifier(Strength);
        }
        [NotMapped]
        public int DexterityModifier {
            get => AbilityModifier(Dexterity);
        }
        [NotMapped]
        public int ConstitutionModifier {
            get => AbilityModifier(Constitution);
        }
        [NotMapped]
        public int IntelligenceModifier {
            get => AbilityModifier(Intelligence);
        }
        [NotMapped]
        public int WisdomModifier {
            get => AbilityModifier(Wisdom);
        }
        [NotMapped]
        public int CharismaModifier {
            get => AbilityModifier(Charisma);
        }

        public int SkillValue(Skill skill){
            int value = this.R_AffectedBy.Where(x => x.Conditional == false).Union(this.ApprovedConditionalEffectInstances).OfType<SkillEffectInstance>().Where(aei => 
            aei.SkillEffectType.SkillEffect == SkillEffect.Bonus &&
            aei.SkillEffectType.SkillEffect_Skill == skill
            ).Aggregate(0, (acc, valueEffectInstance) => acc + valueEffectInstance.DiceSet);

            value += AbilityModifier(AbilityValue(Utils.SkillToAbility(skill)));

            value += this.R_AffectedBy.Where(x => x.Conditional == false).Union(this.ApprovedConditionalEffectInstances).OfType<SkillEffectInstance>().Where(aei => 
            aei.SkillEffectType.SkillEffect == SkillEffect.Proficiency &&
            aei.SkillEffectType.SkillEffect_Skill == skill
            ).Any() ? this.ProficiencyBonus : 0;

            return value;
        }

        [NotMapped]
        public int Athletics {
            get => this.SkillValue(Skill.Athletics);
        }
        [NotMapped]
        public int Acrobatics {
            get => this.SkillValue(Skill.Acrobatics);
        }
        [NotMapped]
        public int Sleight_of_Hand {
            get => this.SkillValue(Skill.Sleight_of_Hand);
        }
        [NotMapped]
        public int Stealth {
            get => this.SkillValue(Skill.Stealth);
        }
        [NotMapped]
        public int Arcana {
            get => this.SkillValue(Skill.Arcana);
        }
        [NotMapped]
        public int History {
            get => this.SkillValue(Skill.History);
        }
        [NotMapped]
        public int Investigation {
            get => this.SkillValue(Skill.Investigation);
        }
        [NotMapped]
        public int Nature {
            get => this.SkillValue(Skill.Nature);
        }
        [NotMapped]
        public int Religion {
            get => this.SkillValue(Skill.Religion);
        }
        [NotMapped]
        public int Animal_Handling {
            get => this.SkillValue(Skill.Animal_Handling);
        }
        [NotMapped]
        public int Insight {
            get => this.SkillValue(Skill.Insight);
        }
        [NotMapped]
        public int Medicine {
            get => this.SkillValue(Skill.Medicine);
        }
        [NotMapped]
        public int Perception {
            get => this.SkillValue(Skill.Perception);
        }
        [NotMapped]
        public int Survival {
            get => this.SkillValue(Skill.Survival);
        }
        [NotMapped]
        public int Deception {
            get => this.SkillValue(Skill.Deception);
        }
        [NotMapped]
        public int Intimidation {
            get => this.SkillValue(Skill.Intimidation);
        }
        [NotMapped]
        public int Performance {
            get => this.SkillValue(Skill.Performance);
        }
        [NotMapped]
        public int Persuasion {
            get => this.SkillValue(Skill.Persuasion);
        }
    }
}