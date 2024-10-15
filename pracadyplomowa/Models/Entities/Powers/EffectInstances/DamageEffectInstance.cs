using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;

namespace pracadyplomowa.Models.Entities.Powers;

public class DamageEffectInstance : ValueEffectInstance
{
    public DamageEffectType EffectType { get; set; } = new DamageEffectType();
    private DamageEffectInstance() : base("EF", 0){}
    public DamageEffectInstance(string name) : base(name, 0){}
    public DamageEffectInstance(DamageEffectBlueprint damageEffectBlueprint, Character roller, Character target) : base(damageEffectBlueprint, roller, target){
        EffectType = damageEffectBlueprint.DamageEffectType;
    }
    public DamageEffectInstance(DamageEffectInstance effectInstance) : base(effectInstance){
        EffectType  = effectInstance.EffectType;
    }
    public override EffectInstance Clone(){
        return new DamageEffectInstance(this);
    }
}