using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Campaign;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Items;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.Entities.Powers
{
    public class Power(string name, ActionType requiredActionType, CastableBy castableBy, PowerType powerType, TargetType targetType) : ObjectWithOwner
    {

        public string Name { get; set; } = name;
        public string Description { get; set; } = "";
        public ActionType RequiredActionType { get; set; } = requiredActionType;
        public bool IsImplemented { get; set; } = true;
        public bool IsMagic { get; set;} = true;
        public CastableBy CastableBy { get; set; } = castableBy;
        public PowerType PowerType { get; set; } = powerType;
        public TargetType TargetType { get; set; } = targetType;
        public bool IsRanged { get; set; } = false;
        public int? Range { get; set; }
        public int MaxTargets { get; set; } = 1;
        public int MaxTargetsToExclude { get; set; }
        public int? AreaSize { get; set; }
        public AreaShape? AreaShape { get; set; }
        public int? AuraSize { get; set; }
        public bool OverrideCastersDC { get; set; } = false;
        public int? DifficultyClass { get; set; }
        public Ability? SavingThrow { get; set; }
        public bool RequiresConcentration { get; set; }
        public SavingThrowBehaviour? SavingThrowBehaviour { get; set; }
        public SavingThrowRoll? SavingThrowRoll { get; set; }
        public bool VerbalComponent { get; set; }
        public bool SomaticComponent { get; set; }
        public int Duration {get; set;} = 1;
        public UpcastBy? UpcastBy {get; set;}
        public Class? R_ClassForUpcasting {get; set;}
        public int? R_ClassForUpcastingId {get; set;}

        // Relationships
        public virtual List<PowerSelection> R_CharacterPreparedPowers { get; set; } = []; // list of selected powers out of all available from 
        public virtual List<Character> R_CharacterKnownsPowers { get; set; } = []; // always available powers
        public virtual List<Item> R_ItemsGrantingPower { get; set; } = [];
        public virtual List<Weapon> R_WeaponsCastingOnHit { get; set; } = [];

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

    }
}