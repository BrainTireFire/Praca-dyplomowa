using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Models.Entities.Campaign
{
    public class ParticipanceData
    {
        public int InitiativeOrder { get; set; }
        public bool IsSurprised { get; set; }
        public int NumberOfActionsTaken { get; set; }
        public int NumberOfBonusActionsTaken { get; set; }
        public int NumberOfAttacksTaken { get; set; }
        public int DistanceTraveled { get; set; }
        

        public virtual ICollection<Character> Characters { get; set; } = [];
    }
}