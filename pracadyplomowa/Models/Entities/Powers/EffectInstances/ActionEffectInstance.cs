using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;

namespace pracadyplomowa.Models.Entities.Powers;

public class ActionEffectInstance : ValueEffectInstance
{
    public ActionEffectType ActionEffectType { get; set; } = new ActionEffectType();
    private ActionEffectInstance() : base("EF", 0){}
    public ActionEffectInstance(string name) : base(name, 0){}
    public ActionEffectInstance(ActionEffectBlueprint actionEffectBlueprint, Character roller, Character target) : base(actionEffectBlueprint, roller, target){
        ActionEffectType = actionEffectBlueprint.ActionEffectType;
    }
}