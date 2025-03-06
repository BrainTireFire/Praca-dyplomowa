using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.Entities.Powers
{
    public class ImmaterialResourceBlueprint : ObjectWithOwner
    {
        public string Name { get; set; } = null!;
        public RefreshType RefreshesOn { get; set; }

        //Relationships
        public virtual ICollection<Power> R_PowersRequiringThis { get; set; } = [];

        public virtual ICollection<ImmaterialResourceInstance> R_Instances {get; set; } = [];
        public virtual ICollection<ImmaterialResourceAmount> R_Amounts {get; set; } = [];
        
        public bool HasEditAccess(int userId)
        {
            return this.R_OwnerId == userId;
        }
    }
}