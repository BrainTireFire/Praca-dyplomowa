using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;

namespace pracadyplomowa.Models.Entities.Powers;

public class HitpointEffectInstance : ValueEffectInstance
{
    public HitpointEffectType EffectType { get; set; } = new HitpointEffectType();
    private HitpointEffectInstance() : base("EF", 0){}
    public HitpointEffectInstance(string name) : base(name, 0){}
    public HitpointEffectInstance(HitpointEffectBlueprint hitpointEffectBlueprint, Character? roller, Character target) : base(hitpointEffectBlueprint, roller, target){
        EffectType = hitpointEffectBlueprint.HitpointEffectType;
    }
    public HitpointEffectInstance(HitpointEffectInstance effectInstance) : base(effectInstance){
    }
    public override EffectInstance Clone(){
        return new HitpointEffectInstance(this);
    }

    public override void Resolve()
    {
        if(EffectType.HitpointEffect == Enums.EffectOptions.HitpointEffect.TemporaryHitpoints){
            int tempHitPoints;
            if(Roller == null){
                tempHitPoints = this.DiceSet.RollPrototype(false, false, null).Aggregate(0, (sum, next) => sum += next.result);
            }
            else{
                tempHitPoints = this.DiceSet.RollPrototype(Roller, false, false, null).Aggregate(0, (sum, next) => sum += next.result);
            }
            if(R_TargetedCharacter != null){
                this.R_TargetedCharacter.TemporaryHitpoints = tempHitPoints; // you can only have temporary hitpoints from a single source so we always overwrite the value during assignment
            }
        }
    }
}