using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Models.Entities.Items
{
    public class Tool : Item
    {

        //Relationship
        public virtual ICollection<Item> R_ToolForItems { get; set; } = [];
    }
}