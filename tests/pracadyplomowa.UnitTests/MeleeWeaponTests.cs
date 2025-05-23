using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Items;
using pracadyplomowa.Models.Entities.Powers;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;
using pracadyplomowa.Models.Enums;
using Xunit;

namespace pracadyplomowa.UnitTests.MeleeWeaponTests
{
    public class MeleeWeaponTest
    {
        [Fact]
        public void EquippedMeleeWeaponStats()
        {
            Class testClass = new("Test class")
            {
                Id = 1
            };
            for (int i = 0; i < 20; i++){
                testClass.R_ClassLevels.Add(new ClassLevel(i){Id = i, R_Class = testClass, R_ClassId = testClass.Id, HitPoints = 5});
            }
            Race race = new(){Name = "test", Size = Size.Medium, Speed = 30};
            for (int i = 0; i < 20; i++){
                race.R_RaceLevels.Add(new RaceLevel(){Id = i, R_Race = race, R_RaceId = race.Id, Level = i});
            }
            EquipmentSlot slot1 = new(){
                Name = "Main hand",
                Type = SlotType.MainHand
            };
            slot1.R_Races.Add(race);
            EquipmentSlot slot2 = new(){
                Name = "Off hand",
                Type = SlotType.OffHand
            };
            slot2.R_Races.Add(race);
            race.R_EquipmentSlots.AddRange([slot1, slot2]);
            Character character = new("Test", false, 14, 8, 6, 4, 2, 12, testClass.R_ClassLevels[0], race, -1);
            EffectInstance proficiencyEffectInstance = new ProficiencyEffectBlueprint(ItemType.MartialMeleeWeapon).Generate(null, character);
            EffectInstance bonusDamageInstance = new DamageEffectBlueprint("test", 2, RollMoment.OnCast, Models.Enums.EffectOptions.DamageEffect.ExtraWeaponDamage, null).Generate(null, character);
            EffectInstance thunderDamageInstance = new DamageEffectBlueprint("test", 2, RollMoment.OnCast, Models.Enums.EffectOptions.DamageEffect.DamageDealt, DamageType.thunder).Generate(null, character);
            EffectInstance attackRollEffectInstance = new AttackRollEffectBlueprint(
                "test",
                2,
                RollMoment.OnCast,
                Models.Enums.EffectOptions.AttackRollEffect_Type.Bonus,
                Models.Enums.EffectOptions.AttackRollEffect_Source.Weapon,
                Models.Enums.EffectOptions.AttackRollEffect_Range.Melee
            ).Generate(null, character);
            EffectInstance attackRollEffectInstance2 = new AttackRollEffectBlueprint(
                "test",
                2,
                RollMoment.OnCast,
                Models.Enums.EffectOptions.AttackRollEffect_Type.Bonus,
                Models.Enums.EffectOptions.AttackRollEffect_Source.Weapon,
                Models.Enums.EffectOptions.AttackRollEffect_Range.Ranged
            ).Generate(null, character);
            character.R_AffectedBy.Add(proficiencyEffectInstance);
            character.R_AffectedBy.Add(bonusDamageInstance);
            character.R_AffectedBy.Add(thunderDamageInstance);
            character.R_AffectedBy.Add(attackRollEffectInstance);
            character.R_AffectedBy.Add(attackRollEffectInstance2);

            ItemFamily itemFamily = new()
            {
                Id = 1,
                ItemType = ItemType.MartialMeleeWeapon
            };
            MeleeWeapon sword = new MeleeWeapon("Sword", "sword", itemFamily, 20, DamageType.slashing, 6, 10){IsBlueprint = false};
            sword.R_ItemIsEquippableInSlots.Add(slot1);
            EffectInstance weaponDamageEffectInstance = new DamageEffectInstance("test"){
                DiceSet = 2,
                EffectType = new Models.ComplexTypes.Effects.DamageEffectType(){
                    DamageEffect = Models.Enums.EffectOptions.DamageEffect.ExtraWeaponDamage
                },
            };
            EffectInstance magicItemEffectInstance = new MagicEffectInstance("test", 1);
            weaponDamageEffectInstance.R_TargetedItem = sword;
            sword.R_AffectedBy.Add(weaponDamageEffectInstance);
            magicItemEffectInstance.R_TargetedItem = sword;
            sword.R_AffectedBy.Add(magicItemEffectInstance);

            
            //unequipped stats
            var baseAttackBonusUnequipped = sword.GetBaseUnequippedAttackBonus();
            var baseDamageUnequipped = sword.GetBaseUnequippedDamageDiceSet();
            var effectAttackBonusUnequipped = sword.GetEffectRelatedUnequippedAttackBonus();
            var effectDamageUnequipped = sword.GetEffectsUnequippedDamageDiceSet();
            var totalAttackBonusUnequipped = sword.GetTotalAttackBonus();
            var totalDamageUnequipped = sword.GetTotalDamageDiceSet();

            character.EquipItem(sword, slot1);

            //equipped stats
            var baseAttackBonus = sword.GetBaseEquippedAttackBonus();
            var baseDamage = sword.GetBaseEquippedDamageDiceSet();
            var effectAttackBonus = sword.GetEffectRelatedEquippedAttackBonus();
            var effectDamage = sword.GetEffectsEquippedDamageDiceSet();
            var totalAttackBonus = sword.GetTotalAttackBonus();
            var totalDamage = sword.GetTotalDamageDiceSet();
            
            Assert.Equal(1, baseAttackBonusUnequipped.flat);
            Assert.Equal(0, effectAttackBonusUnequipped.flat);
            Assert.Equal(9, baseDamageUnequipped.flat); //6 base (not versatile)
            Assert.Equal(0, effectDamageUnequipped.GetValueOrDefault(DamageType.thunder)?.flat ?? 0);
            Assert.Equal(1, totalAttackBonusUnequipped.flat);
            Assert.Equal(9, totalDamageUnequipped.GetValueOrDefault(DamageType.slashing)!.flat);
            Assert.Equal(0, totalDamageUnequipped.GetValueOrDefault(DamageType.thunder)?.flat ?? 0);

            Assert.Equal(5, baseAttackBonus.flat);
            Assert.Equal(2, effectAttackBonus.flat);
            Assert.Equal(19, baseDamage.flat); //10 versatile + 2 strength + 2 proficiency + 2 extra weapon damage
            Assert.Equal(2, effectDamage.GetValueOrDefault(DamageType.thunder)?.flat ?? 0);
            Assert.Equal(7, totalAttackBonus.flat);
            Assert.Equal(19, totalDamage.GetValueOrDefault(DamageType.slashing)!.flat);
            Assert.Equal(2, totalDamage.GetValueOrDefault(DamageType.thunder)?.flat ?? 0);

            
            RangedWeapon bow = new RangedWeapon("Sword", "sword", itemFamily, 20, DamageType.slashing, 6, 20, false){IsBlueprint = false};
            bow.R_ItemIsEquippableInSlots.Add(slot1);
            EffectInstance ranged_weaponDamageEffectInstance = new DamageEffectInstance("test"){
                DiceSet = 2,
                EffectType = new Models.ComplexTypes.Effects.DamageEffectType(){
                    DamageEffect = Models.Enums.EffectOptions.DamageEffect.ExtraWeaponDamage
                },
            };
            EffectInstance ranged_magicItemEffectInstance = new MagicEffectInstance("test", 1);
            weaponDamageEffectInstance.R_TargetedItem = bow;
            bow.R_AffectedBy.Add(ranged_weaponDamageEffectInstance);
            magicItemEffectInstance.R_TargetedItem = bow;
            bow.R_AffectedBy.Add(ranged_magicItemEffectInstance);

            
            //unequipped stats
            var ranged_baseAttackBonusUnequipped = bow.GetBaseUnequippedAttackBonus();
            var ranged_baseDamageUnequipped = bow.GetBaseUnequippedDamageDiceSet();
            var ranged_effectAttackBonusUnequipped = bow.GetEffectRelatedUnequippedAttackBonus();
            var ranged_effectDamageUnequipped = bow.GetEffectsUnequippedDamageDiceSet();
            var ranged_totalAttackBonusUnequipped = bow.GetTotalAttackBonus();
            var ranged_totalDamageUnequipped = bow.GetTotalDamageDiceSet();

            character.EquipItem(bow, slot1);

            //equipped stats
            var ranged_baseAttackBonus = bow.GetBaseEquippedAttackBonus();
            var ranged_baseDamage = bow.GetBaseEquippedDamageDiceSet();
            var ranged_effectAttackBonus = bow.GetEffectRelatedEquippedAttackBonus();
            var ranged_effectDamage = bow.GetEffectsEquippedDamageDiceSet();
            var ranged_totalAttackBonus = bow.GetTotalAttackBonus();
            var ranged_totalDamage = bow.GetTotalDamageDiceSet();
            
            Assert.Equal(1, ranged_baseAttackBonusUnequipped.flat);
            Assert.Equal(0, ranged_effectAttackBonusUnequipped.flat);
            Assert.Equal(9, ranged_baseDamageUnequipped.flat); //6 base + 1 magic + 2 extra weapon damage
            Assert.Equal(0, ranged_effectDamageUnequipped.GetValueOrDefault(DamageType.thunder)?.flat ?? 0);
            Assert.Equal(1, ranged_totalAttackBonusUnequipped.flat);
            Assert.Equal(9, ranged_totalDamageUnequipped.GetValueOrDefault(DamageType.slashing)!.flat);
            Assert.Equal(0, ranged_totalDamageUnequipped.GetValueOrDefault(DamageType.thunder)?.flat ?? 0);

            Assert.Equal(2, ranged_baseAttackBonus.flat);
            Assert.Equal(2, ranged_effectAttackBonus.flat);
            Assert.Equal(12, ranged_baseDamage.flat); //6 base + 1 magic + 2 extra weapon damage - 1 dexterity + 2 proficiency + 2 extra weapon damage from character
            Assert.Equal(2, ranged_effectDamage.GetValueOrDefault(DamageType.thunder)?.flat ?? 0);
            Assert.Equal(4, ranged_totalAttackBonus.flat);
            Assert.Equal(12, ranged_totalDamage.GetValueOrDefault(DamageType.slashing)!.flat);
            Assert.Equal(2, ranged_totalDamage.GetValueOrDefault(DamageType.thunder)?.flat ?? 0);
        }
    }
}