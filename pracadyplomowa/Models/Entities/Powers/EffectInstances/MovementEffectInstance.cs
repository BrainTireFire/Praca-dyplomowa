using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;

namespace pracadyplomowa.Models.Entities.Powers;

public class MovementEffectInstance : ValueEffectInstance
{
    public MovementEffectType EffectType { get; set; } = new MovementEffectType();
    private MovementEffectInstance() : base("EF", 0){}
    public MovementEffectInstance(string name) : base(name, 0){}
    public MovementEffectInstance(MovementEffectBlueprint movementEffectBlueprint, Character? roller, Character target) : base(movementEffectBlueprint, roller, target){
    }
    public MovementEffectInstance(MovementEffectInstance effectInstance) : base(effectInstance){
        EffectType  = effectInstance.EffectType;
    }
    public override EffectInstance Clone(){
        return new MovementEffectInstance(this);
    }
}