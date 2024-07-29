using pracadyplomowa.Models.ComplexTypes.Effects;

namespace pracadyplomowa.Models.Entities.Powers;

public class AttackRollEffectInstance : ValueEffectInstance
{
    public AttackRollEffectType AttackRollEffectType { get; set; } = new AttackRollEffectType();
}