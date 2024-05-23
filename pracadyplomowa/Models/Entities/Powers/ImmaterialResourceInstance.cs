using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Items;

namespace pracadyplomowa.Models.Entities.Powers
{
    public class ImmaterialResourceInstance : ObjectWithId
    {
        //Ids and keys
        public int ResourceGrantedToItemId { get; set; }
        
        public bool NeedsRefresh { get; set; }
        public int Level { get; set; }
        
        // Relationships
        public virtual Item R_ResourceGrantedToItem { get; set; }

        public virtual ImmaterialResourceBlueprint R_Blueprint { get; set; } = null!;
        public virtual int BlueprintId { get; set; }
    }
}