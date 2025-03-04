using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers;

namespace pracadyplomowa.Models.Entities.Characters
{
    public class PowerSelection: ObjectWithId
    {
        public virtual Class R_Class { get; set; }
        public int R_ClassId { get; set; }
        public virtual Character R_Character { get; set; }
        public int R_CharacterId { get; set; }
        public virtual List<Power> R_PreparedPowers { get; set; } = [];
    }
}