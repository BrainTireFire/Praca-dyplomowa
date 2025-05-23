using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Campaign;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Items;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.Entities.Powers
{
    public class Power : ObjectWithOwner, IValidatableObject
    {
        public Power(string name, ActionType requiredActionType, CastableBy castableBy, PowerType powerType, TargetType targetType) : base() {
            Name = name;
            RequiredActionType = requiredActionType;
            CastableBy = castableBy;
            PowerType = powerType;
            TargetType = targetType;
        }

        public string Name { get; set; }
        public string Description { get; set; } = "";
        private ActionType _RequiredActionType;
        public ActionType RequiredActionType {
            get {
                return _RequiredActionType;
            }
            set {
                if(CastableBy != CastableBy.Character){
                    _RequiredActionType = ActionType.None;
                }
                else{
                    _RequiredActionType = value;
                }
            }
        }
        public bool IsImplemented { get; set; } = true;
        public bool IsMagic { get; set;} = true;
        private CastableBy _CastableBy;
        public CastableBy CastableBy {
            get { 
                return _CastableBy;
            }
            set { 
                _CastableBy = value;
                if(value != CastableBy.Character){
                    this.RequiredActionType = ActionType.None;
                    // this.R_ItemsCostRequirement.Clear();
                    this.VerbalComponent = false;
                    this.SomaticComponent = false;
                    this.TargetType = TargetType.Character;
                }
            }
        }
        private PowerType _PowerType;
        public PowerType PowerType {
            get {
                return _PowerType;
            }
            set {
                if(CastableBy != CastableBy.Character && value == PowerType.AuraCreator){
                    _PowerType = PowerType.Attack; // substitute with another valid value since AuraCreator is only allowed for powers casted by character
                }
                else{
                    _PowerType = value;
                }
            }
        }
        private TargetType _TargetType;
        public TargetType TargetType { 
            get {
                return _TargetType;
            }
            set {
                if(PowerType == PowerType.AuraCreator){
                    _TargetType = TargetType.Caster; // substitute with another valid value
                }
                else if(CastableBy != CastableBy.Character) {
                    _TargetType = TargetType.Character;
                }
                else{
                    _TargetType = value;
                }
            }
        }
        public bool IsRanged { get; set; } = false;
        public int? Range { get; set; } = 5;
        public int MaxTargets { get; set; } = 1;
        public int MaxTargetsToExclude { get; set; }
        public int? AreaSize { get; set; }
        public AreaShape AreaShape { get; set; }
        public int? AuraSize { get; set; }
        public bool OverrideCastersDC { get; set; } = false;
        public int? DifficultyClass { get; set; }
        public Ability? SavingThrowAbility { get; set; }
        public bool RequiresConcentration { get; set; }
        public SavingThrowBehaviour? SavingThrowBehaviour { get; set; }
        public SavingThrowRoll? SavingThrowRoll { get; set; }
        private bool _VerbalComponent;
        public bool VerbalComponent {
            get {
                return _VerbalComponent;
            }
            set{
                if(CastableBy != CastableBy.Character){
                    _VerbalComponent = false;
                }
                else{
                    _VerbalComponent = value;
                }
            }
        }
        private bool _SomaticComponent;
        public bool SomaticComponent {
            get {
                return _SomaticComponent;
            }
            set{
                if(CastableBy != CastableBy.Character){
                    _SomaticComponent = false;
                }
                else{
                    _SomaticComponent = value;
                }
            }
        }
        public int Duration {get; set;} = 1;
        public UpcastBy UpcastBy {get; set;} = UpcastBy.NotUpcasted;
        // Relationships
        public virtual Class? R_ClassForUpcasting {get; set;}
        public int? R_ClassForUpcastingId {get; set;}

        public virtual List<PowerSelection> R_CharacterPreparedPowers { get; set; } = []; // list of selected powers out of all available from 
        public virtual List<Character> R_CharacterKnownsPowers { get; set; } = []; // always available powers
        public virtual List<Item> R_ItemsGrantingPower { get; set; } = [];
        // public virtual List<Weapon> R_WeaponsCastingOnHit { get; set; } = [];

        public virtual List<Class> R_ClassesWithAccess { get; set; } = [];
        public virtual ImmaterialResourceBlueprint? R_UsesImmaterialResource { get; set; }
        public int? R_UsesImmaterialResourceId { get; set; }
        public virtual List<ChoiceGroup> R_AlwaysAvailableThroughChoiceGroup { get; set; } = []; // means possibility of being granted through a choice group
        public virtual List<ChoiceGroup> R_ToPrepareThroughChoiceGroups { get; set; } = []; // means possibility of being granted through a choice group

        public virtual List<Field> R_FieldsCasting { get; set; } = [];

        public virtual List<ItemCostRequirement> R_ItemsCostRequirement { get; set; } = [];

        public virtual List<EffectBlueprint> R_EffectBlueprints { get; set; } = [];

        public virtual List<Character> R_SpawnedCharacters { get; set; } = [];
        public virtual List<ChoiceGroupUsage> R_AlwaysAvailableThroughChoiceGroupUsage { get; set; } = []; // means actual usage of a choice group
        public virtual List<ChoiceGroupUsage> R_ToPrepareThroughChoiceGroupUsage { get; set; } = []; // means actual usage of a choice group

        public List<string?> GetSourceNames(int usingCharacterId){
            var source = this.R_AlwaysAvailableThroughChoiceGroupUsage.Union(
                this.R_ToPrepareThroughChoiceGroupUsage
                ).Where(cgu => cgu.R_CharacterId == usingCharacterId).Select(cgu => cgu.R_ChoiceGroup.Name)
                .Union(this.R_ItemsGrantingPower.Where(i => i.R_EquipData?.R_CharacterId == usingCharacterId).Select(i => i.R_EquipData?.R_Item.Name)).ToList();
            if(source == null) return [];
            return source;
        }

        public bool RequiredResourceAvailable(Character caster, int resourceLevel) {
            return caster.AllImmaterialResourceInstances.Where(x => !x.NeedsRefresh && x.Level == resourceLevel && x.R_BlueprintId == this.R_UsesImmaterialResource?.Id).Any();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(CastableBy != CastableBy.Character && RequiredActionType != ActionType.None){
                yield return new ValidationResult(
                    "Powers not castable by 'Character' must have 'None' action requirement",
                    [nameof(CastableBy), nameof(RequiredActionType)]
                );
            }
            if(CastableBy != CastableBy.Character && VerbalComponent != false){
                yield return new ValidationResult(
                    "Powers not castable by 'Character' cannot have verbal component",
                    [nameof(CastableBy), nameof(VerbalComponent)]
                );
            }
            if(CastableBy != CastableBy.Character && SomaticComponent == true){
                yield return new ValidationResult(
                    "Powers not castable by 'Character' cannot have somatic component",
                    [nameof(CastableBy), nameof(SomaticComponent)]
                );
            }
            if(CastableBy != CastableBy.Character && PowerType == PowerType.AuraCreator){
                yield return new ValidationResult(
                    "Powers of type AuraCreator can be only castable by Character",
                    [nameof(CastableBy), nameof(PowerType)]
                );
            }
            if(TargetType != TargetType.Caster && PowerType == PowerType.AuraCreator){
                yield return new ValidationResult(
                    "Powers of type AuraCreator can only target Caster",
                    [nameof(CastableBy), nameof(PowerType)]
                );
            }
            // if(CastableBy != CastableBy.Character && R_ItemsCostRequirement.Count > 0){
            //     yield return new ValidationResult(
            //         "Powers not castable by 'Character' cannot have material component",
            //         [nameof(CastableBy), nameof(SomaticComponent)]
            //     );
            // }
            if(CastableBy != CastableBy.Character && UpcastBy != UpcastBy.NotUpcasted){
                yield return new ValidationResult(
                    "Powers not castable by 'Character' cannot be upcasted",
                    [nameof(CastableBy), nameof(UpcastBy)]
                );
            }
            // if(UpcastBy != UpcastBy.ClassLevel && (
            //     R_ClassForUpcasting == null || R_ClassForUpcastingId == null
            //     )
            // ){
            //     yield return new ValidationResult(
            //         "Powers not upcastable be class level cannot have class level defined",
            //         [nameof(CastableBy), nameof(SomaticComponent)]
            //     );
            // }
            if(UpcastBy == UpcastBy.ClassLevel && R_ClassForUpcasting == null && R_ClassForUpcastingId == null){
                yield return new ValidationResult(
                    "Powers upcastable by class level must have class defined",
                    [nameof(CastableBy), nameof(R_ClassForUpcastingId), nameof(R_ClassForUpcasting)]
                );
            }
            if(IsRanged && (CastableBy != CastableBy.Character || PowerType == PowerType.AuraCreator)){
                yield return new ValidationResult(
                    "Powers not castable by Character and aura creators cannot be ranged",
                    [nameof(CastableBy), nameof(IsRanged), nameof(PowerType)]
                );
            }
            if(IsRanged && !(Range > 0)){
                yield return new ValidationResult(
                    "Ranged powers must have positive range defined",
                    [nameof(IsRanged), nameof(Range)]
                );
            }
            if(Range % 5 != 0){
                yield return new ValidationResult(
                    "Range must be a multiple of 5",
                    [nameof(Range)]
                );
            }
            // if(PowerType == PowerType.AuraCreator || AreaShape != Enums.AreaShape.None && MaxTargets != 0){
            //     yield return new ValidationResult(
            //         "Aura creators and are of effect powers must have 0 max targets",
            //         [nameof(CastableBy), nameof(SomaticComponent)]
            //     );
            // }
            if(!(AreaSize > 0) && AreaShape != Enums.AreaShape.None){
                yield return new ValidationResult(
                    "Area of effect powers must have area size defined",
                    [nameof(AreaSize), nameof(AreaShape)]
                );
            }
            if(!(AuraSize > 0) && PowerType == PowerType.AuraCreator){
                yield return new ValidationResult(
                    "Aura creators must have positive aura size defined",
                    [nameof(CastableBy), nameof(SomaticComponent)]
                );
            }
            if(OverrideCastersDC && !(DifficultyClass > 0)){
                yield return new ValidationResult(
                    "Must specify difficulty class if power overrides casters DC",
                    [nameof(OverrideCastersDC), nameof(DifficultyClass)]
                );
            }
            if(PowerType == PowerType.Saveable && SavingThrowAbility == null){
                yield return new ValidationResult(
                    "Must specify ability for saveable power",
                    [nameof(PowerType), nameof(SavingThrowAbility)]
                );
            }
            if(PowerType == PowerType.Saveable && SavingThrowRoll == Enums.SavingThrowRoll.None){
                yield return new ValidationResult(
                    "Must specify saving throw roll moment for saveable power",
                    [nameof(PowerType), nameof(SavingThrowRoll)]
                );
            }
        }
        
        
        public bool HasEditAccess(int userId)
        {
            return this.R_OwnerId == userId;
        }
    }
}