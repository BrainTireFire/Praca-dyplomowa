using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Powers;

namespace pracadyplomowa.Models.Entities.Items
{
    public class ItemFamily : ObjectWithId
    {
        public string Name { get; set; } = null!;

        //Relationship
        public virtual ICollection<Item> R_ItemFamilyInItems { get; set; } = [];

        public virtual ICollection<EffectBlueprint> R_ProficiencyGrantedByEffectBlueprint { get; set; } = [];

        public virtual ICollection<ItemCostRequirement> R_RequiredAmountsForPowers { get; set; } = [];
    }
}