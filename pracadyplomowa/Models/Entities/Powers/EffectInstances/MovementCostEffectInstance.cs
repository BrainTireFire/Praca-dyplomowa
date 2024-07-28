using pracadyplomowa.Models.ComplexTypes.Effects;

namespace pracadyplomowa.Models.Entities.Powers;

public class MovementCostEffectInstance : EffectInstance
{
    public MovementCostEffectType MovementCostEffectType { get; set; } = new MovementCostEffectType();
}