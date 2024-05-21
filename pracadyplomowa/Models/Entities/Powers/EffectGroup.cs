using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Items;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.Entities.Powers
{
    public class EffectGroup : ObjectWithId
    {
        public bool IsConstant { get; set; }
        public int DurationLeft { get; set; }
        public int DifficultyClassToBreak { get; set; }
        public Ability SavingThrow { get; set; }
        public bool SavingThrowRetakenEveryTurn { get; set; }


        public virtual Character Character { get; set; }
        public virtual Item ItemAffecteBy { get; set; }
        public virtual Item ItemGiveEffect { get; set; }
    }
}