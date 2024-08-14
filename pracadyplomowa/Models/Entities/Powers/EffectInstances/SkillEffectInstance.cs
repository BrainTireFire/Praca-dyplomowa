using pracadyplomowa.Models.ComplexTypes.Effects;

namespace pracadyplomowa.Models.Entities.Powers;

public class SkillEffectInstance(Enums.EffectOptions.SkillEffect skillEffect, Enums.Skill skillEffect_Skill) : ValueEffectInstance
{
    public SkillEffectType SkillEffectType { get; set; } = new SkillEffectType(skillEffect, skillEffect_Skill);
}