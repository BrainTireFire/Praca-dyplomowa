using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;

namespace pracadyplomowa.Models.Entities.Powers;

public class SizeEffectInstance : EffectInstance
{
    public SizeEffectType SizeEffectType { get; set; } = new SizeEffectType();
    private SizeEffectInstance() : base("EF"){}
    public SizeEffectInstance(string name) : base(name){}
    public SizeEffectInstance(SizeEffectBlueprint sizeEffectBlueprint) : base(sizeEffectBlueprint){
        SizeEffectType = sizeEffectBlueprint.SizeEffectType;
    }
}