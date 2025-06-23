using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Campaign;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Interfaces;
using pracadyplomowa.Models.Entities.Powers;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;
using pracadyplomowa.Models.Enums;
using pracadyplomowa.Models.Enums.EffectOptions;

namespace pracadyplomowa.Models.Entities.Items
{
    public abstract class Weapon : Item, ICaster
    {
        protected Weapon() : base(){
        }
        protected Weapon(string name, string description, ItemFamily itemFamily, int weight, DamageType damageType, DiceSet damageValue, int range) : base(name, description, itemFamily, weight)
        {
            this.DamageValue = damageValue;
            this.DamageType = damageType;
            this.Range = range;
        }

        public Weapon(Weapon weapon) : base(weapon){
            WeaponWeight = weapon.WeaponWeight;
            DamageType = weapon.DamageType;
            DamageValue = new DiceSet(weapon.DamageValue);
            Range = weapon.Range;
            // R_PowersCastedOnHit = [.. weapon.R_PowersCastedOnHit];
        }

        public WeaponWeight WeaponWeight { get; set; }
        public DamageType DamageType { get; set; }
        public virtual DiceSet DamageValue { get; set; } = new DiceSet();
        // public int DamageValueId { get; set; }
        public int Range { get; set; } // for ranged or thrown weapons
        
        //Relationship
        // public virtual ICollection<Power> R_PowersCastedOnHit { get; set; } = [];

        [NotMapped]
        protected Character? Wielder {
            get {
                return this.R_EquipData?.R_Character;
            }
        }

        public bool IsWielderProficient(){
            bool wielderProficient = Wielder?.AffectedByApprovedEffects
                                                                        .OfType<ProficiencyEffectInstance>()
                                                                        .Where(ef => ef.ProficiencyEffectType.ProficiencyEffect == ProficiencyEffect.SpecificItemFamily && ef.R_GrantsProficiencyInItemFamilyId == this.R_ItemInItemsFamilyId
                                                                        || ef.ProficiencyEffectType.ProficiencyEffect == ProficiencyEffect.ItemType && ef.ProficiencyEffectType.ItemType == this.R_ItemInItemsFamily.ItemType)
                                                                        .FirstOrDefault() != null;
            return wielderProficient;
        }

        public virtual DiceSet GetBaseUnequippedDamageDiceSet(){
            DiceSet damageDiceSet = DamageValue;
            List<DamageEffectInstance> extraWeaponDamageEffectList = R_AffectedBy
                                                                        .OfType<DamageEffectInstance>()
                                                                        .Where(x => x.EffectType.DamageEffect == DamageEffect.ExtraWeaponDamage)
                                                                        .ToList();
            DiceSet extraWeaponDamage = extraWeaponDamageEffectList.Aggregate(new DiceSet(), (sum, current) => sum + current.DiceSet);
            MagicEffectInstance? magicEffect = R_AffectedBy
                                                                        .OfType<MagicEffectInstance>()
                                                                        .OrderBy(x => x.DiceSet.flat)
                                                                        .LastOrDefault();
            damageDiceSet += extraWeaponDamage + (magicEffect?.DiceSet.flat ?? 0);
            return damageDiceSet;
        }
        
        protected virtual bool ShouldAddAbilityBonus(){
            bool isInMainHand = R_EquipData != null && R_EquipData.R_Slots.Select(s => s.Type).Contains(Enums.SlotType.MainHand);
            bool mainHandDoesntHoldNonLightWeapon = Wielder == null || !Wielder.R_EquippedItems
                                                    .Where(ed => ed.R_Slots.Where(s => s.Type == SlotType.MainHand).Any())
                                                    .Select(ed => ed.R_Item)
                                                    .OfType<Weapon>()
                                                    .Where(w => w.WeaponWeight != WeaponWeight.Light)
                                                    .Any();
            return isInMainHand || (this.WeaponWeight == WeaponWeight.Light && mainHandDoesntHoldNonLightWeapon);
        }

        public virtual DiceSet GetBaseEquippedDamageDiceSet(){ // returns weapon base damage
            DiceSet damageDiceSet = GetBaseUnequippedDamageDiceSet();
            DiceSet wieldersExtraWeaponDamage = 0;
            Wielder?.GetExtraWeaponDamage(out wieldersExtraWeaponDamage);
            damageDiceSet += wieldersExtraWeaponDamage + (Wielder != null && IsWielderProficient() ? Wielder.ProficiencyBonus : 0);
            var abilityBonus = GetAbilityBonus();
            return damageDiceSet.getPersonalizedSet(Wielder) + (ShouldAddAbilityBonus() || abilityBonus < 0 ? GetAbilityBonus() : 0);
        }

        public virtual Dictionary<DamageType, DiceSet> GetEffectsUnequippedDamageDiceSet(){
            //check for additional damage from effects
            Dictionary<DamageType, List<DamageEffectInstance>> extraDamageEffectMap = R_AffectedBy
                                                                    .OfType<DamageEffectInstance>()
                                                                    .Where(x => x.EffectType.DamageEffect == DamageEffect.DamageDealt)
                                                                    .GroupBy(effectInstance => (DamageType)effectInstance.EffectType.DamageEffect_DamageType)
                                                                    .ToDictionary(g => g.Key, g => g.ToList());
            var result = extraDamageEffectMap.ToDictionary(element => element.Key, element => element.Value.Aggregate(new DiceSet(), (sum, current) => sum + current.DiceSet));

            return result;
        }
        public virtual Dictionary<DamageType, DiceSet> GetEffectsEquippedDamageDiceSet(){
            Dictionary<DamageType, DiceSet> result = GetEffectsUnequippedDamageDiceSet();
            //check for additional damage from effects
            if(Wielder != null){
                Wielder.GetAdditionalDamageOnWeaponStrike(out Dictionary<DamageType, DiceSet> damageTypeToDiceSetMap);
                foreach(var pair in damageTypeToDiceSetMap){
                    if(!result.ContainsKey(pair.Key)){
                        result[pair.Key] = pair.Value;
                    }
                    else{
                        result[pair.Key] += pair.Value;
                    }
                }
                foreach(var pair in result){
                    result[pair.Key] = pair.Value.getPersonalizedSet(Wielder);
                }
            }

            return result;
        }

        public virtual Dictionary<DamageType, DiceSet> GetTotalDamageDiceSet(){
            var baseDamage = GetBaseEquippedDamageDiceSet();
            var effectDamageDictionary = GetEffectsEquippedDamageDiceSet();
            var result = new Dictionary<DamageType, DiceSet>
            {
                { DamageType, baseDamage }
            };
            foreach(var pair in effectDamageDictionary){
                if(!result.ContainsKey(pair.Key)){
                    result[pair.Key] = pair.Value;
                }
                else{
                    result[pair.Key] += pair.Value;
                }
            }
            return result;
        }

        protected abstract int GetAbilityBonus();

        public virtual DiceSet GetBaseUnequippedAttackBonus(){
            MagicEffectInstance? magicEffect = R_AffectedBy
                                                                        .OfType<MagicEffectInstance>()
                                                                        .OrderBy(x => x.DiceSet.flat)
                                                                        .LastOrDefault();
            return magicEffect?.DiceSet.flat ?? 0;
        }

        public abstract DiceSet GetBaseEquippedAttackBonus();

        protected virtual DiceSet GetBaseEquippedAttackBonus_Base(Enums.EffectOptions.AttackRollEffect_Range range){
            return GetBaseUnequippedAttackBonus() + (Wielder != null && IsWielderProficient() ? Wielder.ProficiencyBonus : 0) + GetAbilityBonus();
        }

        public abstract DiceSet GetEffectRelatedUnequippedAttackBonus();

        public abstract DiceSet GetEffectRelatedEquippedAttackBonus();

        protected DiceSet GetEffectRelatedUnequippedAttackBonus_Base(Enums.EffectOptions.AttackRollEffect_Range range){
            var attackRollEffectDiceSet = this.R_AffectedBy.OfType<AttackRollEffectInstance>()
                .Where(ei => ei.EffectType.AttackRollEffect_Type == Enums.EffectOptions.AttackRollEffect_Type.Bonus 
                && ei.EffectType.AttackRollEffect_Source == Enums.EffectOptions.AttackRollEffect_Source.Weapon 
                && ei.EffectType.AttackRollEffect_Range == range)
                .Select(ei => ei.DiceSet.getPersonalizedSet(null))
                .Aggregate(new DiceSet(), (accumulator, value) => accumulator + value);
            return attackRollEffectDiceSet;
        }

        protected DiceSet GetEffectRelatedEquippedAttackBonus_Base(Enums.EffectOptions.AttackRollEffect_Range range){
            return GetEffectRelatedUnequippedAttackBonus_Base(range) + (Wielder?.AttackBonusDiceSet(range, AttackRollEffect_Source.Weapon) ?? 0);
        }

        public virtual DiceSet GetTotalAttackBonus(){
            return GetBaseEquippedAttackBonus() + GetEffectRelatedEquippedAttackBonus();
        }

        public Dictionary<int, HitType> CheckIfPowerHitSuccessfull(Encounter encounter, Power power, List<Character> targets, List<string> messages){
            Dictionary<int, HitType> hitMap = [];

            foreach(var targetedCharacter in targets){
                if(power.PowerType == PowerType.Saveable){
                    int roll = targetedCharacter.SavingThrowRoll((Ability)power.SavingThrowAbility);
                    var dc = Wielder?.DifficultyClass(power);
                    var success = roll <= dc;
                    hitMap.Add(
                        targetedCharacter.Id,
                        success ? HitType.Hit : HitType.Miss
                    );
                    messages.Add($"{targetedCharacter.Name} {(!success ? "passed" : "failed")} a {(Ability)power.SavingThrowAbility} saving throw against {power.Name} (rolled total of {roll} against DC{dc})");
                }
                else
                {
                    hitMap.Add(
                        targetedCharacter.Id,
                        HitType.Hit
                    );
                    messages.Add($"{targetedCharacter.Name} is targeted by {power.Name}");
                }
            }
            return hitMap;
        }

        public Outcome ApplyPowerEffects(Power power, Dictionary<Character, HitType> targetsToHitSuccessMap, int? immaterialResourceLevel, int? powerLevel, out List<EffectInstance> generatedEffects, List<string> messages)
        {
            EffectGroup effectGroup = new();
            generatedEffects = [];
            //generate effects
            foreach(Character target in targetsToHitSuccessMap.Keys){
                if (targetsToHitSuccessMap.TryGetValue(target, out var outcome))
                {
                    foreach (EffectBlueprint effectBlueprint in power.R_EffectBlueprints)
                    {
                        bool shouldAdd = false;

                        if (power.PowerType == PowerType.Saveable || power.PowerType == PowerType.Attack)
                        {
                            if ((outcome == HitType.Hit || outcome == HitType.CriticalHit) && !effectBlueprint.Saved)
                            {
                                shouldAdd = true;
                            }
                            else if (!(outcome == HitType.Hit || outcome == HitType.CriticalHit) && effectBlueprint.Saved)
                            {
                                shouldAdd = true;
                            }
                        }
                        else if(power.PowerType == PowerType.PassiveEffect){
                            shouldAdd = true;
                        }

                        if (shouldAdd)
                        {
                            var effectInstance = effectBlueprint.Generate(Wielder, target);
                            generatedEffects.Add(effectInstance);
                            if(outcome == HitType.CriticalHit && effectInstance is DamageEffectInstance damageEffectInstance){
                                damageEffectInstance.CriticalHit = true;
                            }
                            effectGroup.AddEffect(effectInstance);
                        }
                    }
                }
            }
            //configure effect group
            effectGroup.DurationLeft = power.Duration;
            effectGroup.IsConstant = false;
            if(power.PowerType == PowerType.Saveable && power.SavingThrowRoll == Enums.SavingThrowRoll.RetakenEveryTurn){
                effectGroup.DifficultyClassToBreak = Wielder?.DifficultyClass(power);
                effectGroup.SavingThrow = (Ability)power.SavingThrowAbility;
            }
            effectGroup.Name = power.Name;
            return Outcome.Success;
        }
    }
}