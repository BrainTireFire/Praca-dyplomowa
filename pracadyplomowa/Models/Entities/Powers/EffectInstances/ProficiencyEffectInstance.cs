using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Items;

namespace pracadyplomowa.Models.Entities.Powers;

public class ProficiencyEffectInstance : EffectInstance
{
    public ProficiencyEffectType ProficiencyEffectType{ get; set;} = null!;

        
    public virtual ItemFamily? R_GrantsProficiencyInItemFamily { get; set; }
    public int? GrantsProficiencyInItemFamilyId { get; set; }
}