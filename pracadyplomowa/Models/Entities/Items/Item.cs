using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Campaign;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers;

namespace pracadyplomowa.Models.Entities.Items
{
    public class Item : ObjectWithOwner
    {
        public string Name { get; set; }
        // public Purse Value { get; set; } = new Purse();
        public int Weight { get; set; }
        public string Description { get; set; }
        public bool IsSpellFocus { get; set; }

        public virtual EquipmentSlot EquipmentSlot { get; set; }
        public virtual Tool Tool { get; set; }
        public virtual Apparel Apparel { get; set; }
        public virtual Weapon Weapon { get; set; }
        public virtual ItemFamily ItemFamily { get; set; }

        public virtual ICollection<EquipData> EquipDatas { get; set; } = [];
        public virtual ICollection<ImmaterialResourceInstance> ImmaterialResourceInstances { get; set; } = [];
        public virtual ICollection<EffectGroup> EffectGroupAffectedBy { get; set; } = [];
        public virtual ICollection<EffectGroup> EffectGroupFromItem { get; set; } = [];
        public virtual ICollection<ShopItem> ShopItems { get; set; } = [];
        public virtual ICollection<EffectBlueprint> EffectBlueprints { get; set; } = [];
        public virtual ICollection<Power> Powers { get; set; } = [];
    }
}