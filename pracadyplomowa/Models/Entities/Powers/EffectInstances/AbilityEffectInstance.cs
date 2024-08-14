using pracadyplomowa.Models.ComplexTypes.Effects;

namespace pracadyplomowa.Models.Entities.Powers;

public class AbilityEffectInstance(Enums.EffectOptions.AbilityEffect AbilityEffect, Enums.Ability AbilityEffect_Ability) : ValueEffectInstance
{
    public AbilityEffectType AbilityEffectType { get; set; } = new AbilityEffectType(AbilityEffect, AbilityEffect_Ability);
}