using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;

namespace pracadyplomowa.Models.Entities.Powers;

public class SizeEffectInstance : EffectInstance
{
    public SizeEffectType SizeEffectType { get; set; } = new SizeEffectType();
    private SizeEffectInstance() : base("EF"){}
    public SizeEffectInstance(string name) : base(name){}
    public SizeEffectInstance(SizeEffectBlueprint sizeEffectBlueprint, Character target) : base(sizeEffectBlueprint, target){
        SizeEffectType = sizeEffectBlueprint.SizeEffectType;
    }
}