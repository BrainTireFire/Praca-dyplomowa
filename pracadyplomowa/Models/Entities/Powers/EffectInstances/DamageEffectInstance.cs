using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;

namespace pracadyplomowa.Models.Entities.Powers;

public class DamageEffectInstance : ValueEffectInstance
{
    public DamageEffectType EffectType { get; set; } = new DamageEffectType();
    public bool CriticalHit {get; set;}
    private DamageEffectInstance() : base("EF", 0){}
    public DamageEffectInstance(string name) : base(name, 0){}
    public DamageEffectInstance(DamageEffectBlueprint damageEffectBlueprint, Character? roller, Character target) : base(damageEffectBlueprint, roller, target){
        EffectType = damageEffectBlueprint.DamageEffectType;
    }
    public DamageEffectInstance(DamageEffectInstance effectInstance) : base(effectInstance){
        EffectType  = effectInstance.EffectType;
    }
    public override EffectInstance Clone(){
        return new DamageEffectInstance(this);
    }

    public override void Resolve()
    {
        if(EffectType.DamageEffect == Enums.EffectOptions.DamageEffect.DamageTaken){
            int damage;
            var diceSetForResolve = new DiceSet(this.DiceSet);
            if(CriticalHit){
                diceSetForResolve.d100 *= 2;
                diceSetForResolve.d20 *= 2;
                diceSetForResolve.d12 *= 2;
                diceSetForResolve.d10 *= 2;
                diceSetForResolve.d8 *= 2;
                diceSetForResolve.d6 *= 2;
                diceSetForResolve.d4 *= 2;
            }
            if(Roller == null){
                damage = diceSetForResolve.RollPrototype(false, false, null).Aggregate(0, (sum, next) => sum += next.result);
            }
            else{
                damage = diceSetForResolve.RollPrototype(Roller, false, false, null).Aggregate(0, (sum, next) => sum += next.result);
            }
            if(R_TargetedCharacter != null && EffectType.DamageEffect_DamageType != null){
                this.R_TargetedCharacter.TakeDamage(damage, (Enums.DamageType)EffectType.DamageEffect_DamageType);
            }
        }
    }
}