using pracadyplomowa.Models.ComplexTypes.Effects;

namespace pracadyplomowa.Models.Entities.Powers;

public class MovementEffectInstance : ValueEffectInstance
{
    public MovementEffectType MovementEffectType{ get; set; } = null!;
}