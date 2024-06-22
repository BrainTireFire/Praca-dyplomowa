using pracadyplomowa.Models.ComplexTypes.Effects;

namespace pracadyplomowa.Models.Entities.Powers;

public class DamageEffectInstance : ValueEffectInstance
{
    public DamageEffectType DamageEffectType { get; set; } = null!;
}