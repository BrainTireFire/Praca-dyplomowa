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
    public class EffectInstance : ObjectWithId
    {

        
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string SourceName { get; set; } = null!;
        public EffectType EffectType{ get; set; }
        
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
        public virtual EffectGroup R_OwnedByGroup { get; set; } = null!;
        public virtual int OwnedByGroupId { get; set; }
        public virtual ItemFamily? R_GrantsProficiencyInItemFamily { get; set; }
        public virtual int? GrantsProficiencyInItemFamilyId { get; set; }

    }
}