using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;

namespace pracadyplomowa.Models.Entities.Powers;

public class ResistanceEffectInstance : EffectInstance
{
    public ResistanceEffectType ResistanceEffectType { get; set; } = new ResistanceEffectType();
    private ResistanceEffectInstance() : base("EF"){}
    public ResistanceEffectInstance(string name) : base(name){}
    public ResistanceEffectInstance(ResistanceEffectBlueprint resistanceEffectBlueprint, Character target) : base(resistanceEffectBlueprint, target){
        ResistanceEffectType = resistanceEffectBlueprint.ResistanceEffectType;
    }
}