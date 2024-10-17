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
    public abstract class Weapon : Item
    {
        protected Weapon() : base(){
        }
        protected Weapon(string name, string description, ItemFamily itemFamily, int weight, DamageType damageType, DiceSet damageValue) : base(name, description, itemFamily, weight)
        {
            this.DamageValue = damageValue;
            this.DamageType = damageType;
        }

        public Weapon(Weapon weapon) : base(weapon){
            WeaponWeight = weapon.WeaponWeight;
            DamageType = weapon.DamageType;
            DamageValue = new DiceSet(weapon.DamageValue);
            R_PowersCastedOnHit = [.. weapon.R_PowersCastedOnHit];
        }

        public WeaponWeight WeaponWeight { get; set; }
        public DamageType DamageType { get; set; }
        public DiceSet DamageValue { get; set; } = null!;
        
        //Relationship
        public virtual ICollection<Power> R_PowersCastedOnHit { get; set; } = [];

        [NotMapped]
        protected Character? Wielder {
            get {
                return this.R_EquipData?.R_Character;
            }
        }
        
        public virtual DiceSet GetDamageDiceSet(){
            return DamageValue.getPersonalizedSet(Wielder);
        }

        public virtual DiceSet GetAttackBonus(){
            if(Wielder == null){
                return 0;
            }
            else{
                return Wielder.ItemFamilyProficiency(this.R_ItemInItemsFamily) ? Wielder.ProficiencyBonus : 0;
            }
        }
    }
}