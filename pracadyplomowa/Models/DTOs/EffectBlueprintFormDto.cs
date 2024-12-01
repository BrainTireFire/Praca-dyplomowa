using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.DTOs
{
    public class EffectBlueprintFormDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ResourceLevel { get; set; } // use this effect if Level value matches value selected by 
        public int ResourceAmount { get; set;}
        public bool SavingThrowSuccess { get; set; }
        public bool Conditional { get; set; } = false;
        public bool IsImplemented { get; set; } = true;
        public bool HasNoEffectInCombat { get; set; } = false;
        public string EffectType { get; set; }

        public class SubeffectFormDto {
            
        }
        public class ValueEffectBlueprintFormDto : EffectBlueprintFormDto{
            public ValueSubeffectBlueprintFormDto EffectTypeBody { get; set; }

            public class ValueSubeffectBlueprintFormDto : SubeffectFormDto {
                public RollMoment RollMoment { get; set; }
                public DiceSetFormDto value { get; set; }
                
                public class DiceSetFormDto {
                    public int d20 { get; set; }
                    public int d12 { get; set; }
                    public int d10 { get; set; }
                    public int d8 { get; set; }
                    public int d6 { get; set; }
                    public int d4 { get; set; }
                    public int d100 { get; set; }
                    public int flat {get; set;}
                    public List<AdditionalValueFormDto> additionalValues { get; set; } = [];

                    public class AdditionalValueFormDto {
                        public string AdditionalValueType;
                        public int? LevelsInClassId { get; set; }
                        public string? ClassName { get; set; }
                        public string? Ability {get; set;}
                        public string? Skill {get; set;}
                    }
                }
            }
        }
        

        public class MovementEffectBlueprintFormDto : ValueEffectBlueprintFormDto{
            public new MovementSubeffectBlueprintFormDto EffectTypeBody { get; set; }
            public class MovementSubeffectBlueprintFormDto : ValueSubeffectBlueprintFormDto {
                public MovementEffectType effectType { get; set; }
            }
        }

        

        public class ActionEffectBlueprintFormDto : ValueEffectBlueprintFormDto{
            public new ActionSubeffectBlueprintFormDto EffectTypeBody { get; set; }
            public class ActionSubeffectBlueprintFormDto : ValueSubeffectBlueprintFormDto {
                public ActionEffectType effectType { get; set; }
            }
        }

        public class SavingThrowEffectBlueprintFormDto : ValueEffectBlueprintFormDto {
            public new SavingThrowSubeffectBlueprintFormDto EffectTypeBody { get; set; }
            public class SavingThrowSubeffectBlueprintFormDto : ValueSubeffectBlueprintFormDto {
                public SavingThrowEffectType effectType { get; set; }

            }
        }

        public class AbilityEffectBlueprintFormDto : ValueEffectBlueprintFormDto {
            public new AbilitySubeffectBlueprintFormDto EffectTypeBody { get; set; }
            public class AbilitySubeffectBlueprintFormDto : ValueSubeffectBlueprintFormDto {
                public AbilityEffectType effectType { get; set; }

            }
        }

        public class SkillEffectBlueprintFormDto : ValueEffectBlueprintFormDto {
            public new SkillSubeffectBlueprintFormDto EffectTypeBody { get; set; }
            public class SkillSubeffectBlueprintFormDto : ValueSubeffectBlueprintFormDto {
                public SkillEffectType effectType { get; set; }

            }
        }

        public class ResistanceEffectBlueprintFormDto : EffectBlueprintFormDto {
            public new ResistanceSubeffectBlueprintFormDto EffectTypeBody { get; set; }
            public class ResistanceSubeffectBlueprintFormDto : SubeffectFormDto {
                public ResistanceEffectType effectType { get; set; }

            }
        }

        public class AttackRollEffectBlueprintFormDto : ValueEffectBlueprintFormDto {
            public new AttackRollSubeffectBlueprintFormDto EffectTypeBody { get; set; }
            public class AttackRollSubeffectBlueprintFormDto : ValueSubeffectBlueprintFormDto {
                public AttackRollEffectType effectType { get; set; }

            }
        }

        public class ArmorClassEffectBlueprintFormDto : ValueEffectBlueprintFormDto {
            public new ArmorClassSubeffectBlueprintFormDto EffectTypeBody { get; set; }
            public class ArmorClassSubeffectBlueprintFormDto : ValueSubeffectBlueprintFormDto {

            }
        }

        public class ProficiencyEffectBlueprintFormDto : EffectBlueprintFormDto {
            public new ProficiencySubeffectBlueprintFormDto EffectTypeBody { get; set; }
            public class ProficiencySubeffectBlueprintFormDto : SubeffectFormDto {
                public int? GrantsProficiencyInItemFamilyId { get; set; }
                public ProficiencyEffectType effectType { get; set;}
            }
        }

        public class HealingEffectBlueprintFormDto : ValueEffectBlueprintFormDto {
            public new HealingSubeffectBlueprintFormDto EffectTypeBody { get; set; }
            public class HealingSubeffectBlueprintFormDto : ValueSubeffectBlueprintFormDto {

            }
        }

        public class MagicEffectBlueprintFormDto : ValueEffectBlueprintFormDto {
            public new MagicSubeffectBlueprintFormDto EffectTypeBody { get; set; }
            public class MagicSubeffectBlueprintFormDto : ValueSubeffectBlueprintFormDto {

            }
        }

        public class SizeEffectBlueprintFormDto : ValueEffectBlueprintFormDto {
            public new SizeSubeffectBlueprintFormDto EffectTypeBody { get; set; }
            public class SizeSubeffectBlueprintFormDto : ValueSubeffectBlueprintFormDto {
                public SizeEffectType effectType { get; set; }

            }
        }

        public class InitiativeEffectBlueprintFormDto : ValueEffectBlueprintFormDto {
            public new InitiativeSubeffectBlueprintFormDto EffectTypeBody { get; set; }
            public class InitiativeSubeffectBlueprintFormDto : ValueSubeffectBlueprintFormDto {

            }
        }

        public class DamageEffectBlueprintFormDto : ValueEffectBlueprintFormDto {
            public new DamageSubeffectBlueprintFormDto EffectTypeBody { get; set; }
            public class DamageSubeffectBlueprintFormDto : ValueSubeffectBlueprintFormDto {
                public DamageEffectType effectType { get; set; }

            }
        }

        public class HitpointEffectBlueprintFormDto : ValueEffectBlueprintFormDto {
            public new HitpointSubeffectBlueprintFormDto EffectTypeBody { get; set; }
            public class HitpointSubeffectBlueprintFormDto : ValueSubeffectBlueprintFormDto {
                public HitpointEffectType effectType { get; set; }

            }
        }

        public class AttackPerAttackActionEffectBlueprintFormDto : ValueEffectBlueprintFormDto {
            public new AttackPerAttackActionSubeffectBlueprintFormDto EffectTypeBody { get; set; }
            public class AttackPerAttackActionSubeffectBlueprintFormDto : ValueSubeffectBlueprintFormDto {
                public AttackPerAttackActionEffectType effectType { get; set; }

            }
        }

        public class StatusEffectBlueprintFormDto : EffectBlueprintFormDto {
            public new StatusSubeffectBlueprintFormDto EffectTypeBody { get; set; }
            public class StatusSubeffectBlueprintFormDto : SubeffectFormDto {
                public StatusEffectType effectType { get; set;}
            }
        }

        public class MovementCostEffectBlueprintFormDto : EffectBlueprintFormDto {
            public new MovementCostSubeffectBlueprintFormDto EffectTypeBody { get; set; }
            public class MovementCostSubeffectBlueprintFormDto : SubeffectFormDto {
                public MovementCostEffectType effectType { get; set;}
            }
        }
    }

}