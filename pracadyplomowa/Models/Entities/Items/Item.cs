using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.RateLimiting;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Campaign;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers;
using pracadyplomowa.Models.Enums;

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

        public Item(Item item){
            Name = item.Name;
            Description = item.Description;
            Weight = item.Weight;
            IsSpellFocus = item.IsSpellFocus;
            R_ItemIsEquippableInSlots = [.. item.R_ItemIsEquippableInSlots];
            R_ItemInItemsFamily = item.R_ItemInItemsFamily;
            R_ItemInItemsFamilyId = item.R_ItemInItemsFamilyId;
            R_BackpackHasItem = item.R_BackpackHasItem;
            R_BackpackHasItemId = item.R_BackpackHasItemId;
            R_EquipData = item.R_EquipData;
            R_ItemGrantsResources = item.R_ItemGrantsResources.Select(x => new ImmaterialResourceInstance(x)).ToList();
            R_AffectedBy = item.R_AffectedBy.Select(x => x.Clone()).ToList();
            R_EffectsOnEquip = item.R_EffectsOnEquip.Select(x => x.Clone()).ToList();
            R_EquipItemGrantsAccessToPower = [.. item.R_EquipItemGrantsAccessToPower];
        }

        public string Name { get; set; } = null!;
        public int Weight { get; set; }
        public string Description { get; set; } = null!;
        public bool IsSpellFocus { get; set; }
        public bool OccupiesAllSlots { get; set;} // if false then can be placed in any of listed slots, if true then occupies all of them at once
        public CoinSack Price { get; set; }

        //Relationship
        public virtual List<EquipmentSlot> R_ItemIsEquippableInSlots { get; set; } = [];
        public virtual ItemFamily R_ItemInItemsFamily { get; set; } = null!;
        public int R_ItemInItemsFamilyId { get; set; }
        public virtual Backpack? R_BackpackHasItem { get; set; }
        public int? R_BackpackHasItemId { get; set; }
        public virtual EquipData? R_EquipData { get; set; }

        public virtual List<ImmaterialResourceInstance> R_ItemGrantsResources { get; set; } = [];
        public virtual List<EffectInstance> R_AffectedBy { get; set; } = [];
        // public virtual ICollection<EffectGroup> R_EffectGroupFromItem { get; set; } = [];
        public virtual List<EffectInstance> R_EffectsOnEquip { get; set; } = [];
        public virtual ICollection<ShopItem> R_ItemInShops { get; set; } = [];
        // public virtual ICollection<EffectBlueprint> R_ItemCreateEffectsOnEquip { get; set; } = [];
        public virtual ICollection<Power> R_EquipItemGrantsAccessToPower { get; set; } = [];

        public void Unequip(Character character){
            character.R_EquippedItems.RemoveAll(ed => ed.R_Character == character && ed.R_Item == this);
            this.R_EquipData = null;
        }
// powersAlwaysAvailable.Intersect(this.R_PowersAlwaysAvailable).Count() == powersAlwaysAvailable.Count
        public void Equip(Character character, EquipmentSlot slot){
            if(this.R_ItemIsEquippableInSlots.Where(s => s == slot).Any()){
                if(this.R_ItemIsEquippableInSlots.Intersect(character.R_CharacterBelongsToRace.R_EquipmentSlots).Count() == this.R_ItemIsEquippableInSlots.Count){
                    if(!this.OccupiesAllSlots){
                        EquipInSingleSlot(character, slot);
                    }
                    else{
                        EquipInAllSlots(character);
                    }
                }
                else{
                    throw new EquippingException("Race does not have necessary equipment slot");
                }
            }
            else{
                throw new EquippingException("Item is not equippable in this slot");
            }
        }

        protected void EquipInSingleSlot(Character character, EquipmentSlot slot){
            character.R_EquippedItems.Where(ed => ed.R_Slots.Contains(slot)).Select(ed => ed.R_Item).ToList().ForEach(i => i.Unequip(character));
            prepareEquipData(character).R_Slots.Add(slot);
        }

        protected void EquipInAllSlots(Character character){
            character.R_EquippedItems.Where(ed => ed.R_Slots.Intersect(this.R_ItemIsEquippableInSlots).Any()).Select(ed => ed.R_Item).ToList().ForEach(i => i.Unequip(character));
            prepareEquipData(character).R_Slots.AddRange(this.R_ItemIsEquippableInSlots);
        }

        private EquipData prepareEquipData(Character character){
            EquipData equipData = new()
            {
                R_Character = character,
                R_Item = this
            };
            character.R_EquippedItems.Add(equipData);
            this.R_EquipData = equipData;
            return equipData;
        }

        public virtual Item Clone(){
            return new Item(this);
        }

        public class EquippingException(string message) : Exception(message);
    }
}