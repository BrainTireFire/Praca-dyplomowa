using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;

namespace pracadyplomowa.Models.Entities.Powers;

public class DamageEffectInstance : ValueEffectInstance
{
    public DamageEffectType DamageEffectType { get; set; } = new DamageEffectType();
    private DamageEffectInstance() : base("EF", 0){}
    public DamageEffectInstance(string name) : base(name, 0){}
    public DamageEffectInstance(DamageEffectBlueprint damageEffectBlueprint, Character roller, Character target) : base(damageEffectBlueprint, roller, target){
        DamageEffectType = damageEffectBlueprint.DamageEffectType;
    }
}