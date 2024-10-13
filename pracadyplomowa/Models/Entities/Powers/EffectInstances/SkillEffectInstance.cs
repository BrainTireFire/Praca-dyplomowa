using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;
using pracadyplomowa.Models.Enums.EffectOptions;

namespace pracadyplomowa.Models.Entities.Powers;

public class SkillEffectInstance : ValueEffectInstance
{
    public SkillEffectType SkillEffectType { get; set; } = new SkillEffectType();
    private SkillEffectInstance() : base("EF", 0){}
    public SkillEffectInstance(string name) : base(name, 0){}
    public SkillEffectInstance(SkillEffectBlueprint skillEffectBlueprint, Character roller, Character target) : base(skillEffectBlueprint, roller, target){
        SkillEffectType = skillEffectBlueprint.SkillEffectType;
        if(SkillEffectType.SkillEffect == SkillEffect.UpgradeToExpertise && !target.SkillProficiency(SkillEffectType.SkillEffect_Skill)){
            throw new ExpertiseException("Invalid expertise selection");
        }
    }

    public class ExpertiseException(string message) : Exception(message){}
}