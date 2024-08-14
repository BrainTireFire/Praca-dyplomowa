using pracadyplomowa.Models.ComplexTypes.Effects;

namespace pracadyplomowa.Models.Entities.Powers;

public class ResistanceEffectInstance(Enums.EffectOptions.ResistanceEffect resistanceEffect, Enums.DamageType resistanceEffect_DamageType) : EffectInstance
{
    public ResistanceEffectType ResistanceEffectType { get; set; } = new ResistanceEffectType(resistanceEffect, resistanceEffect_DamageType);
}