using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Models.Entities.Powers
{
    public class Aura : ObjectWithId
    {
        //Ids and keys
        public int CharacterAuraId { get; set; }
        
        public int Size { get; set; }
        
        // Relationships
        public virtual Character R_CharacterAura { get; set; }
    }
}