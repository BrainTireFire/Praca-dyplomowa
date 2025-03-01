using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;

namespace pracadyplomowa.Models.Entities.Powers;

public class MovementCostEffectInstance : EffectInstance
{
    public MovementCostEffectType EffectType { get; set; } = new MovementCostEffectType();
    private MovementCostEffectInstance() : base("EF"){}
    public MovementCostEffectInstance(string name) : base(name){}
    public MovementCostEffectInstance(MovementCostEffectBlueprint movementCostEffectBlueprint, Character target) : base(movementCostEffectBlueprint, target){
        EffectType = movementCostEffectBlueprint.MovementCostEffectType.Clone();
    }
    public MovementCostEffectInstance(MovementCostEffectInstance effectInstance) : base(effectInstance){
        EffectType  = effectInstance.EffectType.Clone();
    }
    public override EffectInstance Clone(){
        return new MovementCostEffectInstance(this);
    }
}