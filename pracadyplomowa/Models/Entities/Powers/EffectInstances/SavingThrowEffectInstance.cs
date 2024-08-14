using pracadyplomowa.Models.ComplexTypes.Effects;

namespace pracadyplomowa.Models.Entities.Powers;

public class SavingThrowEffectInstance(Enums.EffectOptions.SavingThrowEffect savingThrowEffect, Enums.Ability savingThrowEffect_Ability) : ValueEffectInstance
{
    public SavingThrowEffectType SavingThrowEffectType { get; set; } = new SavingThrowEffectType(savingThrowEffect, savingThrowEffect_Ability);
}