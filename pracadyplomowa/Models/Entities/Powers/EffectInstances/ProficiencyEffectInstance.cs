using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Items;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;

namespace pracadyplomowa.Models.Entities.Powers;

public class ProficiencyEffectInstance : EffectInstance
{
    //public ProficiencyEffectType ProficiencyEffectType { get; set; } = new ProficiencyEffectType();

        
    public virtual ItemFamily R_GrantsProficiencyInItemFamily { get; set; } = null!;
    public int R_GrantsProficiencyInItemFamilyId { get; set; }
    private ProficiencyEffectInstance() : base("EF"){}
    public ProficiencyEffectInstance(string name) : base(name){}
    public ProficiencyEffectInstance(ProficiencyEffectBlueprint proficiencyEffectBlueprint, Character target) : base(proficiencyEffectBlueprint, target){
        R_GrantsProficiencyInItemFamily = proficiencyEffectBlueprint.R_GrantsProficiencyInItemFamily;
        R_GrantsProficiencyInItemFamilyId = R_GrantsProficiencyInItemFamily.Id;
    }
}