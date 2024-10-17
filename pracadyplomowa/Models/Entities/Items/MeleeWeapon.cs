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
        public MeleeWeapon(string name, string description, ItemFamily itemFamily, int weight, DamageType damageType, DiceSet damageValue, DiceSet? versatileDamageValue) : base(name, description, itemFamily, weight, damageType, damageValue)
        {
            if(versatileDamageValue != null){
                this.VersatileDamageValue = versatileDamageValue;
            }
        }
        public MeleeWeapon(MeleeWeapon weapon) : base(weapon){
            this.Finesse = weapon.Finesse;
            this.Reach = weapon.Reach;
            if(weapon.Versatile){
#pragma warning disable CS8604 // Possible null reference argument.
                this.VersatileDamageValue = new DiceSet(weapon.VersatileDamageValue);
#pragma warning restore CS8604 // Possible null reference argument.
            }
        }

        public bool Finesse { get; set; }
        public bool Reach { get; set;}
        [NotMapped]
        public bool Versatile { get {
            return this.VersatileDamageValue != null;
        }}
        public DiceSet? VersatileDamageValue { get; set; }

        public override DiceSet GetDamageDiceSet(){ //TODO change this method so it returns different values if equipped in two hands for versatile weapons
            if(this.Versatile && this.R_EquipData != null && this.R_EquipData.R_Slots.Intersect(this.R_ItemIsEquippableInSlots).Count() == this.R_ItemIsEquippableInSlots.Count){
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                return VersatileDamageValue.getPersonalizedSet(Wielder);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            }
            return DamageValue.getPersonalizedSet(Wielder);
        }

        public override DiceSet GetAttackBonus()
        {
            if(Wielder == null){
            return base.GetAttackBonus();
            }
            else{
                int baseBonus = base.GetAttackBonus();
                int effectBonus = Wielder.AffectedByApprovedEffects
                .OfType<AttackRollEffectInstance>()
                .Where(ei => ei.EffectType.AttackRollEffect_Type == Enums.EffectOptions.AttackRollEffect_Type.Bonus 
                && ei.EffectType.AttackRollEffect_Source == Enums.EffectOptions.AttackRollEffect_Source.Weapon 
                && ei.EffectType.AttackRollEffect_Range == Enums.EffectOptions.AttackRollEffect_Range.Melee)
                .Select(ei => ei.DiceSet.getPersonalizedSet(Wielder))
                .Aggregate(new DiceSet(), (accumulator, value) => accumulator + value);
                int proficiencyBonus = R_EquipData != null && R_EquipData.R_Slots.Select(s => s.Type).Contains(Enums.SlotType.MainHand) ? (Finesse ? (Wielder.StrengthModifier > Wielder.DexterityModifier ? Wielder.StrengthModifier : Wielder.DexterityModifier) : Wielder.StrengthModifier) : 0;
                return baseBonus + effectBonus + proficiencyBonus;
            }
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