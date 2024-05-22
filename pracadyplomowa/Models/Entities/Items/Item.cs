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
        //Ids and keys
        public int ItemIsEquippableInSlotId { get; set; }
        public int ItemHasToolId { get; set; }
        public int ItemAsApparelId { get; set; }
        public int ItemAsWeaponId { get; set; }
        public int ItemInItemsFamilyId { get; set; }
        public int BackpackHasItemId { get; set; }
        public int ItemToEquippedId { get; set; }
        
        public string Name { get; set; }
        // public Purse Value { get; set; } = new Purse();
        public int Weight { get; set; }
        public string Description { get; set; }
        public bool IsSpellFocus { get; set; }

        //Relationship
        public virtual EquipmentSlot R_ItemIsEquippableInSlot { get; set; }
        public virtual Tool R_ItemHasTool { get; set; }
        public virtual Apparel R_ItemAsApparel { get; set; }
        public virtual Weapon R_ItemAsWeapon { get; set; }
        public virtual ItemFamily R_ItemInItemsFamily { get; set; }
        public virtual Backpack R_BackpackHasItem { get; set; }
        public virtual EquipData R_ItemToEquipped { get; set; }
        
        public virtual ICollection<ImmaterialResourceInstance> R_ItemGrantsResources { get; set; } = [];
        public virtual ICollection<EffectGroup> R_EffectGroupAffectedBy { get; set; } = [];
        public virtual ICollection<EffectGroup> R_EffectGroupFromItem { get; set; } = [];
        public virtual ICollection<ShopItem> R_ItemInShops { get; set; } = [];
        public virtual ICollection<EffectBlueprint> R_ItemCreateEffectsOnEquip { get; set; } = [];
        public virtual ICollection<Power> R_EquipItemGrantsAccessToPower { get; set; } = [];
    }
}