using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;

namespace pracadyplomowa.Models.Entities.Powers.EffectInstances
{
    public class HealingEffectInstance : ValueEffectInstance
    {
        protected HealingEffectInstance() : base("EF", 0){}
        public HealingEffectInstance(string name) : base(name, 0){}
        public HealingEffectInstance(HealingEffectBlueprint initiativeEffectBlueprint, Character? roller, Character target) : base(initiativeEffectBlueprint, roller, target){
        }
        public HealingEffectInstance(HealingEffectInstance effectInstance) : base(effectInstance){
        }
        public override EffectInstance Clone(){
            return new HealingEffectInstance(this);
        }

        public override void Resolve(List<string> messages)
        {
            ResolutionMessage(messages);
            int healing;
            List<DiceSet.Dice> diceResult;
            if(Roller == null){
                diceResult = this.DiceSet.RollPrototype(false, false, null);
            }
            else{
                diceResult = this.DiceSet.RollPrototype(Roller, false, false, null);
            }
            if(R_TargetedCharacter != null){
                GenerateRollMessage(diceResult, "Healing", messages);
                healing = diceResult.Aggregate(0, (sum, next) => sum += next.result);
                if (healing < 0)
                {
                    healing = 0;
                }
                this.R_TargetedCharacter.Hitpoints += healing;
                messages.Add($"Healing {this.R_TargetedCharacter.Name} for {healing} points. Health total: {this.R_TargetedCharacter.Hitpoints}");
            }
        }
    }
}