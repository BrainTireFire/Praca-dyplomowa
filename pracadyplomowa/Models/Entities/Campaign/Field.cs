using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Powers;

namespace pracadyplomowa.Models.Entities.Campaign
{
    public class Field : ObjectWithId
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int PositionZ { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }


        //Relationships
        public virtual ICollection<Power> R_CasterPowers { get; set; } = [];
    }
}