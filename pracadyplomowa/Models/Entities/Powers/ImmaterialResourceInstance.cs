using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Items;

namespace pracadyplomowa.Models.Entities.Powers
{
    public class ImmaterialResourceInstance : ObjectWithId
    {
        public bool NeedsRefresh { get; set; }
        public int Level { get; set; }
        

        public virtual Item Item { get; set; }
    }
}