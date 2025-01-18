
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Expressions;
using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Campaign;
using pracadyplomowa.Models.Entities.Interfaces;
using pracadyplomowa.Models.Entities.Items;
using pracadyplomowa.Models.Entities.Powers;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;
using pracadyplomowa.Models.Entities.Powers.EffectInstances;
using pracadyplomowa.Models.Enums;
using pracadyplomowa.Models.Enums.EffectOptions;
using pracadyplomowa.Utility;

namespace pracadyplomowa.Models.Entities.Characters
{
    public class Character : ObjectWithOwner, ICaster
    {

        //Properties
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        private int _Hitpoints;
        public int Hitpoints { 
            get {
                return _Hitpoints;
            }
            set {
                if(value > MaxHealth) {
                    _Hitpoints = MaxHealth;}
                else{
                    _Hitpoints = value;
                }
            }
        }
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
        
        public void AddParticipanceData(ParticipanceData participanceData)
        {
            if (participanceData == null)
            {
                throw new ArgumentNullException(nameof(participanceData), "ParticipanceData cannot be null.");
            }

            if (!R_CharactersParticipatesInEncounters.Contains(participanceData))
            {
                R_CharactersParticipatesInEncounters.Add(participanceData);
                participanceData.UpdateCharacter(this); // Ensure the relationship is bidirectional
            }
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

        // [NotMapped]
        // public int TemporaryHitpoints {
        //     get => this.AffectedByApprovedEffects.OfType<HitpointEffectInstance>().Where(hei => 
        //             hei.EffectType.HitpointEffect == HitpointEffect.TemporaryHitpoints
        //         ).Aggregate(0, (acc, valueEffectInstance) => acc + valueEffectInstance.DiceSet);
        // }
        [NotMapped]
        private int _TemporaryHitpoints;
        public int TemporaryHitpoints {
            get {
                return _TemporaryHitpoints;
            }
            set {
                _TemporaryHitpoints = value;
                if(_TemporaryHitpoints < 0){
                    _TemporaryHitpoints = 0;
                }
            }
        }

        [NotMapped]
        public int ProficiencyBonus {
            get => this.R_CharacterHasLevelsInClass.Count / 5 + 2;
        }

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

        public bool SkillProficiencyNative(Skill skill){
            return this.NativeEffects.OfType<SkillEffectInstance>().Where(aei => 
            aei.EffectType.SkillEffect == SkillEffect.Proficiency &&
            aei.EffectType.SkillEffect_Skill == skill
            ).Any();
        }
        public bool SkillExpertise(Skill skill){
            bool hasUpgradeToExpertise =  this.AffectedByApprovedEffects.OfType<SkillEffectInstance>().Where(aei => 
            aei.EffectType.SkillEffect == SkillEffect.UpgradeToExpertise &&
            aei.EffectType.SkillEffect_Skill == skill
            ).Any();
            bool hasProficiency =  this.AffectedByApprovedEffects.OfType<SkillEffectInstance>().Where(aei => 
            aei.EffectType.SkillEffect == SkillEffect.Proficiency &&
            aei.EffectType.SkillEffect_Skill == skill
            ).Any();
            return hasProficiency && hasUpgradeToExpertise;
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
        public List<EffectInstance> AffectedByApprovedEffects {
            get {
                return this.AllEffects.Where(x => x.Conditional == false || x.ConditionalApproved).ToList();
            }
        }

        [NotMapped]
        public List<Power> AllPowers => R_PowersKnown.Union(this.R_PowersPrepared.SelectMany(x => x.R_PreparedPowers)).Union(this.R_EquippedItems.Select(x => x.R_Item).Distinct().SelectMany(x => x.R_EquipItemGrantsAccessToPower)).ToList();
        
        [NotMapped]
        public List<EffectInstance> AllEffects => R_AffectedBy
        .Union(NativeEffects)
        .Union(EffectsFromItems)
            // .Where(effect => effect is not SavingThrowEffectInstance || (effect is SavingThrowEffectInstance instance && instance.EffectType.SavingThrowEffect_Condition == null))
        .ToList();

        [NotMapped]
        public List<EffectInstance> NativeEffects => this.R_UsedChoiceGroups.SelectMany(cg => cg.R_EffectsGranted)
            // .Where(effect => effect is not SavingThrowEffectInstance || (effect is SavingThrowEffectInstance instance && instance.EffectType.SavingThrowEffect_Condition == null))
        .ToList();

        [NotMapped]
        public List<EffectInstance> EffectsFromItems => this.R_EquippedItems.SelectMany(ed => ed.R_Item.R_EffectsOnEquip)
        .ToList();

        public void ResolveAffectingEffects() {
            AllEffects.ForEach(x => x.Resolve());
        }

        public void StartNextTurn() {
            ResolveAffectingEffects();
            var effectGroupsForSavingThrowChecks = AllEffects.Where(e => e.R_OwnedByGroup != null && e.R_OwnedByGroup.DifficultyClassToBreak != null && e.R_OwnedByGroup.SavingThrow != null).Select(e => e.R_OwnedByGroup).Distinct().ToList();
            effectGroupsForSavingThrowChecks.ForEach(eg => {
                var result = SavingThrowRoll((Ability)eg!.SavingThrow!);
                if(result >= eg.DifficultyClassToBreak){
                    eg.Disperse();
                }
            });
            R_ConcentratesOn?.TickDuration();
        }

        public void StopConcentrating(){
            R_ConcentratesOn?.Disperse();
        }

        private bool HasCondition(Condition condition){
            return this.AffectedByApprovedEffects
                    .OfType<StatusEffectInstance>()
                    .Where(z => z.EffectType.StatusEffect == condition)
                    .Any();
        }

        [NotMapped]
        public bool IsBlinded {
            get {
                return HasCondition(Condition.Blinded);
            }
        }
        [NotMapped]
        public bool IsParalyzed {
            get {
                return HasCondition(Condition.Paralyzed);
            }
        }
        [NotMapped]
        public bool IsRestrained {
            get {
                return HasCondition(Condition.Restrained);
            }
        }
        [NotMapped]
        public bool IsStunned {
            get {
                return HasCondition(Condition.Stunned);
            }
        }
        [NotMapped]
        public bool IsUnconscious {
            get {
                return HasCondition(Condition.Unconscious);
            }
        }
        [NotMapped]
        public bool IsInvisible {
            get {
                return HasCondition(Condition.Invisible);
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
                    AccessLevels.Delete
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
            Read,
            Delete
        }

        public int DifficultyClass(Power power){
            return 8 + PowerCastBonus(power);
        }

        private int PowerCastBonus(Power power){ // attack roll bonus or to be used with +8 as difficulty class
            // Power power = this.AllPowers.First(p => p.Id == powerId);
            if(power.OverrideCastersDC && power.DifficultyClass != null){
                return (int)power.DifficultyClass;
            }
            List<Class> classes = power.R_AlwaysAvailableThroughChoiceGroupUsage
            .Where(cgu => cgu.R_ChoiceGroup.R_GrantedByClassLevel != null)
            .Select(cgu =>
    #pragma warning disable CS8602 // Dereference of a possibly null reference.
                cgu.R_ChoiceGroup.R_GrantedByClassLevel
    #pragma warning restore CS8602 // Dereference of a possibly null reference.
                .R_Class
            )
            .Union(
                power.R_ToPrepareThroughChoiceGroupUsage
                .Where(cgu => cgu.R_ChoiceGroup.R_GrantedByClassLevel != null)
                .Select(cgu =>
        #pragma warning disable CS8602 // Dereference of a possibly null reference.
                    cgu.R_ChoiceGroup.R_GrantedByClassLevel
        #pragma warning restore CS8602 // Dereference of a possibly null reference.
                    .R_Class
                )
            ).Distinct().ToList();
            if(classes.Count != 0){
                classes = this.R_CharacterHasLevelsInClass.Select(x => x.R_Class).Distinct().ToList();
            }
            return classes.Aggregate( 0, (max, next) => {
                int difficultyClass;
                if(next.SpellcastingAbility == null){
                    difficultyClass = 0;
                }
                difficultyClass = AbilityModifier((int)next.SpellcastingAbility) + ProficiencyBonus;
                return difficultyClass > max ? difficultyClass : max;
            }) + ProficiencyBonus;
            
        }

        public DiceSet AttackBonusDiceSet(AttackRollEffect_Range range, AttackRollEffect_Source source){
            return this.AffectedByApprovedEffects.OfType<AttackRollEffectInstance>()
            .Where(effect => effect.EffectType.AttackRollEffect_Range == range && effect.EffectType.AttackRollEffect_Source == source && effect.EffectType.AttackRollEffect_Type == AttackRollEffect_Type.Bonus)
            .Select(effect => effect.DiceSet)
            .Aggregate(new DiceSet(), (sum, next) => sum += next).getPersonalizedSet(this);
        }

        private int PowerAttackRoll(Encounter encounter, Character target, Power power){
            var range = power.IsRanged ? AttackRollEffect_Range.Ranged : AttackRollEffect_Range.Melee;
            var source = power.IsMagic ? AttackRollEffect_Source.Spell : AttackRollEffect_Source.Weapon;
            List<DiceSet.Dice> bonusRollResults = AttackBonusDiceSet(range, source).RollPrototype(false, false, null);

            //check for rerolls
            bool rerollEffectPresent = RerollOnAttackRoll(source, range, out int rerollLowerThan);
            //check for advantage
            bool advantage = AdvantageOnAttackRoll(encounter, target, source, range);
            //check for disadvantage
            bool disadvantage = DisadvantageOnAttackRoll(encounter, target, range);

            DiceSet.Dice baseRollResult = new DiceSet(){d20 = 1}.RollPrototype(advantage, disadvantage, rerollLowerThan).First();

            return bonusRollResults.Concat([baseRollResult]).Aggregate(0, (sum, current) => sum + current.result) + PowerCastBonus(power);
        }

        public int SavingThrowRoll(Ability ability){
            DiceSet bonusDiceSet = this.AffectedByApprovedEffects.OfType<SavingThrowEffectInstance>()
             .Where(effect => 
                effect.EffectType.SavingThrowEffect_Ability == ability && effect.EffectType.SavingThrowEffect == SavingThrowEffect.Bonus
             ).Select(effect => effect.DiceSet)
            .Aggregate(new DiceSet(), (sum, next) => sum += next).getPersonalizedSet(this);

            DiceSet baseDiceSet = new DiceSet(){d20 = 1};

            List<DiceSet.Dice> bonusRollResults = bonusDiceSet.RollPrototype(false, false, null);
            
            //check for rerolls
            List<SavingThrowEffectInstance> rerollEffectList = this.AffectedByApprovedEffects
                                                                    .OfType<SavingThrowEffectInstance>()
                                                                    .Where(x => x.EffectType.SavingThrowEffect == SavingThrowEffect.RerollLowerThan && x.EffectType.SavingThrowEffect_Ability == ability)
                                                                    .ToList();
            bool rerollEffectPresent = rerollEffectList.Count != 0;
            int? rerollLowerThan = null;
            if(rerollEffectPresent){
                rerollLowerThan = rerollEffectList.Aggregate(0, (maximum, current) => current.DiceSet.flat > maximum ? current.DiceSet.flat : maximum);
            }
            //check for advantage
            List<SavingThrowEffectInstance> advantageEffectList = this.AffectedByApprovedEffects
                                                                        .OfType<SavingThrowEffectInstance>()
                                                                        .Where(x => x.EffectType.SavingThrowEffect == SavingThrowEffect.Advantage && x.EffectType.SavingThrowEffect_Ability == ability)
                                                                        .ToList();
            bool advantageEffectPresent = advantageEffectList.Count != 0;
            //check for proficiency
            List<SavingThrowEffectInstance> proficiencyEffectList = this.AffectedByApprovedEffects
                                                                        .OfType<SavingThrowEffectInstance>()
                                                                        .Where(x => x.EffectType.SavingThrowEffect == SavingThrowEffect.Proficiency && x.EffectType.SavingThrowEffect_Ability == ability)
                                                                        .ToList();
            bool proficiencyEffectPresent = advantageEffectList.Count != 0;

            DiceSet.Dice baseRollResult = new DiceSet(){d20 = 1}.RollPrototype(advantageEffectPresent, false, rerollLowerThan).First();

            return bonusRollResults.Concat([baseRollResult]).Aggregate(0, (sum, current) => sum + current.result) + (proficiencyEffectPresent ? ProficiencyBonus : 0);
        }

        public Dictionary<int, HitType> CheckIfPowerHitSuccessfull(Encounter encounter, Power power, List<Character> targets){
            //retrieve data
            Dictionary<int, HitType> hitMap = [];

            foreach(var targetedCharacter in targets){
                if(power.PowerType == PowerType.Attack){
                    int roll = this.PowerAttackRoll(encounter, targetedCharacter, power);
                    HitType outcome = HitType.Miss;
                    if(roll == 1){
                        outcome = HitType.CriticalMiss;
                    }
                    if(roll == 20){
                        outcome = HitType.CriticalHit;
                    }
                    if(roll >= targetedCharacter.ArmorClass){
                        outcome = HitType.Hit;
                    }
                    hitMap.Add(
                        targetedCharacter.Id,
                        outcome
                    );
                }
                else if(power.PowerType == PowerType.Saveable){
                    int roll = targetedCharacter.SavingThrowRoll((Ability)power.SavingThrow);
                    HitType outcome = HitType.Miss;
                    if(roll <= this.DifficultyClass(power) && roll != 20){
                        outcome = HitType.Hit;
                    }
                    hitMap.Add(
                        targetedCharacter.Id,
                        outcome
                    );
                }
                else
                {
                    hitMap.Add(
                        targetedCharacter.Id,
                        HitType.Hit
                    );
                }
            }
            return hitMap;
        }

        public bool CheckIfUnarmedHitSuccessfull(Encounter encounter, Character target, out bool criticalHit){
            //check for rerolls
            bool rerollEffectPresent = RerollOnAttackRoll(AttackRollEffect_Source.Weapon, AttackRollEffect_Range.Melee, out int rerollLowerThan);

            //check for advantage
            bool advantage = AdvantageOnAttackRoll(encounter, target, AttackRollEffect_Source.Weapon, AttackRollEffect_Range.Melee);

            //check for disadvantage
            bool disadvantage = DisadvantageOnAttackRoll(encounter, target, AttackRollEffect_Range.Melee);

            //roll the dice
            List<DiceSet.Dice> bonusRollResults = this.AttackBonusDiceSet(AttackRollEffect_Range.Melee, AttackRollEffect_Source.Weapon).RollPrototype(false, false, null);
            DiceSet.Dice baseRollResult = new DiceSet(){d20 = 1}.RollPrototype(advantage, disadvantage, rerollLowerThan).First();

            //check for hit
            if(baseRollResult.result == 20){
                criticalHit = true;
                return true;
            }
            else{
                criticalHit = false;
                return bonusRollResults.Concat([baseRollResult]).Aggregate(0, (sum, current) => sum + current.result) >= target.ArmorClass;
            }
        }
        public bool CheckIfWeaponHitSuccessfull(Encounter encounter, Weapon weapon, Character target, AttackRollEffect_Range attacksRange, out bool criticalHit){
            if(weapon is MeleeWeapon meleeWeapon && !meleeWeapon.Thrown && attacksRange == AttackRollEffect_Range.Ranged){
                criticalHit = false;
                return false;
            }
            //check for rerolls
            bool rerollEffectPresent = RerollOnAttackRoll(AttackRollEffect_Source.Weapon, attacksRange, out int rerollLowerThan);

            //check for advantage
            bool advantage = AdvantageOnAttackRoll(encounter, target, AttackRollEffect_Source.Weapon, attacksRange);

            //check for disadvantage
            bool disadvantage = DisadvantageOnAttackRoll(encounter, target, attacksRange);

            //roll the dice
            List<DiceSet.Dice> bonusRollResults = weapon.GetTotalAttackBonus().RollPrototype(false, false, null);
            DiceSet.Dice baseRollResult = new DiceSet(){d20 = 1}.RollPrototype(advantage, disadvantage, rerollLowerThan).First();

            //check for hit
            if(baseRollResult.result == 20){
                criticalHit = true;
                return true;
            }
            else{
                criticalHit = false;
                return bonusRollResults.Concat([baseRollResult]).Aggregate(0, (sum, current) => sum + current.result) >= target.ArmorClass;
            }
        }

        public bool RerollOnAttackRoll(AttackRollEffect_Source source, AttackRollEffect_Range range, out int rerollLowerThan){
            List<AttackRollEffectInstance> rerollEffectList = this.AffectedByApprovedEffects
                                                                    .OfType<AttackRollEffectInstance>()
                                                                    .Where(x => x.EffectType.AttackRollEffect_Type == AttackRollEffect_Type.RerollLowerThan
                                                                    && x.EffectType.AttackRollEffect_Source == source
                                                                    && x.EffectType.AttackRollEffect_Range == range)
                                                                    .ToList();
            bool rerollEffectPresent = rerollEffectList.Count != 0;
            rerollLowerThan = 0;
            if(rerollEffectPresent){
                rerollLowerThan = rerollEffectList.Aggregate(0, (maximum, current) => current.DiceSet.flat > maximum ? current.DiceSet.flat : maximum);
            }
            return rerollEffectPresent;
        }

        public bool AdvantageOnAttackRoll(Encounter encounter, Character target, AttackRollEffect_Source source, AttackRollEffect_Range range){
            bool attackerHasEffectGrantingAdvantage = this.AffectedByApprovedEffects
                                                                        .OfType<AttackRollEffectInstance>()
                                                                        .Where(x => x.EffectType.AttackRollEffect_Type == AttackRollEffect_Type.Advantage && x.EffectType.AttackRollEffect_Source == source && x.EffectType.AttackRollEffect_Range == range)
                                                                        .Any();
            bool targetHasConditionGrantingAdvantage = target.AffectedByApprovedEffects
                                                                        .OfType<StatusEffectInstance>()
                                                                        .Where(z => new List<Condition>(){
                                                                            Condition.Blinded,
                                                                            Condition.Paralyzed,
                                                                            Condition.Restrained,
                                                                            Condition.Stunned,
                                                                            Condition.Unconscious
                                                                        }.Contains(z.EffectType.StatusEffect))
                                                                        .Any();
            bool attackerHasConditionGrantingAdvantage = target.AffectedByApprovedEffects
                                                                        .OfType<StatusEffectInstance>()
                                                                        .Where(z => new List<Condition>(){
                                                                            Condition.Invisible,
                                                                        }.Contains(z.EffectType.StatusEffect))
                                                                        .Any();
            //prone analysis
            bool isTargetProne = target.AffectedByApprovedEffects
                                        .OfType<StatusEffectInstance>()
                                        .Where(z => new List<Condition>(){
                                            Condition.Prone,
                                        }.Contains(z.EffectType.StatusEffect))
                                        .Any();
                                        
            var characterParticipanceData = encounter.R_Participances.First(z => z.R_CharacterId == this.Id);
            var targetParticipanceData = encounter.R_Participances.First(z => z.R_CharacterId == target.Id);
            bool isTargetAdjacentToAttacker = characterParticipanceData.IsAdjacentToParticipant(targetParticipanceData);

            return attackerHasEffectGrantingAdvantage || targetHasConditionGrantingAdvantage || attackerHasConditionGrantingAdvantage || (isTargetProne && isTargetAdjacentToAttacker);
        }

        public bool DisadvantageOnAttackRoll(Encounter encounter, Character target, AttackRollEffect_Range range){
            bool attackerHasConditionImposingDisadvantage = this.AffectedByApprovedEffects
                                                                        .OfType<StatusEffectInstance>()
                                                                        .Where(z => new List<Condition>(){
                                                                            Condition.Blinded,
                                                                            Condition.Restrained,
                                                                        }.Contains(z.EffectType.StatusEffect))
                                                                        .Any();
            bool targetHasConditionImposingDisadvantage = target.AffectedByApprovedEffects
                                                                        .OfType<StatusEffectInstance>()
                                                                        .Where(z => new List<Condition>(){
                                                                            Condition.Invisible,
                                                                        }.Contains(z.EffectType.StatusEffect))
                                                                        .Any();

            //prone analysis
            bool isTargetProne = target.AffectedByApprovedEffects
                                        .OfType<StatusEffectInstance>()
                                        .Where(z => new List<Condition>(){
                                            Condition.Prone,
                                        }.Contains(z.EffectType.StatusEffect))
                                        .Any();
                                        
            var characterParticipanceData = encounter.R_Participances.First(z => z.R_CharacterId == this.Id);
            var targetParticipanceData = encounter.R_Participances.First(z => z.R_CharacterId == target.Id);
            bool isTargetAdjacentToAttacker = characterParticipanceData.IsAdjacentToParticipant(targetParticipanceData);

            return attackerHasConditionImposingDisadvantage
                || targetHasConditionImposingDisadvantage
                || (isTargetProne && !isTargetAdjacentToAttacker)
                || (range == AttackRollEffect_Range.Ranged && isTargetAdjacentToAttacker);
        }

        public void GetExtraWeaponDamage(out DiceSet extraWeaponDamage){
            //check for extra weapon damage from character
            List<DamageEffectInstance> extraWeaponDamageEffectList = this.AffectedByApprovedEffects
                                                                    .OfType<DamageEffectInstance>()
                                                                    .Where(x => x.EffectType.DamageEffect == DamageEffect.ExtraWeaponDamage)
                                                                    .ToList();
            extraWeaponDamage = extraWeaponDamageEffectList.Aggregate(new DiceSet(), (sum, current) => sum + current.DiceSet.getPersonalizedSet(this));
        }

        public void GetAdditionalDamageOnWeaponStrike(out Dictionary<DamageType, DiceSet> damageTypeToDiceSetMap){
            //check for additional damage from character
            Dictionary<DamageType, List<DamageEffectInstance>> extraDamageEffectMap = AffectedByApprovedEffects
                                                                    .OfType<DamageEffectInstance>()
                                                                    .Where(x => x.EffectType.DamageEffect == DamageEffect.DamageDealt)
                                                                    .GroupBy(effectInstance => (DamageType)effectInstance.EffectType.DamageEffect_DamageType)
                                                                    .ToDictionary(g => g.Key, g => g.ToList());
            damageTypeToDiceSetMap = extraDamageEffectMap.ToDictionary(element => element.Key, element => element.Value.Aggregate(new DiceSet(), (sum, current) => sum + current.DiceSet.getPersonalizedSet(this)));
        }

        public WeaponHitResult ApplyWeaponHitEffects(Encounter encounter, Weapon weapon, Character target, bool criticalHit){
            //get weapon damage
            var weaponBaseDamage = weapon.GetBaseEquippedDamageDiceSet();
            var weaponEffectDamage = weapon.GetEffectsEquippedDamageDiceSet();

            //check for rerolls
            List<DamageEffectInstance> rerollEffectList = this.AffectedByApprovedEffects
                                                                    .OfType<DamageEffectInstance>()
                                                                    .Where(x => x.EffectType.DamageEffect == DamageEffect.RerollLowerThan)
                                                                    .ToList();
            bool rerollEffectPresent = rerollEffectList.Count != 0;
            int? rerollLowerThan = null;
            if(rerollEffectPresent){
                rerollLowerThan = rerollEffectList.Aggregate(0, (maximum, current) => current.DiceSet.flat > maximum ? current.DiceSet.flat : maximum);
            }
            //roll the dice
            List<DiceSet.Dice> baseWeaponDamageRollResult = weaponBaseDamage.RollPrototype(false, false, rerollLowerThan);
            Dictionary<DamageType, List<DiceSet.Dice>> weaponEffectDamageRollResults = weaponEffectDamage.ToDictionary(element => element.Key, element => element.Value.RollPrototype(false, false, null));
            if(criticalHit){
                baseWeaponDamageRollResult.ForEach(die => die.result = die.result *= 2);
                weaponEffectDamageRollResults.Values.ToList().ForEach(list => list.ForEach( die => die.result = die.result *= 2));
            }

            //calculate total damage of weapon type from weapon and wielder modifiers
            int totalWeaponDamage = baseWeaponDamageRollResult.Aggregate(0, (sum, current) => sum + current.result);

            //calculate total damage coming from effects applied to weapon and wielder
            Dictionary<DamageType, int> totalEffectDamage = weaponEffectDamageRollResults.ToDictionary(g => g.Key, g => g.Value.Aggregate(0, (sum, current) => sum + current.result));

            //check for targets damage resistance and apply damage
            var weaponHitResult = new WeaponHitResult();
            weaponHitResult.DamageTaken.Add(weapon.DamageType, target.TakeDamage(totalWeaponDamage, weapon.DamageType));
            foreach(var pair in totalEffectDamage){
                weaponHitResult.DamageTaken.Add(pair.Key, target.TakeDamage(pair.Value, pair.Key));
            }
            foreach (var power in weapon.R_PowersCastedOnHit){
                weapon.CheckIfPowerHitSuccessfull(encounter, power, [target]).TryGetValue(target.Id, out HitType outcome);
                weaponHitResult.PowerIdToHitStatus.Add(power.Id, outcome);
            }
            return weaponHitResult;
        }

        public class WeaponHitResult {
            public Dictionary<DamageType, int> DamageTaken {get; set;} = [];
            public Dictionary<int, HitType> PowerIdToHitStatus {get; set;} = [];
        }

        public int TakeDamage(int damage, DamageType damageType){ // returns damage actually taken after resistance/vulnerability analysis
            //check for targets damage resistance
            List<ResistanceEffectInstance> resistanceEffectInstances = this.AffectedByApprovedEffects
                                                                        .OfType<ResistanceEffectInstance>()
                                                                        .Where(x => x.EffectType.ResistanceEffect_DamageType == damageType && x.EffectType.ResistanceEffect == ResistanceEffect.Resistance)
                                                                        .ToList();
            bool resistanceEffectPresent = resistanceEffectInstances.Count != 0;

            //check for vulnerability to damage
            List<ResistanceEffectInstance> vulnerabilityEffectInstances = this.AffectedByApprovedEffects
                                                                        .OfType<ResistanceEffectInstance>()
                                                                        .Where(x => x.EffectType.ResistanceEffect_DamageType == damageType && x.EffectType.ResistanceEffect == ResistanceEffect.Vulnerability)
                                                                        .ToList();
            bool vulnerabilityEffectPresent = vulnerabilityEffectInstances.Count != 0;
            
            //check for immunity to damage
            List<ResistanceEffectInstance> immunityEffectInstances = this.AffectedByApprovedEffects
                                                                        .OfType<ResistanceEffectInstance>()
                                                                        .Where(x => x.EffectType.ResistanceEffect_DamageType == damageType && x.EffectType.ResistanceEffect == ResistanceEffect.Immunity)
                                                                        .ToList();
            bool immunityEffectPresent = immunityEffectInstances.Count != 0;

            if(immunityEffectPresent){
                return 0;
            }
            if(resistanceEffectPresent){
                damage /= 2;
            }
            if(vulnerabilityEffectPresent){
                damage *= 2;
            }

            int reducedDamage = damage - this.TemporaryHitpoints;
            if(reducedDamage < 0){
                reducedDamage = 0;
            }
            this.TemporaryHitpoints -= damage;
            this.Hitpoints -= reducedDamage;
            
            return damage;
        }

        [NotMapped]
        public int Level {
            get {
                return this.R_CharacterHasLevelsInClass.Count;
            }
        }

        public int GetLevelInClass(int classId){
            return this.R_CharacterHasLevelsInClass.Where(c => c.R_ClassId == classId).Count();
        }

        public Outcome ApplyPowerEffects(Power power, Dictionary<Character, HitType> targetsToHitSuccessMap, int? immaterialResourceLevel){
            // check for available immaterial resource
            if(power.R_UsesImmaterialResource != null){
                var immaterialResourceInstance = this.AllImmaterialResourceInstances.FirstOrDefault(x => x.R_Blueprint == power.R_UsesImmaterialResource && !x.NeedsRefresh && x.Level == immaterialResourceLevel);
                if(immaterialResourceInstance == null){
                    return Outcome.ImmaterialResourceUnavailable;
                }
                else{
                    //consume immaterial resource
                    immaterialResourceInstance.NeedsRefresh = true;
                }
            }
            // check whether material components present
            List<ItemCostRequirement> materialComponentsRequired = (power.R_ItemsCostRequirement?.OrderBy(req => req.Worth.GetValueInCopperPieces()).ToList()) ?? [];
            HashSet<Item> itemsSetAside = [];
            bool allMaterialComponentsFound = true;
            foreach(var requirement in materialComponentsRequired){
                var materialComponentFound = this.R_CharacterHasBackpack.R_BackpackHasItems.OrderBy(item => item.Price.GetValueInCopperPieces()).FirstOrDefault(
                    item => 
                    requirement.R_ItemFamilyId == item.R_ItemInItemsFamilyId
                    && requirement.Worth <= item.Price
                    && !itemsSetAside.Contains(item));
                if(materialComponentFound == null){
                    allMaterialComponentsFound = false;
                }
                else{
                    itemsSetAside.Add(materialComponentFound);
                }
            }
            if(!allMaterialComponentsFound){
                return Outcome.InsufficientMaterialComponents;
            }
            
            //configure effect group
            EffectGroup effectGroup = new();
            effectGroup.DurationLeft = power.Duration;
            effectGroup.IsConstant = false;
            if(power.PowerType == PowerType.Saveable && power.SavingThrowRoll == Enums.SavingThrowRoll.RetakenEveryTurn){
                effectGroup.DifficultyClassToBreak = DifficultyClass(power);
                effectGroup.SavingThrow = (Ability)power.SavingThrow;
            }
            effectGroup.Name = power.Name;
            if(power.RequiresConcentration){
                effectGroup.R_ConcentratedOnByCharacter = this;
            }
            List<EffectInstance> generatedEffects = [];
            foreach(Character target in targetsToHitSuccessMap.Keys){
                if(power.PowerType != PowerType.AuraCreator){
                    //generate effects
                    if (targetsToHitSuccessMap.TryGetValue(target, out var outcome))
                    {
                        foreach (EffectBlueprint effectBlueprint in power.R_EffectBlueprints)
                        {
                            bool shouldAdd = false;
                            if(power.UpcastBy == UpcastBy.NotUpcasted
                            || (power.UpcastBy == UpcastBy.ResourceLevel && immaterialResourceLevel >= effectBlueprint.Level)
                            || (power.UpcastBy == UpcastBy.CharacterLevel && this.Level >= effectBlueprint.Level)
                            || (power.UpcastBy == UpcastBy.ClassLevel && this.GetLevelInClass((int)power.R_ClassForUpcastingId) >= effectBlueprint.Level)
                            ){
                                if (power.PowerType == PowerType.Attack && outcome == HitType.Hit || outcome == HitType.CriticalHit)
                                {
                                    shouldAdd = true;
                                }
                                else if (power.PowerType == PowerType.Saveable)
                                {
                                    if ((outcome == HitType.Hit || outcome == HitType.CriticalHit) && !effectBlueprint.Saved)
                                    {
                                        shouldAdd = true;
                                    }
                                    else if ((outcome == HitType.Miss || outcome == HitType.CriticalMiss) && effectBlueprint.Saved && power.SavingThrowBehaviour == SavingThrowBehaviour.Modifies)
                                    {
                                        shouldAdd = true;
                                    }
                                }

                                if (shouldAdd)
                                {
                                    var effectInstance = effectBlueprint.Generate(this, target);
                                    if(outcome == HitType.CriticalHit && effectInstance is DamageEffectInstance damageEffectInstance){
                                        damageEffectInstance.CriticalHit = true;
                                    }
                                    generatedEffects.Add(effectInstance);
                                }
                            }
                        }
                    }
                }
                else{
                    effectGroup.GenerateAura(target, power.R_EffectBlueprints, (int)power.AuraSize!);
                }
                foreach(var effect in generatedEffects){
                    effectGroup.AddEffectOnCharacter(effect);
                }
            }
            return Outcome.Success;
        }

        [NotMapped]
        public List<ImmaterialResourceInstance> AllImmaterialResourceInstances{
            get {
                List<ImmaterialResourceInstance> immaterialResourceInstances = [];
                immaterialResourceInstances.AddRange(this.R_ImmaterialResourceInstances);
                foreach(var item in this.R_EquippedItems.Select(x => x.R_Item)){
                    immaterialResourceInstances.AddRange(item.R_ItemGrantsResources);
                }
                immaterialResourceInstances.Reverse();
                return immaterialResourceInstances;
            }
        }

        // public ICollection<Field> GetOccupiedFields(Encounter encounter){
        //     return encounter.R_Participances.Where(p => p.R_Character == this).FirstOrDefault()?.R_OccupiedFields ?? [];
        // }
        public ICollection<Field> GetOccupiedFieldsAlternative(Encounter encounter){
            var occupiedField = encounter.R_Participances.Where(p => p.R_Character == this).FirstOrDefault()?.R_OccupiedField ?? null;
            List<Field> occupiedFields = [];
            if(occupiedField != null){
                var size = occupiedField.R_OccupiedBy!.R_Character.Size;
                List<Tuple<int, int>> occupiedCoordinates = occupiedField.GetOccupiedCoordinates(size);
                foreach(var field in encounter.R_Board.R_ConsistsOfFields){
                    if(occupiedCoordinates.Contains(new Tuple<int, int>(field.PositionX, field.PositionY))){
                        occupiedFields.Add(field);
                    }
                }
            }
            return occupiedFields;
        }
        public void Move(Encounter encounter, Field field){
            var participance = encounter.R_Participances.First(x => x.R_CharacterId == this.Id);
            participance.R_OccupiedField = encounter.R_Board.R_ConsistsOfFields.First(x => x == field);
            field.R_OccupiedBy = participance;
        }

        public List<Field> CanTraversePath(List<Field> path){ //returns achieveable part of the path
            List<Field> enterable = [];
            foreach(var field in path){
                if(field.CanBeEnteredBy(this)){
                    enterable.Add(field);
                }
                else{
                    break;
                }
            }
            return enterable;
        }

        [NotMapped]
        public int TotalActionsPerTurn{
            get {
                return AffectedByApprovedEffects.OfType<ActionEffectInstance>().Where(x => x.EffectType.ActionEffect == ActionEffect.Action).Count() + 1;
            }
        }

        [NotMapped]
        public int TotaBonusActionsPerTurn{
            get {
                return AffectedByApprovedEffects.OfType<ActionEffectInstance>().Where(x => x.EffectType.ActionEffect == ActionEffect.BonusAction).Count() + 1;
            }
        }

        [NotMapped]
        public int TotaReactionsPerTurn{
            get {
                return AffectedByApprovedEffects.OfType<ActionEffectInstance>().Where(x => x.EffectType.ActionEffect == ActionEffect.Reaction).Count() + 1;
            }
        }

        [NotMapped]
        public int TotalMovementPerTurn{
            get {
                var bonus = AffectedByApprovedEffects.OfType<MovementEffectInstance>().Where(x => x.EffectType.MovementEffect == MovementEffect.Bonus).Select(x => x.DiceSet.flat).Sum();
                var multiplierEffects = AffectedByApprovedEffects.OfType<MovementEffectInstance>().Where(x => x.EffectType.MovementEffect == MovementEffect.Multiplier);
                var multiplier = multiplierEffects.Select(x => x.DiceSet.flat).Sum();
                var numberOfMultipliers = multiplierEffects.Count();
                return R_CharacterBelongsToRace.Speed * (multiplier - (numberOfMultipliers - 1)) + bonus;
            }
        }

        [NotMapped]
        public int TotalAttacksPerTurn{
            get {
                int baseNumber = 1;
                int maxFromEffects = AffectedByApprovedEffects.OfType<AttackPerAttackActionEffectInstance>().Where(x => x.EffectType.AttackPerActionEffect == AttackPerActionEffect.AttacksTotal).Select(x => x.DiceSet.flat).Max();
                int additional = AffectedByApprovedEffects.OfType<AttackPerAttackActionEffectInstance>().Where(x => x.EffectType.AttackPerActionEffect == AttackPerActionEffect.AdditionalAttacks).Select(x => x.DiceSet.flat).Sum();
                return Math.Max(baseNumber, maxFromEffects) + additional;
            }
        }
    }
}