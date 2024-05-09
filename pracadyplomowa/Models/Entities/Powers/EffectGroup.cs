using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Items;

namespace pracadyplomowa.Models.Entities.Powers
{
    public class EffectGroup : ObjectWithId
    {
        public virtual Character Character { get; set; }
        public virtual Item ItemAffecteBy { get; set; }
        public virtual Item ItemGiveEffect { get; set; }
    }
}