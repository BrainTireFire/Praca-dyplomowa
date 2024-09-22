using System.Diagnostics;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;

namespace pracadyplomowa.Models.Entities.Powers;

public class ValueEffectInstance : EffectInstance
{
    public DiceSet DiceSet { get; set; }

    private ValueEffectInstance() : base("EF"){
        DiceSet = new DiceSet();
    }
    public ValueEffectInstance(string name, DiceSet diceSet) : base(name){
        DiceSet = diceSet;
    }
    public ValueEffectInstance(ValueEffectBlueprint blueprint, Character roller, Character target) : base(blueprint, target){
        if(blueprint.RollMoment == Enums.RollMoment.OnCast){
            DiceSet = blueprint.DiceSet.Roll(roller);
        }
        else if(blueprint.RollMoment == Enums.RollMoment.OnResolve){
            DiceSet = blueprint.DiceSet;
        }
        else{
            throw new UnreachableException();
        }
    }
}