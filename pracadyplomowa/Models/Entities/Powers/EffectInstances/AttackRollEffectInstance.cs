using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;

namespace pracadyplomowa.Models.Entities.Powers;

public class AttackRollEffectInstance : ValueEffectInstance
{
    public AttackRollEffectType AttackRollEffectType { get; set; } = new AttackRollEffectType();
    private AttackRollEffectInstance() : base("EF", 0){}
    public AttackRollEffectInstance(string name) : base(name, 0){}
    public AttackRollEffectInstance(AttackRollEffectBlueprint attackRollEffectBlueprint, Character roller, Character target) : base(attackRollEffectBlueprint, roller, target){
        AttackRollEffectType = attackRollEffectBlueprint.AttackRollEffectType;
    }
}