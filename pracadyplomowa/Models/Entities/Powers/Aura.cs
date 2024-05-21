using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Models.Entities.Powers
{
    public class Aura : ObjectWithId
    {
        public int Size { get; set; }
        public virtual Character Character { get; set; }
    }
}