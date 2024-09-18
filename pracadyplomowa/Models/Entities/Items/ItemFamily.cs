using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Powers;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;
using pracadyplomowa.Models.Enums.EffectOptions;

namespace pracadyplomowa.Models.Entities.Items
{
    public class ItemFamily : ObjectWithId
    {
        public string Name { get; set; } = null!;
        // ProficiencyEffect ItemType {get; set;}

        //Relationship
        public virtual ICollection<Item> R_ItemFamilyInItems { get; set; } = [];

        public virtual ICollection<ProficiencyEffectBlueprint> R_ProficiencyGrantedByEffectBlueprint { get; set; } = [];
        public virtual ICollection<EffectInstance> R_ProficiencyGrantedByEffectInstance { get; set; } = [];

        public virtual ICollection<ItemCostRequirement> R_RequiredAmountsForPowers { get; set; } = [];
    }
}