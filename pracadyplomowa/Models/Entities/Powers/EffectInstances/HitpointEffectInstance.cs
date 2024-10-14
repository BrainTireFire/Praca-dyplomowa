using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;

namespace pracadyplomowa.Models.Entities.Powers;

public class HitpointEffectInstance : ValueEffectInstance
{
    public HitpointEffectType HitpointEffectType { get; set; } = new HitpointEffectType();
    private HitpointEffectInstance() : base("EF", 0){}
    public HitpointEffectInstance(string name) : base(name, 0){}
    public HitpointEffectInstance(HitpointEffectBlueprint hitpointEffectBlueprint, Character roller, Character target) : base(hitpointEffectBlueprint, roller, target){
        HitpointEffectType = hitpointEffectBlueprint.HitpointEffectType;
    }
}