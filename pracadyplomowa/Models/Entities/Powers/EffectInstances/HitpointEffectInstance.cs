using pracadyplomowa.Models.ComplexTypes.Effects;

namespace pracadyplomowa.Models.Entities.Powers;

public class HitpointEffectInstance : ValueEffectInstance
{
    public HitpointEffectType HitpointEffectType { get; set; } = new HitpointEffectType();
}