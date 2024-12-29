
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using Microsoft.OpenApi.Expressions;
using pracadyplomowa.Data.Migrations;
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
        public bool IsNpc { get; set; } = false;
        public DiceSet UsedHitDice { get; set; } = new DiceSet();

        public int SucceededDeathSavingThrows { get; set; }
        public int FailedDeathSavingThrows { get; set; }

        //Relationship
        public virtual Race R_CharacterBelongsToRace { get; set; } = null!;
        public int R_CharacterBelongsToRaceId { get; set; }
        public virtual EffectGroup? R_ConcentratesOn { get; set; }
        public int? R_ConcentratesOnId { get; set; }
        public virtual List<ParticipanceData> R_CharactersParticipatesInEncounters { get; set; } = [];
        public virtual Backpack R_CharacterHasBackpack { get; set; } = new Backpack();
        public int R_CharacterHasBackpackId { get; set; }

        public virtual List<EquipData> R_EquippedItems { get; set; } = [];
        public virtual Campaign.Campaign? R_Campaign { get; set; }
        public int? R_CampaignId { get; set; }
        public virtual List<ClassLevel> R_CharacterHasLevelsInClass { get; set; } = [];
        public virtual List<Aura> R_AuraCenteredAtCharacter { get; set; } = [];
        public virtual List<EffectInstance> R_AffectedBy { get; set; } = [];
        public virtual List<PowerSelection> R_PowersPrepared { get; set; } = [];
        public virtual List<Power> R_PowersKnown { get; set; } = [];
        public virtual Power? R_SpawnedByPower { get; set; }
        public int? R_SpawnedByPowerId { get; set; }
        public virtual List<ImmaterialResourceInstance> R_ImmaterialResourceInstances { get; set; } = []; // only for manual assignment
        public virtual List<ChoiceGroupUsage> R_UsedChoiceGroups { get; set;} = [];

        public Character(){

        }
        public Character(string name, bool isNpc, int strengthValue, int dexterityValue, int constitutionValue, int intelligenceValue, int wisdomValue, int charismaValue, ClassLevel classLevel, Race race, int ownerId){

            this.Name = name;
            this.IsNpc = isNpc;

            this.R_CharacterHasLevelsInClass.Add(classLevel);
            this.R_CharacterBelongsToRace = race;

            AbilityEffectInstance strength = new("Strength base")
            {
                Description = "Strength base"
            };
            strength.EffectType.AbilityEffect = AbilityEffect.Bonus;
            strength.EffectType.AbilityEffect_Ability = Ability.STRENGTH;
            strength.DiceSet = strengthValue;

            AbilityEffectInstance dexterity = new("Dexterity base")
            {
                Description = "Dexterity base"
            };
            dexterity.EffectType.AbilityEffect = AbilityEffect.Bonus;
            dexterity.EffectType.AbilityEffect_Ability = Ability.DEXTERITY;
            dexterity.DiceSet = dexterityValue;

            AbilityEffectInstance constitution = new("Constitution base")
            {
                Description = "Constitution base"
            };
            constitution.EffectType.AbilityEffect = AbilityEffect.Bonus;
            constitution.EffectType.AbilityEffect_Ability = Ability.CONSTITUTION;
            constitution.DiceSet = constitutionValue;

            AbilityEffectInstance intelligence = new("Intelligence base")
            {
                Description = "Intelligence base"
            };
            intelligence.EffectType.AbilityEffect = AbilityEffect.Bonus;
            intelligence.EffectType.AbilityEffect_Ability = Ability.INTELLIGENCE;
            intelligence.DiceSet = intelligenceValue;

            AbilityEffectInstance wisdom = new("Wisdom base")
            {
                Description = "Wisdom base"
            };
            wisdom.EffectType.AbilityEffect = AbilityEffect.Bonus;
            wisdom.EffectType.AbilityEffect_Ability = Ability.WISDOM;
            wisdom.DiceSet = wisdomValue;

            AbilityEffectInstance charisma = new("Charisma base")
            {
                Description = "Charisma base"
            };
            charisma.EffectType.AbilityEffect = AbilityEffect.Bonus;
            charisma.EffectType.AbilityEffect_Ability = Ability.CHARISMA;
            charisma.DiceSet = charismaValue;

            EffectGroup basicStats = new()
            {
                IsConstant = true,
                Name = "Base abilities"
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

            this.R_AffectedBy.AddRange(basicStats.R_OwnedEffects);

            // List<ChoiceGroup> fullChoiceGroups = classLevel.R_ChoiceGroups.Union(
            //     race.R_RaceLevels.SelectMany(rl => rl.R_ChoiceGroups)
            //     ).Where(cg => cg.NumberToChoose == 0).ToList();
            // foreach(ChoiceGroup cg in fullChoiceGroups){
            //     cg.Generate(this);
            // }
            GenerateChoiceGroupUsage();

            this.Hitpoints = this.MaxHealth;

            this.R_OwnerId = ownerId;
        }

        protected void GenerateChoiceGroupUsage(){
            List<ChoiceGroup> fullChoiceGroups = this.R_CharacterHasLevelsInClass.SelectMany(cl => cl.R_ChoiceGroups).Union(
                this.R_CharacterBelongsToRace.R_RaceLevels.Where(rl => rl.Level <= this.R_CharacterHasLevelsInClass.Count).SelectMany(rl => rl.R_ChoiceGroups)
                ).Where(cg => cg.NumberToChoose == 0 && !this.R_UsedChoiceGroups.Select(ucg => ucg.R_ChoiceGroup).Contains(cg)).ToList();
            foreach(ChoiceGroup cg in fullChoiceGroups){
                cg.Generate(this);
            }
        }

        public void AddClassLevel(ClassLevel classLevel){
            this.R_CharacterHasLevelsInClass.Add(classLevel);
            classLevel.R_Characters.Add(this);

            GenerateChoiceGroupUsage();
        }

        // public void EquipItem(Item item){
        //     if(this.R_CharacterHasBackpack.R_BackpackHasItems.Contains(item)){
        //         this.
        //     }
        // }

        [NotMapped]
        public int MaxHealth {
            get {
                int healthBase = this.R_CharacterHasLevelsInClass.Sum(cl => cl.HitPoints);
                int optional = this.AffectedByApprovedEffects.OfType<HitpointEffectInstance>().Where(hei => 
                    hei.EffectType.HitpointEffect == HitpointEffect.HitpointMaximumBonus
                ).Aggregate(0, (acc, valueEffectInstance) => acc + valueEffectInstance.DiceSet);
                return healthBase + optional;
            }
        }

        [NotMapped]
        public int TemporaryHitpoints {
            get => this.AffectedByApprovedEffects.OfType<HitpointEffectInstance>().Where(hei => 
                    hei.EffectType.HitpointEffect == HitpointEffect.TemporaryHitpoints
                ).Aggregate(0, (acc, valueEffectInstance) => acc + valueEffectInstance.DiceSet);
        }

        [NotMapped]
        public int ProficiencyBonus {
            get => this.R_CharacterHasLevelsInClass.Count / 5 + 2;
        }

        [NotMapped]
        public List<EffectInstance> ApprovedConditionalEffectInstances {get; set;} = [];

        public int AbilityValue(Ability ability){
            return this.AffectedByApprovedEffects.OfType<AbilityEffectInstance>().Where(aei => 
            aei.EffectType.AbilityEffect == AbilityEffect.Bonus &&
            aei.EffectType.AbilityEffect_Ability == ability
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

        public int SavingThrowValue(Ability ability){
            int returnValue = this.AffectedByApprovedEffects.OfType<SavingThrowEffectInstance>().Where(aei => 
            aei.EffectType.SavingThrowEffect == SavingThrowEffect.Bonus &&
            aei.EffectType.SavingThrowEffect_Ability == ability
            ).Aggregate(0, (acc, valueEffectInstance) => acc + valueEffectInstance.DiceSet) 
            + AbilityModifier(AbilityValue(ability));

            return returnValue + (SavingThrowProficiency(ability) ? ProficiencyBonus : 0);
        }

        public bool SavingThrowProficiency(Ability ability){
            return this.AffectedByApprovedEffects.OfType<SavingThrowEffectInstance>().Where(aei => 
            aei.EffectType.SavingThrowEffect == SavingThrowEffect.Proficiency &&
            aei.EffectType.SavingThrowEffect_Ability == ability
            ).Any();
        }

        [NotMapped]
        public int StrengthSavingThrowValue {
            get => SavingThrowValue(Ability.STRENGTH);
        }
        [NotMapped]
        public int DexteritySavingThrowValue {
            get => SavingThrowValue(Ability.DEXTERITY);
        }
        [NotMapped]
        public int ConstitutionSavingThrowValue {
            get => SavingThrowValue(Ability.CONSTITUTION);
        }
        [NotMapped]
        public int IntelligenceSavingThrowValue {
            get => SavingThrowValue(Ability.INTELLIGENCE);
        }
        [NotMapped]
        public int WisdomSavingThrowValue {
            get => SavingThrowValue(Ability.WISDOM);
        }
        [NotMapped]
        public int CharismaSavingThrowValue {
            get => SavingThrowValue(Ability.CHARISMA);
        }

        [NotMapped]
        public bool StrengthSavingThrowProficiency {
            get => SavingThrowProficiency(Ability.STRENGTH);
        }
        [NotMapped]
        public bool DexteritySavingThrowProficiency {
            get => SavingThrowProficiency(Ability.DEXTERITY);
        }
        [NotMapped]
        public bool ConstitutionSavingThrowProficiency {
            get => SavingThrowProficiency(Ability.CONSTITUTION);
        }
        [NotMapped]
        public bool IntelligenceSavingThrowProficiency {
            get => SavingThrowProficiency(Ability.INTELLIGENCE);
        }
        [NotMapped]
        public bool WisdomSavingThrowProficiency {
            get => SavingThrowProficiency(Ability.WISDOM);
        }
        [NotMapped]
        public bool CharismaSavingThrowProficiency {
            get => SavingThrowProficiency(Ability.CHARISMA);
        }

        public int SkillValue(Skill skill){
            int value = this.AffectedByApprovedEffects.OfType<SkillEffectInstance>().Where(aei => 
            aei.EffectType.SkillEffect == SkillEffect.Bonus &&
            aei.EffectType.SkillEffect_Skill == skill
            ).Aggregate(0, (acc, valueEffectInstance) => acc + valueEffectInstance.DiceSet);

            value += AbilityModifier(AbilityValue(Utils.SkillToAbility(skill)));

            var proficiencyBonus = this.ProficiencyBonus;
            if(SkillExpertise(skill)) proficiencyBonus *= 2;

            value += SkillProficiency(skill) ? proficiencyBonus : 0;

            return value;
        }

        public bool SkillProficiency(Skill skill){
            return this.AffectedByApprovedEffects.OfType<SkillEffectInstance>().Where(aei => 
            aei.EffectType.SkillEffect == SkillEffect.Proficiency &&
            aei.EffectType.SkillEffect_Skill == skill
            ).Any();
        }

        public bool SkillExpertise(Skill skill){
            return this.AffectedByApprovedEffects.OfType<SkillEffectInstance>().Where(aei => 
            aei.EffectType.SkillEffect == SkillEffect.UpgradeToExpertise &&
            aei.EffectType.SkillEffect_Skill == skill
            ).Any();
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

        [NotMapped]
        public int Initiative {
            get => this.DexterityModifier + this.AffectedByApprovedEffects.OfType<InitiativeEffectInstance>()
            .Sum(valueEffectInstance => valueEffectInstance.DiceSet);
        }

        [NotMapped]
        public int Speed {
            get {
                int speed = this.R_CharacterBelongsToRace.Speed;
                IEnumerable<MovementEffectInstance> multiplierData = this.AffectedByApprovedEffects
                                .OfType<MovementEffectInstance>()
                                .Where(m => m.EffectType.MovementEffect == MovementEffect.Multiplier);
                bool hasMultiplier = multiplierData.Any();
                int multiplier = 1;
                if(hasMultiplier){
                    multiplier = multiplierData
                                .Sum(m => m.DiceSet.flat);
                }

                int bonus = this.AffectedByApprovedEffects
                                .OfType<MovementEffectInstance>()
                                .Where(m => m.EffectType.MovementEffect == MovementEffect.Bonus)
                                .Sum(m => m.DiceSet.flat);

                return speed*multiplier+bonus;
            }
        }

        [NotMapped]
        public int ArmorClass {
            get {
                int baseArmorClass = 10;
                int dexterityModifier = this.DexterityModifier;
                IEnumerable<Apparel> apparel = this.R_EquippedItems.Where(ed => ed.R_Slots.Select(s => s.Type).Contains(SlotType.Apparel)).Select(aed => aed.R_Item).OfType<Apparel>();
                bool wearsHeavyArmor = apparel.Where(a => a.R_ItemInItemsFamily.ItemType == ItemType.HeavyArmor).Any();
                if(wearsHeavyArmor){
                    dexterityModifier = Math.Min(dexterityModifier, 0);
                }
                else{
                    bool wearsMediumArmor = apparel.Where(a => a.R_ItemInItemsFamily.ItemType == ItemType.MediumArmor).Any();
                    if(wearsMediumArmor){
                        dexterityModifier = Math.Min(dexterityModifier, 2);
                    }
                }
                int armorClassFromEffects = this.AffectedByApprovedEffects
                                .OfType<ArmorClassEffectInstance>()
                                .Sum(m => m.DiceSet);
                int armorClassFromItems = this.R_EquippedItems
                                .Where(ei => ei.R_Slots.Select(s => s.Type).Contains(SlotType.Apparel))
                                .Select(ei => ei.R_Item)
                                .OfType<Apparel>()
                                .Distinct()
                                .Sum(i => i.ArmorClass);

                return baseArmorClass + dexterityModifier + armorClassFromItems + armorClassFromEffects;
            }
        }

        [NotMapped]
        public DiceSet HitDiceTotal {
            get => new()
            {
                d20 = this.R_CharacterHasLevelsInClass.Sum(cl => cl.HitDie.d20),
                d12 = this.R_CharacterHasLevelsInClass.Sum(cl => cl.HitDie.d12),
                d10 = this.R_CharacterHasLevelsInClass.Sum(cl => cl.HitDie.d10),
                d8 = this.R_CharacterHasLevelsInClass.Sum(cl => cl.HitDie.d8),
                d6 = this.R_CharacterHasLevelsInClass.Sum(cl => cl.HitDie.d6),
                d4 = this.R_CharacterHasLevelsInClass.Sum(cl => cl.HitDie.d4),
                d100 = this.R_CharacterHasLevelsInClass.Sum(cl => cl.HitDie.d100),
                flat = 0,
            };
        }

        [NotMapped]
        public Size Size {
            get {
                var size = this.R_CharacterBelongsToRace.Size;
                var setSizes = this.AffectedByApprovedEffects
                                    .OfType<SizeEffectInstance>()
                                    .Where(ei => ei.EffectType.SizeEffect == SizeEffect.Change)
                                    .Select(ei => ei.EffectType.SizeEffect_SizeToSet)
                                    .ToList();
                if(setSizes.Count != 0){
                    size = setSizes.Max();
                }
                var sizeChanges = this.AffectedByApprovedEffects
                                    .OfType<SizeEffectInstance>()
                                    .Where(ei => ei.EffectType.SizeEffect == SizeEffect.Bonus)
                                    .Select(ei => ei.DiceSet.flat)
                                    .Sum();
                var result = ((int)size) + sizeChanges;
                if (Enum.IsDefined(typeof(Size), result))
                {
                    return (Size)result;
                }
                else
                {
                    if(result < 0) return Size.Tiny;
                    else return Size.Gargantuan;
                }
            }
        }

        [NotMapped]
        public List<ImmaterialResourceInstance> ImmaterialResources {
            get {
                return this.R_ImmaterialResourceInstances
                .Union(this.R_UsedChoiceGroups
                    .SelectMany(ucg => ucg.R_ResourcesGranted)
                )
                .Union(this.R_EquippedItems
                    .Select(equipData => equipData.R_Item)
                    .Distinct()
                    .SelectMany(item => item.R_ItemGrantsResources)
                )
                .Union(this.R_ImmaterialResourceInstances
                )
                .ToList();
            }
        }

        [NotMapped]
        public IEnumerable<EffectInstance> AffectedByApprovedEffects {
            get {
                return this.R_AffectedBy.Where(x => x.Conditional == false).Union(this.ApprovedConditionalEffectInstances);
            }
        }

        public int GetMaximumPreparedPowers(int classId){
            var maximum = this.R_CharacterHasLevelsInClass.Select(x => x.R_Class).Distinct().Where(c => c.Id == classId).Select(x => x.MaximumPreparedSpellsFormula).FirstOrDefault()?.Roll(this) ?? 0;
            if(maximum < 0) {
                return 0;
            }
            else {
                return maximum;
            }
        }


        public bool ItemFamilyProficiency(ItemFamily family){
            return this.AffectedByApprovedEffects.OfType<ProficiencyEffectInstance>().Where(pei => 
                        pei.R_GrantsProficiencyInItemFamilyId == family.Id || pei.ProficiencyEffectType.ItemType == family.ItemType
                        ).Any();
        }

        public void EquipItem(Item item, EquipmentSlot slot){
            item.Equip(this, slot);
        }
        public void UnequipItem(Item item){
            item.Unequip(this);
        }

        public bool HasAccess(int userId, out List<AccessLevels> accessLevels)
        {
            accessLevels = [];
            if (
                this.R_Campaign != null &&
                this.R_Campaign.R_OwnerId == userId)
            {
                accessLevels = [
                    AccessLevels.Read,
                    AccessLevels.EditDescriptiveFields, 
                    AccessLevels.EditEquipmentInBackpack, 
                    AccessLevels.EditEquippingItems, 
                    AccessLevels.EditLevelingUp, 
                    AccessLevels.EditEffects, 
                    AccessLevels.EditResources, 
                    AccessLevels.EditPowersKnown, 
                    AccessLevels.EditSpellbook, 
                ];
            }
            else if(
                this.R_Campaign != null &&
                this.R_OwnerId == userId
            ){
                accessLevels = [
                    AccessLevels.Read,
                    AccessLevels.EditDescriptiveFields, 
                    AccessLevels.EditEquippingItems, 
                    AccessLevels.EditLevelingUp, 
                    AccessLevels.EditSpellbook, 
                ];
            }
            else if(this.R_OwnerId == userId){
                accessLevels = [
                    AccessLevels.Read,
                    AccessLevels.EditDescriptiveFields, 
                    AccessLevels.EditEquipmentInBackpack, 
                    AccessLevels.EditEquippingItems, 
                    AccessLevels.EditLevelingUp, 
                    AccessLevels.EditEffects, 
                    AccessLevels.EditResources, 
                    AccessLevels.EditPowersKnown, 
                    AccessLevels.EditSpellbook, 
                ];
            }
            return accessLevels.Count > 0;
        }

        public enum AccessLevels{
            EditDescriptiveFields,
            EditEquipmentInBackpack,
            EditEquippingItems,
            EditLevelingUp,
            EditEffects,
            EditResources,
            EditPowersKnown,
            EditSpellbook,
            Read
        }
    }
}