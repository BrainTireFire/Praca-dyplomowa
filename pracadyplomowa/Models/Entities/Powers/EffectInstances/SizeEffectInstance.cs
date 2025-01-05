using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;

namespace pracadyplomowa.Models.Entities.Powers;

public class SizeEffectInstance : ValueEffectInstance
{
    public SizeEffectType EffectType { get; set; } = new SizeEffectType();
    private SizeEffectInstance() : base("EF", 0){}
    public SizeEffectInstance(string name) : base(name, 0){}
    public SizeEffectInstance(SizeEffectBlueprint sizeEffectBlueprint, Character? roller, Character target) : base(sizeEffectBlueprint, roller, target){
        EffectType = sizeEffectBlueprint.SizeEffectType;
    }
    public SizeEffectInstance(SizeEffectInstance effectInstance) : base(effectInstance){
        EffectType  = effectInstance.EffectType;
    }
    public override EffectInstance Clone(){
        return new SizeEffectInstance(this);
    }
}