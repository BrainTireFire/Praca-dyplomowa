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
        public Class R_Class { get; set; }
        public int R_ClassId { get; set; }
        public Character R_Character { get; set; }
        public int R_CharacterId { get; set; }
        public List<Power> R_PreparedPowers { get; set; } = [];
    }
}