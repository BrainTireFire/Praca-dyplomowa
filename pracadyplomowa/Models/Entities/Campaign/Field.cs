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
        public int R_BoardId { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int PositionZ { get; set; }
        public string Color { get; set; } = null!;
        public string? Description { get; set; }


        //Relationships
        public virtual ICollection<Power> R_CasterPowers { get; set; } = [];
        public virtual Board R_Board { get; set; } = null!;
        public virtual ParticipanceData R_OccupiedBy { get; set; } = null!;
        public int R_OccupiedById { get; set; }
        public virtual ICollection<EffectGroup> R_EffectOnField { get; set; } = [];
    }
}