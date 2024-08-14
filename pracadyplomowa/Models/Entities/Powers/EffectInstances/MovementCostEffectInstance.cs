using pracadyplomowa.Models.ComplexTypes.Effects;

namespace pracadyplomowa.Models.Entities.Powers;

public class MovementCostEffectInstance(int movementCost_Multiplier) : EffectInstance
{
    public MovementCostEffectType MovementCostEffectType { get; set; } = new MovementCostEffectType(movementCost_Multiplier);
}