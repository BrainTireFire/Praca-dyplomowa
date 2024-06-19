using pracadyplomowa.Models.ComplexTypes.Effects;

namespace pracadyplomowa.Models.Entities.Powers;

public class StatusEffectInstance : EffectInstance
{
    public StatusEffectType StatusEffectType{ get; set; } = null!;
}