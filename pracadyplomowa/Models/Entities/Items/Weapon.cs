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
        protected Weapon(string name, string description, ItemFamily itemFamily, int weight) : base(name, description, itemFamily, weight)
        {
        }

        public WeaponWeight WeaponWeight { get; set; }
        public DamageType DamageType { get; set; }
        public DiceSet DamageValue { get; set; } = new DiceSet();
        
        //Relationship
        public virtual ICollection<Power> R_PowersCastedOnHit { get; set; } = [];

        [NotMapped]
        protected Character? Wielder {
            get {
                return this.R_EquipData?.R_Character;
            }
        }
        
        public DiceSet GetDamageDiceSet(){
            return DamageValue.getPersonalizedSet(Wielder);
        }

        public virtual DiceSet GetAttackBonus(){
            if(Wielder == null){
                return 0;
            }
            else{
                return Wielder.ItemFamilyProficiency(this.R_ItemInItemsFamilyId) ? Wielder.ProficiencyBonus : 0;
            }
        }
    }
}