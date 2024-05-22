using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Models.Entities.Items
{
    public class Apparel : Item
    {
        public int ArmorClass { get; set; }
        public bool StealthDisadvantage { get; set; }
        public int StrengthRequirement { get; set; }

        //Relationship
        public virtual Item R_ApperelIsItem { get; set; }
    }
}