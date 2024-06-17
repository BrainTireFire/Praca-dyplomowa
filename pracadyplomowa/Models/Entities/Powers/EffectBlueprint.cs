using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Items;
using pracadyplomowa.Models.Enums;
using pracadyplomowa.Models.Enums.EffectOptions;

namespace pracadyplomowa.Models.Entities.Powers
{
    public class EffectBlueprint : ObjectWithId
    {

        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int ResourceLevel { get; set; }
        public bool Saved { get; set; }
        public EffectType EffectType { get; set; }

        //movement effect
        public MovementEffectType MovementEffectType{ get; set; }

        //saving throw effect
        public SavingThrowEffectType SavingThrowEffectType{ get; set; }

        //ability effect
        public AbilityEffectType AbilityEffectType{ get; set; }

        //skill effect
        public SkillEffectType SkillEffectType{ get; set; }

        //resistance effect
        public ResistanceEffectType ResistanceEffectType{ get; set;}

        //attack roll effect
        public AttackRollEffectType AttackRollEffectType{ get; set; }

        //armor class effect
        public ArmorClassEffectType ArmorClassEffectType{ get; set; }

        //proficiency effect
        public ProficiencyEffectType ProficiencyEffectType{ get; set;}
        // public ItemFamily ItemFamily { get; set; } //relationship

        //size effect
        public SizeEffectType SizeEffectType{ get; set; }

        //initiative effect
        public InitiativeEffectType InitiativeEffectType{ get; set;}

        //damage effect
        public DamageEffectType DamageEffectType{ get; set;}

        //hitpoint effect
        public HitpointEffectType HitpointEffectType{ get; set; }

        //healing effect
        public HealingEffectType HealingEffectType{ get; set;}

        //action effect
        public ActionEffectType ActionEffectType{ get; set; }

        //attacks per attack action effect
        public AttackPerAttackActionEffectType AttackPerAttackActionEffectType{get; set; }

        //magic item effect
        public MagicItemEffectType MagicItemEffectType{ get; set; }

        //status effect
        public StatusEffectType StatusEffectType{ get; set; }

        //movement cost effect 
        public MovementCostEffectType MovementCostEffectType{ get; set; }



        //Relationship
        public virtual Item? R_CreatedByEquipping { get; set; }
        public int R_CreatedByEquippingId { get; set; }

        public virtual Power? R_Power { get; set; }
        public int? R_PowerId { get; set; }

        public virtual Aura? R_CastedOnCharactersByAura { get; set; } = null;
        public int? R_CastedOnCharactersByAuraId { get; set; }
        public virtual Aura? R_CastedOnTilesByAura { get; set; } = null;
        public int? R_CastedOnTilesByAuraId { get; set; }

        public virtual ItemFamily? R_GrantsProficiencyInItemFamily { get; set; }
        public int? GrantsProficiencyInItemFamilyId { get; set; }

        public virtual ICollection<ChoiceGroup> R_ChoiceGroups { get; set; } = [];
    }
}