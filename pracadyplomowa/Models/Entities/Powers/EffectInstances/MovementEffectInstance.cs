using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;

namespace pracadyplomowa.Models.Entities.Powers;

public class MovementEffectInstance : ValueEffectInstance
{
    public MovementEffectType MovementEffectType { get; set; } = new MovementEffectType();
        private MovementEffectInstance() : base("EF", 0){}
        public MovementEffectInstance(string name) : base(name, 0){}
        public MovementEffectInstance(MovementEffectBlueprint movementEffectBlueprint, Character roller) : base(movementEffectBlueprint, roller){
        }
}