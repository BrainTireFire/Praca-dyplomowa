using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers;

namespace pracadyplomowa.Models.Entities.Items
{
    public class MeleeWeapon: Weapon
    {
        protected MeleeWeapon() : base(){

        }
        public MeleeWeapon(string name, string description, ItemFamily itemFamily, int weight) : base(name, description, itemFamily, weight)
        {
        }

        public bool Finesse { get; set; }
        public bool Reach { get; set;}
        public bool Versatile { get; set; }

        public override DiceSet GetAttackBonus()
        {
            if(Wielder == null){
            return base.GetAttackBonus();
            }
            else{
                return base.GetAttackBonus() 
                + Wielder.AffectedByApprovedEffects
                .OfType<AttackRollEffectInstance>()
                .Where(ei => ei.AttackRollEffectType.AttackRollEffect_Type == Enums.EffectOptions.AttackRollEffect_Type.Bonus 
                && ei.AttackRollEffectType.AttackRollEffect_Source == Enums.EffectOptions.AttackRollEffect_Source.Weapon 
                && ei.AttackRollEffectType.AttackRollEffect_Range == Enums.EffectOptions.AttackRollEffect_Range.Melee)
                .Select(ei => ei.DiceSet.getPersonalizedSet(Wielder))
                .Aggregate(new DiceSet(), (accumulator, value) => accumulator + value)
                + (R_EquipData != null && R_EquipData.Types.Contains(Enums.SlotType.MainHand) ? (Finesse ? (Wielder.StrengthModifier > Wielder.DexterityModifier ? Wielder.StrengthModifier : Wielder.DexterityModifier) : Wielder.StrengthModifier) : 0);
            }
        }
    }
}