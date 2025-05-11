using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.Entities.Items
{
    public class MeleeWeapon: Weapon
    {
        protected MeleeWeapon() : base(){

        }
        public MeleeWeapon(string name, string description, ItemFamily itemFamily, int weight, DamageType damageType, DiceSet damageValue, DiceSet? versatileDamageValue) : this(name, description, itemFamily, weight, damageType, damageValue, versatileDamageValue, false, 0)
        {
        }
        public MeleeWeapon(string name, string description, ItemFamily itemFamily, int weight, DamageType damageType, DiceSet damageValue, DiceSet? versatileDamageValue, bool thrown, int range) : base(name, description, itemFamily, weight, damageType, damageValue, range)
        {
            if(versatileDamageValue != null){
                this.VersatileDamageValue = versatileDamageValue;
                this.Versatile = true;
            }
            this.Thrown = thrown;
        }
        public MeleeWeapon(MeleeWeapon weapon) : base(weapon){
            this.Finesse = weapon.Finesse;
            this.Reach = weapon.Reach;
            this.Thrown = weapon.Thrown;
            if(weapon.Versatile){
#pragma warning disable CS8604 // Possible null reference argument.
                this.VersatileDamageValue = new DiceSet(weapon.VersatileDamageValue);
#pragma warning restore CS8604 // Possible null reference argument.
            }
        }

        public bool Finesse { get; set; }
        public bool Reach { get; set;}
        public bool Thrown { get; set;}
        public bool Versatile { get; set;}
        public virtual DiceSet VersatileDamageValue { get; set; } = new DiceSet();
        public int VersatileDamageValueId { get; set; }
        protected override int GetAbilityBonus(){
            return R_EquipData != null ? (Finesse ? (Wielder.StrengthModifier > Wielder.DexterityModifier ? Wielder.StrengthModifier : Wielder.DexterityModifier) : Wielder.StrengthModifier) : 0;
        }

        public override DiceSet GetBaseEquippedDamageDiceSet(){ //TODO change this method so it returns different values if equipped in two hands for versatile weapons
            var diceSet = base.GetBaseEquippedDamageDiceSet();
            if(this.Versatile && this.R_EquipData != null && this.R_EquipData.R_Slots.Intersect(this.R_ItemIsEquippableInSlots).Count() == this.R_ItemIsEquippableInSlots.Count){
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                diceSet -= DamageValue.getPersonalizedSet(Wielder);
                diceSet += VersatileDamageValue.getPersonalizedSet(Wielder);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            }
            return diceSet;
        }

        public override DiceSet GetBaseEquippedAttackBonus(){
            return GetBaseEquippedAttackBonus_Base(Enums.EffectOptions.AttackRollEffect_Range.Melee);
        }
        public override DiceSet GetEffectRelatedUnequippedAttackBonus(){
            return GetEffectRelatedUnequippedAttackBonus_Base(Enums.EffectOptions.AttackRollEffect_Range.Melee);
        }
        public override DiceSet GetEffectRelatedEquippedAttackBonus(){
            return GetEffectRelatedEquippedAttackBonus_Base(Enums.EffectOptions.AttackRollEffect_Range.Melee);
        }

        public override Item Clone(){
            return new MeleeWeapon(this);
        }

        public void EquipVersatile(){
            if(!this.Versatile){
                throw new EquippingException("Weapon is not versatile");
            }
            var character = Wielder;
            if(character != null){
                this.EquipInAllSlots(character);
            }
            else{
                throw new EquippingException("Weapon is not equipped");
            }
        }
    }
}