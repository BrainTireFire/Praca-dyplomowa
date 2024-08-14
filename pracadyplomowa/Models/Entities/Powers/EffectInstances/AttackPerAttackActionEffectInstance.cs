using pracadyplomowa.Models.ComplexTypes.Effects;

namespace pracadyplomowa.Models.Entities.Powers;

public class AttackPerAttackActionEffectInstance(Enums.EffectOptions.AttackPerActionEffect attackPerActionEffect) : ValueEffectInstance
{
    public AttackPerAttackActionEffectType AttackPerAttackActionEffectType { get; set; } = new AttackPerAttackActionEffectType(attackPerActionEffect);
}