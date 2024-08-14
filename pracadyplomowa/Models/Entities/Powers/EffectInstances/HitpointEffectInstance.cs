using pracadyplomowa.Models.ComplexTypes.Effects;

namespace pracadyplomowa.Models.Entities.Powers;

public class HitpointEffectInstance(Enums.EffectOptions.HitpointEffect hitpointEffect) : ValueEffectInstance
{
    public HitpointEffectType HitpointEffectType { get; set; } = new HitpointEffectType(hitpointEffect);
}