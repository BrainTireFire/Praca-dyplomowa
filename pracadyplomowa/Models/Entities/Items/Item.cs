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
        public string Name { get; set; } = null!;
        // public Purse Value { get; set; } = new Purse();
        public int Weight { get; set; }
        public string Description { get; set; } = null!;
        public bool IsSpellFocus { get; set; }

        //Relationship
        public virtual ICollection<EquipmentSlot> R_ItemIsEquippableInSlots { get; set; } = [];
        public virtual ItemFamily R_ItemInItemsFamily { get; set; } = null!;
        public int R_ItemInItemsFamilyId { get; set; }
        public virtual Backpack? R_BackpackHasItem { get; set; }
        public int? R_BackpackHasItemId { get; set; }
        public virtual EquipData? R_EquipData { get; set; }

        public virtual ICollection<ImmaterialResourceInstance> R_ItemGrantsResources { get; set; } = [];
        public virtual ICollection<EffectGroup> R_EffectGroupAffectedBy { get; set; } = [];
        public virtual ICollection<EffectGroup> R_EffectGroupFromItem { get; set; } = [];
        public virtual ICollection<ShopItem> R_ItemInShops { get; set; } = [];
        public virtual ICollection<EffectBlueprint> R_ItemCreateEffectsOnEquip { get; set; } = [];
        public virtual ICollection<Power> R_EquipItemGrantsAccessToPower { get; set; } = [];
    }
}