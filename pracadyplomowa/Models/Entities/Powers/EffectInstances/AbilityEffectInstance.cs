using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;

namespace pracadyplomowa.Models.Entities.Powers;

public class AbilityEffectInstance : ValueEffectInstance
{
    public AbilityEffectType EffectType { get; set; } = new AbilityEffectType();

    private AbilityEffectInstance() : base("EF", 0){}
    public AbilityEffectInstance(string name) : base(name, 0){}
    public AbilityEffectInstance(AbilityEffectBlueprint abilityEffectBlueprint, Character? roller, Character target) : base(abilityEffectBlueprint, roller, target){
        EffectType = abilityEffectBlueprint.AbilityEffectType.Clone();
    }
    public AbilityEffectInstance(AbilityEffectInstance effectInstance) : base(effectInstance){
        EffectType  = effectInstance.EffectType.Clone();
    }
    public override EffectInstance Clone(){
        return new AbilityEffectInstance(this);
    }
}