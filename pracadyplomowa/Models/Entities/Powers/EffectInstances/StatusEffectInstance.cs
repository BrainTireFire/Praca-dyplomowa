using pracadyplomowa.Models.ComplexTypes.Effects;

namespace pracadyplomowa.Models.Entities.Powers;

public class StatusEffectInstance(Enums.Condition statusEffect) : EffectInstance
{
    public StatusEffectType StatusEffectType { get; set; } = new StatusEffectType(statusEffect);
}