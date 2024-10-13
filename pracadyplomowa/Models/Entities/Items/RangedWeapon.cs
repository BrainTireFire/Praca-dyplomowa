using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers;

namespace pracadyplomowa.Models.Entities.Items
{
    public class RangedWeapon : Weapon, IRangedWeapon
    {
        protected RangedWeapon() : base(){
            
        }
        public RangedWeapon(string name, string description, ItemFamily itemFamily, int weight) : base(name, description, itemFamily, weight)
        {
        }

        public int Range { get; set; }
        public bool LoadedRange { get; set; }

        public override DiceSet GetAttackBonus()
        {
            if(Wielder == null){
            return base.GetAttackBonus();
            }
            else{
                return base.GetAttackBonus() + Wielder.AffectedByApprovedEffects
                .OfType<AttackRollEffectInstance>()
                .Where(ei => ei.AttackRollEffectType.AttackRollEffect_Type == Enums.EffectOptions.AttackRollEffect_Type.Bonus 
                && ei.AttackRollEffectType.AttackRollEffect_Source == Enums.EffectOptions.AttackRollEffect_Source.Weapon 
                && ei.AttackRollEffectType.AttackRollEffect_Range == Enums.EffectOptions.AttackRollEffect_Range.Ranged)
                .Select(ei => ei.DiceSet.getPersonalizedSet(Wielder))
                .Aggregate(new DiceSet(), (accumulator, value) => accumulator + value)
                + (R_EquipData != null && R_EquipData.Types.Contains(Enums.SlotType.MainHand) ? Wielder.Dexterity : 0);
            }
        }
    }
}