using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Items;
using pracadyplomowa.Models.Enums;
using pracadyplomowa.Models.Enums.EffectOptions;

namespace pracadyplomowa.Models.Entities.Powers
{
    public class EffectInstance : ObjectWithId
    {

        
        public string Name { get; set; }
        public string Description { get; set; }
        public string SourceName { get; set; }
        public EffectType EffectType{ get; set; }
        //movement effect
        public MovementEffect MovementEffect { get; set; }
        public int MovementEffect_Value { get; set; }

        //saving throw effect
        public SavingThrowEffect SavingThrowEffect { get; set; }
        public int SavingThrowEffect_Value { get; set; }
        public Ability SavingThrowEffect_Ability { get; set; }

        //ability effect
        public AbilityEffect AbilityEffect { get; set; }
        public int AbilityEffect_Value { get; set; }
        public Ability AbilityEffect_Ability {get; set; }

        //skill effect
        public SkillEffect SkillEffect {get; set; }
        public int SkillEffect_Value { get; set; }
        public Skill SkillEffect_Skill {get; set; }

        //resistance effect
        public ResistanceEffect ResistanceEffect { get; set; }
        public DamageType ResistanceEffect_DamageType {get; set; }

        //attack roll effect
        public AttackRollEffect_Range AttackRollEffect_Range { get; set; }
        public AttackRollEffect_Source AttackRollEffect_Source { get; set; }
        public AttackRollEffect_Type AttackRollEffect_Type { get; set; }
        public string AttackRollEffect_Value { get; set;}

        //armor class effect
        public int ArmorClassEffect_Value { get; set; }

        //proficiency effect
        public ProficiencyEffect ProficiencyEffect { get; set; }
        public ItemFamily ItemFamily { get; set; } //relationship

        //size effect
        public SizeEffect SizeEffect { get; set; }
        public int SizeEffect_Value { get; set; }
        public Size SizeEffect_SizeToSet { get; set; }

        //initiative effect
        public int InitiativeEffect_Value { get; set; }

        //damage effect
        public DamageEffect DamageEffect { get; set; }
        public string DamageEffect_Value { get; set; }
        public DamageType DamageEffect_DamageType { get; set; }

        //hitpoint effect
        public HitpointEffect HitpointEffect { get; set; }
        public int HitpointEffect_Value { get; set; }

        //healing effect
        public int HealingEffec_Value { get; set; }

        //action effect
        public ActionEffect ActionEffect { get; set; }
        public int ActionEffect_Value { get; set; }

        //attacks per attack action effect
        public AttackPerActionEffect AttackPerActionEffect { get; set; }
        public int AttackPerActionEffect_Value { get; set; }

        //magic item effect
        public int MagicItemEffect_Value { get; set; }

        //status effect
        public Condition StatusEffect {get; set;}

        //movement cost effect 
        public int MovementCost_Multiplier { get; set; }

        //Relationship
        public virtual ICollection<Character> R_EffectAffectedOnCharacters { get; set; } = [];
    }
}