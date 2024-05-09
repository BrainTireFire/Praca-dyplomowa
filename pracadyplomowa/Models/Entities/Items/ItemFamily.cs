using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Models.Entities.Items
{
    public class ItemFamily : ObjectWithId
    {
        public string Name { get; set; }


        public virtual ICollection<Item> Items { get; set; } = [];
    }
}