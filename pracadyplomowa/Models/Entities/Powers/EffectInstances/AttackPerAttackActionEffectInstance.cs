using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;

namespace pracadyplomowa.Models.Entities.Powers;

public class AttackPerAttackActionEffectInstance : ValueEffectInstance
{
    public AttackPerAttackActionEffectType AttackPerAttackActionEffectType { get; set; } = new AttackPerAttackActionEffectType();
    private AttackPerAttackActionEffectInstance() : base("EF", 0){}
    public AttackPerAttackActionEffectInstance(string name) : base(name, 0){}
    public AttackPerAttackActionEffectInstance(AttackPerAttackActionEffectBlueprint attackPerAttackActionEffectBlueprint, Character roller, Character target) : base(attackPerAttackActionEffectBlueprint, roller, target){
        AttackPerAttackActionEffectType = attackPerAttackActionEffectBlueprint.AttackPerAttackActionEffectType;
    }
}