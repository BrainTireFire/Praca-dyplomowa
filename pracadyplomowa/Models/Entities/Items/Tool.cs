using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Models.Entities.Items
{
    public class Tool : Item
    {
        protected Tool() : base(){
            
        }
        public Tool(string name, string description, ItemFamily itemFamily, int weight) : base(name, description, itemFamily, weight)
        {
        }
    }
}