using pracadyplomowa.Models.ComplexTypes.Effects;

namespace pracadyplomowa.Models.Entities.Powers;

public class SkillEffectInstance : ValueEffectInstance
{
    public SkillEffectType SkillEffectType{ get; set; } = null!;
}