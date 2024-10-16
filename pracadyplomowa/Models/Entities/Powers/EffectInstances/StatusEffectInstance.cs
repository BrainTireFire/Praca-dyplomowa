using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;

namespace pracadyplomowa.Models.Entities.Powers;

public class StatusEffectInstance : EffectInstance
{
    public StatusEffectType EffectType { get; set; } = new StatusEffectType();
    private StatusEffectInstance() : base("EF"){}
    public StatusEffectInstance(string name) : base(name){}
    public StatusEffectInstance(StatusEffectBlueprint statusEffectBlueprint, Character target) : base(statusEffectBlueprint, target){
        EffectType = statusEffectBlueprint.StatusEffectType;
    }
    public StatusEffectInstance(StatusEffectInstance effectInstance) : base(effectInstance){
        EffectType  = effectInstance.EffectType;
    }
    public override EffectInstance Clone(){
        return new StatusEffectInstance(this);
    }
}