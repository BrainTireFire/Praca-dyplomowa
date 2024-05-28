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
        public int DurationLeft { get; set; }
        public int DifficultyClassToBreak { get; set; }
        public Ability SavingThrow { get; set; }
        public bool SavingThrowRetakenEveryTurn { get; set; }

        //Relationships
        public virtual Character? R_ConcentratedOnByCharacter { get; set; }
        public int? R_ConcentratedOnByCharacterId { get; set; }
        public virtual Item? R_ItemAffectedBy { get; set; }
        public int? R_ItemAffectedById { get; set; }
        public virtual Item? R_ItemGiveEffect { get; set; }
        public int? R_ItemGiveEffectId { get; set; }

        public virtual ICollection<EffectInstance> R_OwnedEffects { get; set; } = [];

        public virtual Aura? R_OriginatesFromAura { get; set; }
        public virtual int? R_OriginatesFromAuraId { get; set; }

        public virtual Aura? R_GeneratesAura { get; set; }
        public virtual int? GeneratesAuraId { get; set; }
        public virtual ICollection<Field> R_EffectOnField { get; set; } = [];

        public virtual ICollection<Character> R_TargetedCharacters { get; set; } = [];
    }
}