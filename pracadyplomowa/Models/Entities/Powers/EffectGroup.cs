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
        //Ids and keys
        public int EffectCharacterConcentrateId { get; set; }
        public int ItemAffecteById { get; set; }
        public int ItemGiveEffectId { get; set; }
        
        public bool IsConstant { get; set; }
        public int DurationLeft { get; set; }
        public int DifficultyClassToBreak { get; set; }
        public Ability SavingThrow { get; set; }
        public bool SavingThrowRetakenEveryTurn { get; set; }

        //Relationships
        public virtual Character R_EffectCharacterConcentrate { get; set; }
        public virtual Item R_ItemAffecteBy { get; set; }
        public virtual Item R_ItemGiveEffect { get; set; }
        public virtual ICollection<Field> R_EffectOnField { get; set; }

    }
}   