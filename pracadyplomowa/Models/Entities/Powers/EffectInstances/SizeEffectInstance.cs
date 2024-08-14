using pracadyplomowa.Models.ComplexTypes.Effects;

namespace pracadyplomowa.Models.Entities.Powers;

public class SizeEffectInstance(Enums.EffectOptions.SizeEffect sizeEffect, Enums.Size sizeEffect_SizeToSet, int sizeBonus) : EffectInstance
{
    public SizeEffectType SizeEffectType { get; set; } = new SizeEffectType(sizeEffect, sizeEffect_SizeToSet, sizeBonus);
}