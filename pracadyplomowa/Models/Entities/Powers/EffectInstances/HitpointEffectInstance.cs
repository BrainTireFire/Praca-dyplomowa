using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;

namespace pracadyplomowa.Models.Entities.Powers;

public class HitpointEffectInstance : ValueEffectInstance
{
    public HitpointEffectType EffectType { get; set; } = new HitpointEffectType();
    protected HitpointEffectInstance() : base("EF", 0){}
    public HitpointEffectInstance(string name) : base(name, 0){}
    public HitpointEffectInstance(HitpointEffectBlueprint hitpointEffectBlueprint, Character? roller, Character target) : base(hitpointEffectBlueprint, roller, target){
        EffectType = hitpointEffectBlueprint.HitpointEffectType.Clone();
    }
    public HitpointEffectInstance(HitpointEffectInstance effectInstance) : base(effectInstance){
        EffectType = effectInstance.EffectType.Clone();
    }
    public override EffectInstance Clone(){
        return new HitpointEffectInstance(this);
    }

    public override void Resolve(List<string> messages)
    {
        ResolutionMessage(messages);
        if(EffectType.HitpointEffect == Enums.EffectOptions.HitpointEffect.TemporaryHitpoints){
            int tempHitPoints;
            List<DiceSet.Dice> diceResult;
            if(Roller == null){
                diceResult = this.DiceSet.RollPrototype(false, false, null);
            }
            else{
                diceResult = this.DiceSet.RollPrototype(Roller, false, false, null);
            }
            if(R_TargetedCharacter != null){
                GenerateRollMessage(diceResult, "Temporary hitpoints", messages);
                tempHitPoints = diceResult.Aggregate(0, (sum, next) => sum += next.result);
                this.R_TargetedCharacter.TemporaryHitpoints = tempHitPoints; // you can only have temporary hitpoints from a single source so we always overwrite the value during assignment
                messages.Add($"Granting {this.R_TargetedCharacter} {tempHitPoints} temporary points.");
            }
        }
    }
}