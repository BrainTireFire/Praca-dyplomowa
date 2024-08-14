using pracadyplomowa.Models.ComplexTypes.Effects;

namespace pracadyplomowa.Models.Entities.Powers;

public class ActionEffectInstance(Enums.EffectOptions.ActionEffect actionEffect) : ValueEffectInstance
{
    public ActionEffectType ActionEffectType { get; set; } = new ActionEffectType(actionEffect);
}