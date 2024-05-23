using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.Entities.Powers
{
    public class ImmaterialResourceBlueprint : ObjectWithId
    {
        public string Name { get; set; }
        public RefreshType RefreshesOn { get; set; }
        
    }
}