using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Items;
using pracadyplomowa.Models.Enums;
using pracadyplomowa.Models.Entities.Campaign;

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

        public void AddEffectOnCharacter(EffectInstance effectInstance){
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
    }
}