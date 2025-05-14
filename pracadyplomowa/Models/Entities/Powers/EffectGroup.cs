using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Items;
using pracadyplomowa.Models.Enums;
using pracadyplomowa.Models.Entities.Campaign;
using System.ComponentModel.DataAnnotations.Schema;

namespace pracadyplomowa.Models.Entities.Powers
{
    public class EffectGroup : ObjectWithId
    {

        public bool IsConstant { get; set; }
        public int? DurationLeft { get; set; }
        public int? DifficultyClassToBreak { get; set; }
        public Ability? SavingThrow { get; set; }
        public string Name {get; set;} = null!;

        //Relationships
        public virtual Character? R_ConcentratedOnByCharacter { get; set; }
        public int? R_ConcentratedOnByCharacterId { get; set; }
        // public virtual Item? R_ItemAffectedBy { get; set; }
        // public int? R_ItemAffectedById { get; set; }
        // public virtual Item? R_ItemGiveEffect { get; set; }
        // public int? R_ItemGiveEffectId { get; set; }

        public virtual ICollection<EffectInstance> R_OwnedEffects { get; set; } = [];

        // public virtual Aura? R_OriginatesFromAura { get; set; }
        // public int? R_OriginatesFromAuraId { get; set; }

        public virtual Aura? R_GeneratesAura { get; set; }
        public int? R_GeneratesAuraId { get; set; }
        public virtual ICollection<Field> R_EffectOnField { get; set; } = [];

        public void AddEffect(EffectInstance effectInstance){
            this.R_OwnedEffects.Add(effectInstance);
            effectInstance.R_OwnedByGroup = this;
        }

        public void GenerateAura(Character centeredAt, List<EffectBlueprint> effectBlueprints, int size){
            var aura = new Aura
                    {
                        R_CenteredAtCharacter = centeredAt,
                        R_EffectsOnCharactersInRange = effectBlueprints,
                        Size = size
                        
                    };
            this.R_GeneratesAura = aura;
            aura.R_GeneratedBy = this;
        }

        [NotMapped]
        public bool DeleteOnSave { get; set; }

        public void TickDuration(){
            if(!IsConstant){
                if(DurationLeft != null){
                    DurationLeft -= 1;
                }
                if(DurationLeft == null || DurationLeft < 0){
                    R_OwnedEffects.ToList().ForEach(e => {
                        e.Unlink();
                    });
                    Disperse();
                }
            }
        }

        public void Disperse() {
            if(R_ConcentratedOnByCharacter != null){
                R_ConcentratedOnByCharacter.R_ConcentratesOn = null;
                R_ConcentratedOnByCharacter.R_ConcentratesOnId = null;
            }
            DeleteOnSave = true;
            R_ConcentratedOnByCharacter = null;
            R_ConcentratedOnByCharacterId = null;
        }

        public void DisperseOnTarget(Character target) {
            var effects = R_OwnedEffects.Where(x => x.R_TargetedCharacter == target).ToList();
            foreach(var effect in effects){
                effect.Unlink();
            }
        }

    }
}