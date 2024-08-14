using pracadyplomowa.Models.ComplexTypes.Effects;

namespace pracadyplomowa.Models.Entities.Powers;

public class DamageEffectInstance(Enums.EffectOptions.DamageEffect damageEffect, Enums.DamageType damageEffect_DamageType) : ValueEffectInstance
{
    public DamageEffectType DamageEffectType { get; set; } = new DamageEffectType(damageEffect, damageEffect_DamageType);
}