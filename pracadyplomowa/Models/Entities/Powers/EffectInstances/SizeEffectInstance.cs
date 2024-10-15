using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;

namespace pracadyplomowa.Models.Entities.Powers;

public class SizeEffectInstance : EffectInstance
{
    public SizeEffectType EffectType { get; set; } = new SizeEffectType();
    private SizeEffectInstance() : base("EF"){}
    public SizeEffectInstance(string name) : base(name){}
    public SizeEffectInstance(SizeEffectBlueprint sizeEffectBlueprint, Character target) : base(sizeEffectBlueprint, target){
        EffectType = sizeEffectBlueprint.SizeEffectType;
    }
    public SizeEffectInstance(SizeEffectInstance effectInstance) : base(effectInstance){
        EffectType  = effectInstance.EffectType;
    }
    public override EffectInstance Clone(){
        return new SizeEffectInstance(this);
    }
}