using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.Entities.Items
{
    public class RangedWeapon : Weapon
    {
        protected RangedWeapon() : base(){
            
        }
        public RangedWeapon(string name, string description, ItemFamily itemFamily, int weight, DamageType damageType, DiceSet damageValue, int range, bool loaded) : base(name, description, itemFamily, weight, damageType, damageValue, range)
        {
            this.Loaded = loaded;
            this.IsReloaded = false;
        }
        public RangedWeapon(RangedWeapon weapon) : base(weapon){
            this.Loaded = weapon.Loaded;
            this.IsReloaded = weapon.IsReloaded;
        }

        public bool Loaded { get; set; }
        public bool IsReloaded { get; set; }

        protected override int GetAbilityBonus(){
            return R_EquipData != null && R_EquipData.R_Slots.Select(s => s.Type).Contains(Enums.SlotType.MainHand) ? Wielder.DexterityModifier : 0;
        }
        public override DiceSet GetBaseEquippedAttackBonus(){
            return GetBaseEquippedAttackBonus_Base(Enums.EffectOptions.AttackRollEffect_Range.Ranged);
        }
        public override DiceSet GetEffectRelatedUnequippedAttackBonus(){
            return GetEffectRelatedUnequippedAttackBonus_Base(Enums.EffectOptions.AttackRollEffect_Range.Ranged);
        }
        public override DiceSet GetEffectRelatedEquippedAttackBonus(){
            return GetEffectRelatedEquippedAttackBonus_Base(Enums.EffectOptions.AttackRollEffect_Range.Ranged);
        }

        public override Item Clone(){
            return new RangedWeapon(this);
        }
    }
}