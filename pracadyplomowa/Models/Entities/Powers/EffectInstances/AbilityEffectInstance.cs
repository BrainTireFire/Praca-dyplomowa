using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;

namespace pracadyplomowa.Models.Entities.Powers;

public class AbilityEffectInstance : ValueEffectInstance
{
    public AbilityEffectType AbilityEffectType { get; set; } = new AbilityEffectType();

    private AbilityEffectInstance() : base("EF", 0){}
    public AbilityEffectInstance(string name) : base(name, 0){}
    public AbilityEffectInstance(AbilityEffectBlueprint abilityEffectBlueprint, Character roller, Character target) : base(abilityEffectBlueprint, roller, target){
        AbilityEffectType = abilityEffectBlueprint.AbilityEffectType;
    }
}