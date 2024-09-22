using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;

namespace pracadyplomowa.Models.Entities.Powers;

public class ValueEffectInstance : EffectInstance
{
    public int Value { get; set; }

    private ValueEffectInstance() : base("EF"){}
    public ValueEffectInstance(string name, int value) : base(name){
        Value = value;
    }
    public ValueEffectInstance(ValueEffectBlueprint blueprint, Character roller) : base(blueprint){
        Value = blueprint.DiceSet.Roll(roller);
    }
}