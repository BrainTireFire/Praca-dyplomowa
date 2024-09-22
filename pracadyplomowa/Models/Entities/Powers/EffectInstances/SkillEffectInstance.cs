using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;

namespace pracadyplomowa.Models.Entities.Powers;

public class SkillEffectInstance : ValueEffectInstance
{
    public SkillEffectType SkillEffectType { get; set; } = new SkillEffectType();
    private SkillEffectInstance() : base("EF", 0){}
    public SkillEffectInstance(string name) : base(name, 0){}
    public SkillEffectInstance(SkillEffectBlueprint skillEffectBlueprint, Character roller) : base(skillEffectBlueprint, roller){
        SkillEffectType = skillEffectBlueprint.SkillEffectType;
    }
}