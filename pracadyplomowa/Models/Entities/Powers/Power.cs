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
    public class Power : ObjectWithOwner
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public ActionType RequiredActionType { get; set; }
        public bool IsImplemented { get; set; }
        public CastableBy CastableBy { get; set; }
        public PowerType PowerType { get; set; }
        public TargetType TargetType { get; set; }

        public int Range { get; set; }
        public int MaxTargets { get; set; }
        public int MaxTargetsToExclude { get; set; }
        public int AreaSize { get; set; }
        public AreaShape AreaShape { get; set; }
        public int AuraSize { get; set; }
        public int DifficultyClass { get; set; }
        public Ability SavingThrow { get; set; }
        public bool RequiresConcentration { get; set; }
        public SavingThrowBehaviour SavingThrowBehaviour { get; set; }
        public SavingThrowRoll SavingThrowRoll { get; set; }
        public bool VerbalComponent { get; set; }
        public bool SomaticComponent { get; set; }

        // Relationships
        public virtual ICollection<Character> R_CharacterPreparedPowers { get; set; } = [];
        public virtual ICollection<Character> R_CharacterKnownsPowers { get; set; } = [];
        public virtual ICollection<Item> R_ItemsGrantingPower { get; set; } = [];
        public virtual ICollection<Weapon> R_WeaponsCastingOnHit { get; set; } = [];

        public virtual ICollection<Class> R_ClassesWithAccess { get; set; } = [];
        public virtual ImmaterialResourceBlueprint? R_UsesImmaterialResource { get; set; }
        public int? R_UsesImmaterialResourceId { get; set; }
        public virtual ICollection<ChoiceGroup> R_ChoiceGroups { get; set; } = [];

        public virtual ICollection<Field> R_FieldsCasting { get; set; } = [];

        public virtual ICollection<ItemCostRequirement> R_ItemsCostRequirement { get; set; } = [];

        public virtual ICollection<EffectBlueprint> R_EffectBlueprints { get; set; } = [];

        public virtual ICollection<Character> R_SpawnedCharacters { get; set; } = [];
    }
}