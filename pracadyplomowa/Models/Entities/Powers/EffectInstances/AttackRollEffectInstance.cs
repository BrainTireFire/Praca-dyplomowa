using pracadyplomowa.Models.ComplexTypes.Effects;

namespace pracadyplomowa.Models.Entities.Powers;

public class AttackRollEffectInstance(Enums.EffectOptions.AttackRollEffect_Range attackRollEffect_Range, Enums.EffectOptions.AttackRollEffect_Source attackRollEffect_Source, Enums.EffectOptions.AttackRollEffect_Type attackRollEffect_Type) : ValueEffectInstance
{
    public AttackRollEffectType AttackRollEffectType { get; set; } = new AttackRollEffectType(attackRollEffect_Range, attackRollEffect_Source, attackRollEffect_Type);
}