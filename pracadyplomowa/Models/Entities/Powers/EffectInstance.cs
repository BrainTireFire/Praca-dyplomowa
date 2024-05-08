using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Models.Entities.Powers
{
    public class EffectInstance : ObjectWithId
    {
        public virtual ICollection<Character> Characters { get; set; } = [];
    }
}