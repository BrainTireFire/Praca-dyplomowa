using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Models.Entities.Powers;

public class ValueEffectInstance : EffectInstance
{
    public DiceSet DiceSet { get; set; } = new DiceSet();
}