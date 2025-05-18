using System.Text;
using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;

namespace pracadyplomowa.Models.Entities.Powers;

public class DamageEffectInstance : ValueEffectInstance
{
    public DamageEffectType EffectType { get; set; } = new DamageEffectType();
    public bool CriticalHit {get; set;}
    protected DamageEffectInstance() : base("EF", 0){}
    public DamageEffectInstance(string name) : base(name, 0){}
    public DamageEffectInstance(DamageEffectBlueprint damageEffectBlueprint, Character? roller, Character target) : base(damageEffectBlueprint, roller, target){
        EffectType = damageEffectBlueprint.DamageEffectType.Clone();
    }
    public DamageEffectInstance(DamageEffectInstance effectInstance) : base(effectInstance){
        EffectType  = effectInstance.EffectType.Clone();
    }
    public override EffectInstance Clone(){
        return new DamageEffectInstance(this);
    }

    public override void Resolve(List<string> messages)
    {
        if(EffectType.DamageEffect == Enums.EffectOptions.DamageEffect.DamageTaken){
            ResolutionMessage(messages);
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
            List<DiceSet.Dice> diceResult;
            if(Roller == null){
                diceResult = diceSetForResolve.RollPrototype(false, false, null);
            }
            else{
                diceResult = diceSetForResolve.RollPrototype(Roller, false, false, null);
            }
            if(R_TargetedCharacter != null && EffectType.DamageEffect_DamageType != null){
                GenerateRollMessage(diceResult, "Damage", messages);
                damage = diceResult.Aggregate(0, (sum, next) => sum += next.result);
                this.R_TargetedCharacter.TakeDamage(damage, (Enums.DamageType)EffectType.DamageEffect_DamageType, messages);
            }
        }
    }
}