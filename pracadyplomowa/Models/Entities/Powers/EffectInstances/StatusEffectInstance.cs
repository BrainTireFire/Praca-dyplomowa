using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;

namespace pracadyplomowa.Models.Entities.Powers;

public class StatusEffectInstance : EffectInstance
{
    public StatusEffectType StatusEffectType { get; set; } = new StatusEffectType();
    private StatusEffectInstance() : base("EF"){}
    public StatusEffectInstance(string name) : base(name){}
    public StatusEffectInstance(StatusEffectBlueprint statusEffectBlueprint) : base(statusEffectBlueprint){
        StatusEffectType = statusEffectBlueprint.StatusEffectType;
    }
}