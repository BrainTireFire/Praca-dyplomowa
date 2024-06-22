using pracadyplomowa.Models.ComplexTypes.Effects;

namespace pracadyplomowa.Models.Entities.Powers;

public class ActionEffectInstance : ValueEffectInstance
{
    public ActionEffectType ActionEffectType { get; set; } = null!;
}