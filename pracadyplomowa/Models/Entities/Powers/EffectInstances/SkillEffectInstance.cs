using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;
using pracadyplomowa.Models.Enums.EffectOptions;

namespace pracadyplomowa.Models.Entities.Powers;

public class SkillEffectInstance : ValueEffectInstance
{
    public SkillEffectType EffectType { get; set; } = new SkillEffectType();
    protected SkillEffectInstance() : base("EF", 0){}
    public SkillEffectInstance(string name) : base(name, 0){}
    public SkillEffectInstance(SkillEffectBlueprint skillEffectBlueprint, Character? roller, Character target) : base(skillEffectBlueprint, roller, target){
        EffectType = skillEffectBlueprint.SkillEffectType.Clone();
        if(EffectType.SkillEffect == SkillEffect.UpgradeToExpertise && !target.SkillProficiency(EffectType.SkillEffect_Skill)){
            throw new ExpertiseException("Invalid expertise selection");
        }
    }
    public SkillEffectInstance(SkillEffectInstance effectInstance) : base(effectInstance){
        EffectType  = effectInstance.EffectType.Clone();
    }
    public override EffectInstance Clone(){
        return new SkillEffectInstance(this);
    }

    public class ExpertiseException(string message) : Exception(message){}
}