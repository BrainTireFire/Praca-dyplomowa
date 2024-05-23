using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Powers;

namespace pracadyplomowa.Models.Entities.Campaign
{
    public class Field : ObjectWithId
    {
        //Properties
        public int BoardId { get; set; }
        public int ParticipatesOnFieldId {get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int PositionZ { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }


        //Relationships
        public virtual ICollection<Power> R_CasterPowers { get; set; } = [];
        public virtual Board R_Board { get; set; }
        public virtual ParticipanceData R_ParticipatesOnField { get; set;}
        public virtual ICollection<EffectGroup> R_EffectOnField { get; set; }
    }
}