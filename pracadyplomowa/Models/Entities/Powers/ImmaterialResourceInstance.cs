using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Items;

namespace pracadyplomowa.Models.Entities.Powers
{
    public class ImmaterialResourceInstance : ObjectWithId
    {

        public ImmaterialResourceInstance(){

        }
        public ImmaterialResourceInstance(ImmaterialResourceInstance resource){
            this.NeedsRefresh = resource.NeedsRefresh;
            this.Level = resource.Level;
            this.R_Blueprint = resource.R_Blueprint;
            this.R_BlueprintId = resource.R_BlueprintId;
        }
        public bool NeedsRefresh { get; set; }
        public int Level { get; set; }

        // Relationships
        public virtual Item? R_Item { get; set; }
        public int? R_ItemId { get; set; }

        public virtual ImmaterialResourceBlueprint R_Blueprint { get; set; } = null!;
        public virtual int R_BlueprintId { get; set; }


        public virtual Character? R_Character { get; set; }
        public int? R_CharacterId { get; set; }

        public virtual ChoiceGroupUsage? R_ChoiceGroupUsage { get; set; }
        public virtual int? R_ChoiceGroupUsageId { get; set; }

        [NotMapped]
        public string Source {
            get {
                if(R_Item != null){
                    return R_Item.Name;
                }
                else if(R_ChoiceGroupUsage != null){
                    return R_ChoiceGroupUsage.R_ChoiceGroup.Name;
                }
                else if(R_Character != null){
                    return "Custom";
                }
                else throw new UnreachableException();
            }
        }
    }
}