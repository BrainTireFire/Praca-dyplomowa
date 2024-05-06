using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Models.Entities.Items
{
    public class Item : ObjectWithOwner
    {
        public string Name { get; set; }
        public Purse Value { get; set; } = new Purse();
        public int Weight { get; set; }
        public string Description { get; set; }
        public bool IsSpellFocus { get; set; }
    }
}