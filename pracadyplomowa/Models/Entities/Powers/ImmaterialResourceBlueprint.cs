using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.Entities.Powers
{
    public class ImmaterialResourceBlueprint : ObjectWithId
    {
        public string Name { get; set; } = null!;
        public RefreshType RefreshesOn { get; set; }

        //Relationships
        public ICollection<Power> R_PowersRequiringThis { get; set; } = [];

        public ICollection<ImmaterialResourceInstance> R_Instances {get; set; } = [];
        public ICollection<ImmaterialResourceAmount> R_Amounts {get; set; } = [];
        
    }
}