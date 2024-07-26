using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Items;

namespace pracadyplomowa.Models.Entities.Powers;

public class ProficiencyEffectInstance : EffectInstance
{
    public ProficiencyEffectType ProficiencyEffectType { get; set; } = new ProficiencyEffectType();

        
    public virtual ItemFamily R_GrantsProficiencyInItemFamily { get; set; } = null!;
    public int R_GrantsProficiencyInItemFamilyId { get; set; }
}