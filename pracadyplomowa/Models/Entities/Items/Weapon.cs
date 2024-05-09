using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Models.Entities.Items
{
    public class Weapon : Item
    {
        public bool Finesse { get; set; }
        public bool Heavy { get; set; }
        public bool Light { get; set; }

        public virtual Item Item { get; set; }
    }
}