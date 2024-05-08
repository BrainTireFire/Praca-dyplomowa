using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Models.Entities.Campaign
{
    public class ParticipanceData
    {
        public virtual ICollection<Character> Characters { get; set; } = [];
    }
}