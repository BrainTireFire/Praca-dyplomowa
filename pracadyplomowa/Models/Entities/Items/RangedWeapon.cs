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

        public override DiceSet GetAttackBonus()
        {
            if(Wielder == null){
            return base.GetAttackBonus();
            }
            else{
                return base.GetAttackBonus() + Wielder.AffectedByApprovedEffects
                .OfType<AttackRollEffectInstance>()
                .Where(ei => ei.EffectType.AttackRollEffect_Type == Enums.EffectOptions.AttackRollEffect_Type.Bonus 
                && ei.EffectType.AttackRollEffect_Source == Enums.EffectOptions.AttackRollEffect_Source.Weapon 
                && ei.EffectType.AttackRollEffect_Range == Enums.EffectOptions.AttackRollEffect_Range.Ranged)
                .Select(ei => ei.DiceSet.getPersonalizedSet(Wielder))
                .Aggregate(new DiceSet(), (accumulator, value) => accumulator + value)
                + (R_EquipData != null && R_EquipData.R_Slots.Select(s => s.Type).Contains(Enums.SlotType.MainHand) ? Wielder.DexterityModifier : 0);
            }
        }

        public override Item Clone(){
            return new RangedWeapon(this);
        }
    }
}