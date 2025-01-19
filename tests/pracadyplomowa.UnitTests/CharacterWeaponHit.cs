using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Campaign;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Items;
using pracadyplomowa.Models.Entities.Powers;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;
using pracadyplomowa.Models.Enums;
using Xunit;

namespace pracadyplomowa.UnitTests.CharacterTests
{
    public class CharacterWeaponHit
    {
        [Fact]
        public void ShouldApplyWeaponHitEffects()
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
            Character target = new("Test", false, 14, 8, 6, 4, 2, 12, testClass.R_ClassLevels[0], race, -1);
            EffectInstance proficiencyEffectInstance = new ProficiencyEffectBlueprint(ItemType.MartialWeapon).Generate(null, character);
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
                ItemType = ItemType.MartialWeapon
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

            character.EquipItem(sword, slot1);

            Encounter enc = new Encounter(){
                Id = 1,
            };
            Board board = new Board(){
                R_Encounter = enc,
            };
            enc.R_Board = board;
            enc.R_BoardId = board.Id;
            Field field1 = new Field(){
                PositionX = 1,
                PositionY = 1,
            };
            Field field2 = new Field(){
                PositionX = 1,
                PositionY = 2,
            };
            Field field3 = new Field(){
                PositionX = 1,
                PositionY = 3,
            };
            board.AddField(field1);
            board.AddField(field2);
            board.AddField(field3);
            enc.R_Participances.Add(new ParticipanceData(){
                R_Character = character,
                R_CharacterId = character.Id,
                R_Encounter = enc,
                R_EncounterId = enc.Id,
                R_OccupiedField = field1
            });
            enc.R_Participances.Add(new ParticipanceData(){
                R_Character = target,
                R_CharacterId = target.Id,
                R_Encounter = enc,
                R_EncounterId = enc.Id,
                R_OccupiedField = field3
            });

            int hitpointsBefore = target.Hitpoints;
            var result = character.ApplyWeaponHitEffects(enc, sword, target, false);
            int hitpointsAfter = target.Hitpoints;

            Assert.Equal(hitpointsBefore - result.DamageTaken.GetValueOrDefault(DamageType.slashing) - result.DamageTaken.GetValueOrDefault(DamageType.thunder), hitpointsAfter);
        }
    }
}