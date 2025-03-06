using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;

namespace pracadyplomowa.Models.Entities.Powers;

public class StatusEffectInstance : EffectInstance
{
    public StatusEffectType EffectType { get; set; } = new StatusEffectType();
    protected StatusEffectInstance() : base("EF"){}
    public StatusEffectInstance(string name) : base(name){}
    public StatusEffectInstance(StatusEffectBlueprint statusEffectBlueprint, Character target) : base(statusEffectBlueprint, target){
        EffectType = statusEffectBlueprint.StatusEffectType.Clone();
    }
    public StatusEffectInstance(StatusEffectInstance effectInstance) : base(effectInstance){
        EffectType  = effectInstance.EffectType.Clone();
    }
    public override EffectInstance Clone(){
        return new StatusEffectInstance(this);
    }
}