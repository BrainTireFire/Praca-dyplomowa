using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.RateLimiting;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Campaign;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers;

namespace pracadyplomowa.Models.Entities.Items
{
    public class Item : ObjectWithOwner
    {
        protected Item(){

        }
        public Item(string name, string description, ItemFamily itemFamily, int weight)
        {
            Name = name;
            Weight = weight;
            Description = description;
            R_ItemInItemsFamily = itemFamily;
            R_ItemInItemsFamilyId = itemFamily.Id;
        }

        public string Name { get; set; } = null!;
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
        public virtual List<EffectInstance> R_AffectedBy { get; set; } = [];
        // public virtual ICollection<EffectGroup> R_EffectGroupFromItem { get; set; } = [];
        public virtual List<EffectInstance> R_EffectsOnEquip { get; set; } = [];
        public virtual ICollection<ShopItem> R_ItemInShops { get; set; } = [];
        // public virtual ICollection<EffectBlueprint> R_ItemCreateEffectsOnEquip { get; set; } = [];
        public virtual ICollection<Power> R_EquipItemGrantsAccessToPower { get; set; } = [];

        // private EquipData Equip(Character character){
        //     EquipData equipData = new()
        //     {
        //         R_Character = character,
        //         R_Item = this
        //     };
        //     return equipData;
        // }

        // public void Unequip(Character character){
        //     character.R_EquippedItems.ToList().RemoveAll(ed => ed.R_Character == character && ed.R_Item == this);
        //     this.R_EquipData = null;
        // }

        // public EquipData EquipInMainHand(Character character){
        //     if(this.R_ItemIsEquippableInSlots.Where(slot => slot.Type == Enums.SlotType.MainHand).Any()){
        //         character.R_EquippedItems.Where(ed => ed.Types.Contains(Enums.SlotType.MainHand)).Select(ed => ed.R_Item).ToList().ForEach(i => i.Unequip(character));
        //         EquipData equipData = Equip(character);
        //         equipData.Types.AddRange(this.R_ItemIsEquippableInSlots)
        //     }
        // }
    }
}