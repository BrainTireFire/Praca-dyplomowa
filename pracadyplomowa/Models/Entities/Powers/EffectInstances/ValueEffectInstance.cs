using System.Diagnostics;
using System.Text;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;

namespace pracadyplomowa.Models.Entities.Powers;

public abstract class ValueEffectInstance : EffectInstance
{
    public DiceSet DiceSet { get; set; }
    public Character? Roller { get; set; }

    private ValueEffectInstance() : base("EF"){
        DiceSet = new DiceSet();
    }
    public ValueEffectInstance(string name, DiceSet diceSet) : base(name){
        DiceSet = diceSet;
    }
    public ValueEffectInstance(ValueEffectBlueprint blueprint, Character? roller, Character target) : base(blueprint, target){
        if(blueprint.RollMoment == Enums.RollMoment.OnCast){
            DiceSet = blueprint.DiceSet.Roll(roller);
        }
        else if(blueprint.RollMoment == Enums.RollMoment.OnResolve){
            DiceSet = blueprint.DiceSet;
            Roller = roller;
        }
        else{
            throw new UnreachableException();
        }
    }

    public ValueEffectInstance(ValueEffectInstance effectInstance) : base(effectInstance){
        this.DiceSet = new DiceSet(effectInstance.DiceSet);
    }

    protected void GenerateRollMessage(List<DiceSet.Dice> diceResult, string rollName,  List<string> messages){
        StringBuilder messageBuilder = new();
        messageBuilder.Append($"{rollName} roll:");
        foreach(var dice in diceResult){
            messageBuilder.Append(" + " + dice.result + "(d" + dice.size + ")");
        }
        messageBuilder.Replace($"{rollName} roll: + ", $"{rollName} roll: ");
        messages.Add(messageBuilder.ToString());
    }
}