using pracadyplomowa.Models.ComplexTypes.Effects;

namespace pracadyplomowa.Models.Entities.Powers;

public class AbilityEffectInstance : ValueEffectInstance
{
    public AbilityEffectType AbilityEffectType{ get; set; } = null!;
}