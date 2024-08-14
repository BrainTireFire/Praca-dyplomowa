using pracadyplomowa.Models.ComplexTypes.Effects;

namespace pracadyplomowa.Models.Entities.Powers;

public class MovementEffectInstance(Enums.EffectOptions.MovementEffect movementEffect) : ValueEffectInstance
{
    public MovementEffectType MovementEffectType { get; set; } = new MovementEffectType(movementEffect);
}