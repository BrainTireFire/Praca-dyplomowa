using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Interfaces;
using pracadyplomowa.Models.Entities.Items;
using pracadyplomowa.Models.Entities.Powers;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;
using pracadyplomowa.Models.Enums;
using Xunit;

namespace pracadyplomowa.UnitTests.CharacterTests
{
    public class CharacterPowerCasting
    {
        [Fact]
        public void ShouldSucceed()
        {
            User user = new()
            {
                Id = 1
            };
            Class testClass = new("Test class")
            {
                Id = 1
            };
            for (int i = 0; i < 20; i++){
                testClass.R_ClassLevels.Add(new ClassLevel(i){Id = i, R_Class = testClass, R_ClassId = testClass.Id});
            }
            Character character = new(){
                Id = 1,
                Name = "Test character",
                R_CharacterHasLevelsInClass = [.. testClass.R_ClassLevels],
            };
            Character target = new(){
                Id = 2,
                Name = "Target character",
                R_CharacterHasLevelsInClass = [.. testClass.R_ClassLevels],
            };
            foreach(var classLevel in testClass.R_ClassLevels){
                classLevel.R_Characters.Add(character);
                classLevel.R_Characters.Add(target);
            }
            ItemFamily itemFamily = new()
            {
                Id = 1
            };
            Item item= new("Test item", "", itemFamily, 20){
                Id = 1,
                Price = new CoinSack(){
                    GoldPieces = 200,
                    SilverPieces = 100,
                    CopperPieces = 100
                }
            };
            Item item2= new("Test item", "", itemFamily, 20){
                Id = 1,
                Price = new CoinSack(){
                    GoldPieces = 5000,
                    SilverPieces = 100,
                    CopperPieces = 100
                }
            };
            Backpack backpack = new Backpack(){
                Id = 1,
                R_BackpackOfCharacter = character,
            };
            character.R_CharacterHasBackpack = backpack;
            character.R_CharacterHasBackpackId = backpack.Id;
            backpack.R_BackpackHasItems.Add(item);
            backpack.R_BackpackHasItems.Add(item2);
            Power power = new("Test power", Models.Enums.ActionType.Action, Models.Enums.CastableBy.Character, Models.Enums.PowerType.Attack, Models.Enums.TargetType.Character);
            ItemCostRequirement itemCostRequirement = new()
            {
                Id = 1,
                R_ItemFamilyId = itemFamily.Id,
                R_ItemFamily = itemFamily,
                Worth = new CoinSack(){
                    GoldPieces = 100,
                    SilverPieces = 100,
                    CopperPieces = 100,
                },
                R_Power = power,
                PowerId = power.Id
            };
            ItemCostRequirement itemCostRequirement2 = new()
            {
                Id = 1,
                R_ItemFamilyId = itemFamily.Id,
                R_ItemFamily = itemFamily,
                Worth = new CoinSack(){
                    GoldPieces = 1000,
                    SilverPieces = 100,
                    CopperPieces = 100,
                },
                R_Power = power,
                PowerId = power.Id
            };
            power.R_ItemsCostRequirement.Add(itemCostRequirement);
            power.R_ItemsCostRequirement.Add(itemCostRequirement2);

            ImmaterialResourceBlueprint immaterialResourceBlueprint = new(){
                Id = 1,
                Name = "Test resource"
            };
            ImmaterialResourceInstance immaterialResourceInstance = new(){
                Id = 1,
                R_Blueprint = immaterialResourceBlueprint,
                R_BlueprintId = immaterialResourceBlueprint.Id,
                Level = 1,
                R_Character = character,
                R_CharacterId = character.Id,
            };
            character.R_ImmaterialResourceInstances.Add(immaterialResourceInstance);
            power.R_UsesImmaterialResource = immaterialResourceBlueprint;
            EffectBlueprint effectBlueprint = new DamageEffectBlueprint("Test effect", 0, Models.Enums.RollMoment.OnCast, Models.Enums.EffectOptions.DamageEffect.DamageTaken, Models.Enums.DamageType.force){
                Id = 1,
                R_Power = power,
                R_PowerId = power.Id,
                Level = 1
            };
            
            power.R_EffectBlueprints.Add(effectBlueprint);
            
            var outcome = character.ApplyPowerEffects(power, new Dictionary<Character, HitType>(){{target, HitType.Hit}}, 1);
            
            var effectInstances = target.R_AffectedBy;
            Assert.True(effectInstances.Count == 1);
            Assert.True(effectInstances[0] is DamageEffectInstance);
            Assert.True(outcome == Models.Entities.Interfaces.Outcome.Success);
        }

        [Fact]
        public void ShouldFailDueToLackOfImmaterialResources()
        {
            User user = new()
            {
                Id = 1
            };
            Class testClass = new("Test class")
            {
                Id = 1
            };
            for (int i = 0; i < 20; i++){
                testClass.R_ClassLevels.Add(new ClassLevel(i){Id = i, R_Class = testClass, R_ClassId = testClass.Id});
            }
            Character character = new(){
                Id = 1,
                Name = "Test character",
                R_CharacterHasLevelsInClass = [.. testClass.R_ClassLevels],
            };
            Character target = new(){
                Id = 2,
                Name = "Target character",
                R_CharacterHasLevelsInClass = [.. testClass.R_ClassLevels],
            };
            foreach(var classLevel in testClass.R_ClassLevels){
                classLevel.R_Characters.Add(character);
                classLevel.R_Characters.Add(target);
            }
            ItemFamily itemFamily = new()
            {
                Id = 1
            };
            Item item= new("Test item", "", itemFamily, 20){
                Id = 1,
                Price = new CoinSack(){
                    GoldPieces = 200,
                    SilverPieces = 100,
                    CopperPieces = 100
                }
            };
            Item item2= new("Test item", "", itemFamily, 20){
                Id = 1,
                Price = new CoinSack(){
                    GoldPieces = 5000,
                    SilverPieces = 100,
                    CopperPieces = 100
                }
            };
            Backpack backpack = new Backpack(){
                Id = 1,
                R_BackpackOfCharacter = character,
            };
            character.R_CharacterHasBackpack = backpack;
            character.R_CharacterHasBackpackId = backpack.Id;
            backpack.R_BackpackHasItems.Add(item);
            backpack.R_BackpackHasItems.Add(item2);
            Power power = new("Test power", Models.Enums.ActionType.Action, Models.Enums.CastableBy.Character, Models.Enums.PowerType.Attack, Models.Enums.TargetType.Character);
            ItemCostRequirement itemCostRequirement = new()
            {
                Id = 1,
                R_ItemFamilyId = itemFamily.Id,
                R_ItemFamily = itemFamily,
                Worth = new CoinSack(){
                    GoldPieces = 100,
                    SilverPieces = 100,
                    CopperPieces = 100,
                },
                R_Power = power,
                PowerId = power.Id
            };
            ItemCostRequirement itemCostRequirement2 = new()
            {
                Id = 1,
                R_ItemFamilyId = itemFamily.Id,
                R_ItemFamily = itemFamily,
                Worth = new CoinSack(){
                    GoldPieces = 1000,
                    SilverPieces = 100,
                    CopperPieces = 100,
                },
                R_Power = power,
                PowerId = power.Id
            };
            power.R_ItemsCostRequirement.Add(itemCostRequirement);
            power.R_ItemsCostRequirement.Add(itemCostRequirement2);

            ImmaterialResourceBlueprint immaterialResourceBlueprint = new(){
                Id = 1,
                Name = "Test resource"
            };
            ImmaterialResourceInstance immaterialResourceInstance = new(){
                Id = 1,
                R_Blueprint = immaterialResourceBlueprint,
                R_BlueprintId = immaterialResourceBlueprint.Id,
                Level = 1,
                R_Character = character,
                R_CharacterId = character.Id,
                NeedsRefresh = true,
            };
            character.R_ImmaterialResourceInstances.Add(immaterialResourceInstance);
            power.R_UsesImmaterialResource = immaterialResourceBlueprint;
            EffectBlueprint effectBlueprint = new DamageEffectBlueprint("Test effect", 0, Models.Enums.RollMoment.OnCast, Models.Enums.EffectOptions.DamageEffect.DamageTaken, Models.Enums.DamageType.force){
                Id = 1,
                R_Power = power,
                R_PowerId = power.Id,
                Level = 1
            };
            
            power.R_EffectBlueprints.Add(effectBlueprint);
            
            var outcome = character.ApplyPowerEffects(power, new Dictionary<Character, HitType>(){{target, HitType.Hit}}, 1);
            
            var effectInstances = target.R_AffectedBy;
            Assert.True(effectInstances.Count == 0);
            Assert.True(outcome == Models.Entities.Interfaces.Outcome.ImmaterialResourceUnavailable);
        }

        [Fact]
        public void ShouldFailDueToUnavailableMaterialComponents()
        {
            User user = new()
            {
                Id = 1
            };
            Class testClass = new("Test class")
            {
                Id = 1
            };
            for (int i = 0; i < 20; i++){
                testClass.R_ClassLevels.Add(new ClassLevel(i){Id = i, R_Class = testClass, R_ClassId = testClass.Id});
            }
            Character character = new(){
                Id = 1,
                Name = "Test character",
                R_CharacterHasLevelsInClass = [.. testClass.R_ClassLevels],
            };
            Character target = new(){
                Id = 2,
                Name = "Target character",
                R_CharacterHasLevelsInClass = [.. testClass.R_ClassLevels],
            };
            foreach(var classLevel in testClass.R_ClassLevels){
                classLevel.R_Characters.Add(character);
                classLevel.R_Characters.Add(target);
            }
            ItemFamily itemFamily = new()
            {
                Id = 1
            };
            Item item= new("Test item", "", itemFamily, 20){
                Id = 1,
                Price = new CoinSack(){
                    GoldPieces = 200,
                    SilverPieces = 100,
                    CopperPieces = 100
                }
            };
            Item item2= new("Test item", "", itemFamily, 20){
                Id = 1,
                Price = new CoinSack(){
                    GoldPieces = 5000,
                    SilverPieces = 100,
                    CopperPieces = 100
                }
            };
            Backpack backpack = new Backpack(){
                Id = 1,
                R_BackpackOfCharacter = character,
            };
            character.R_CharacterHasBackpack = backpack;
            character.R_CharacterHasBackpackId = backpack.Id;
            backpack.R_BackpackHasItems.Add(item);
            backpack.R_BackpackHasItems.Add(item2);
            Power power = new("Test power", Models.Enums.ActionType.Action, Models.Enums.CastableBy.Character, Models.Enums.PowerType.Attack, Models.Enums.TargetType.Character);
            ItemCostRequirement itemCostRequirement = new()
            {
                Id = 1,
                R_ItemFamilyId = itemFamily.Id,
                R_ItemFamily = itemFamily,
                Worth = new CoinSack(){
                    GoldPieces = 100,
                    SilverPieces = 100,
                    CopperPieces = 100,
                },
                R_Power = power,
                PowerId = power.Id
            };
            ItemCostRequirement itemCostRequirement2 = new()
            {
                Id = 1,
                R_ItemFamilyId = itemFamily.Id,
                R_ItemFamily = itemFamily,
                Worth = new CoinSack(){
                    GoldPieces = 10000,
                    SilverPieces = 100,
                    CopperPieces = 100,
                },
                R_Power = power,
                PowerId = power.Id
            };
            power.R_ItemsCostRequirement.Add(itemCostRequirement);
            power.R_ItemsCostRequirement.Add(itemCostRequirement2);

            ImmaterialResourceBlueprint immaterialResourceBlueprint = new(){
                Id = 1,
                Name = "Test resource"
            };
            ImmaterialResourceInstance immaterialResourceInstance = new(){
                Id = 1,
                R_Blueprint = immaterialResourceBlueprint,
                R_BlueprintId = immaterialResourceBlueprint.Id,
                Level = 1,
                R_Character = character,
                R_CharacterId = character.Id,
                NeedsRefresh = false,
            };
            character.R_ImmaterialResourceInstances.Add(immaterialResourceInstance);
            power.R_UsesImmaterialResource = immaterialResourceBlueprint;
            EffectBlueprint effectBlueprint = new DamageEffectBlueprint("Test effect", 0, Models.Enums.RollMoment.OnCast, Models.Enums.EffectOptions.DamageEffect.DamageTaken, Models.Enums.DamageType.force){
                Id = 1,
                R_Power = power,
                R_PowerId = power.Id,
                Level = 1
            };
            
            power.R_EffectBlueprints.Add(effectBlueprint);
            
            var outcome = character.ApplyPowerEffects(power, new Dictionary<Character, HitType>(){{target, HitType.Hit}}, 1);
            
            var effectInstances = target.R_AffectedBy;
            Assert.True(effectInstances.Count == 0);
            Assert.True(outcome == Models.Entities.Interfaces.Outcome.InsufficientMaterialComponents);
        }
        
        [Fact]
        public void ShouldSucceedOnSaveable()
        {
            User user = new()
            {
                Id = 1
            };
            Class testClass = new("Test class")
            {
                Id = 1
            };
            for (int i = 0; i < 20; i++){
                testClass.R_ClassLevels.Add(new ClassLevel(i){Id = i, R_Class = testClass, R_ClassId = testClass.Id});
            }
            Character character = new(){
                Id = 1,
                Name = "Test character",
                R_CharacterHasLevelsInClass = [.. testClass.R_ClassLevels],
            };
            Character target = new(){
                Id = 2,
                Name = "Target character",
                R_CharacterHasLevelsInClass = [.. testClass.R_ClassLevels],
            };
            foreach(var classLevel in testClass.R_ClassLevels){
                classLevel.R_Characters.Add(character);
                classLevel.R_Characters.Add(target);
            }
            ItemFamily itemFamily = new()
            {
                Id = 1
            };
            Item item= new("Test item", "", itemFamily, 20){
                Id = 1,
                Price = new CoinSack(){
                    GoldPieces = 200,
                    SilverPieces = 100,
                    CopperPieces = 100
                }
            };
            Item item2= new("Test item", "", itemFamily, 20){
                Id = 1,
                Price = new CoinSack(){
                    GoldPieces = 5000,
                    SilverPieces = 100,
                    CopperPieces = 100
                }
            };
            Backpack backpack = new Backpack(){
                Id = 1,
                R_BackpackOfCharacter = character,
            };
            character.R_CharacterHasBackpack = backpack;
            character.R_CharacterHasBackpackId = backpack.Id;
            backpack.R_BackpackHasItems.Add(item);
            backpack.R_BackpackHasItems.Add(item2);
            Power power = new("Test power", Models.Enums.ActionType.Action, Models.Enums.CastableBy.Character, Models.Enums.PowerType.Saveable, Models.Enums.TargetType.Character){
                SavingThrowAbility = Ability.STRENGTH,
                SavingThrowBehaviour = SavingThrowBehaviour.Breaks,
                SavingThrowRoll = SavingThrowRoll.TakenOnce,
            };
            ItemCostRequirement itemCostRequirement = new()
            {
                Id = 1,
                R_ItemFamilyId = itemFamily.Id,
                R_ItemFamily = itemFamily,
                Worth = new CoinSack(){
                    GoldPieces = 100,
                    SilverPieces = 100,
                    CopperPieces = 100,
                },
                R_Power = power,
                PowerId = power.Id
            };
            ItemCostRequirement itemCostRequirement2 = new()
            {
                Id = 1,
                R_ItemFamilyId = itemFamily.Id,
                R_ItemFamily = itemFamily,
                Worth = new CoinSack(){
                    GoldPieces = 1000,
                    SilverPieces = 100,
                    CopperPieces = 100,
                },
                R_Power = power,
                PowerId = power.Id
            };
            power.R_ItemsCostRequirement.Add(itemCostRequirement);
            power.R_ItemsCostRequirement.Add(itemCostRequirement2);

            ImmaterialResourceBlueprint immaterialResourceBlueprint = new(){
                Id = 1,
                Name = "Test resource"
            };
            ImmaterialResourceInstance immaterialResourceInstance = new(){
                Id = 1,
                R_Blueprint = immaterialResourceBlueprint,
                R_BlueprintId = immaterialResourceBlueprint.Id,
                Level = 1,
                R_Character = character,
                R_CharacterId = character.Id,
            };
            character.R_ImmaterialResourceInstances.Add(immaterialResourceInstance);
            power.R_UsesImmaterialResource = immaterialResourceBlueprint;
            EffectBlueprint effectBlueprint = new DamageEffectBlueprint("Test effect", 0, Models.Enums.RollMoment.OnCast, Models.Enums.EffectOptions.DamageEffect.DamageTaken, Models.Enums.DamageType.force){
                Id = 1,
                R_Power = power,
                R_PowerId = power.Id,
                Level = 1
            };
            
            power.R_EffectBlueprints.Add(effectBlueprint);
            
            var outcome = character.ApplyPowerEffects(power, new Dictionary<Character, HitType>(){{target, HitType.Hit}}, 1);
            
            var effectInstances = target.R_AffectedBy;
            Assert.True(effectInstances.Count == 1);
            Assert.True(effectInstances[0] is DamageEffectInstance);
            Assert.True(outcome == Models.Entities.Interfaces.Outcome.Success);
        }
        
        [Fact]
        public void ShouldUpcast()
        {
            User user = new()
            {
                Id = 1
            };
            Class testClass = new("Test class")
            {
                Id = 1
            };
            for (int i = 0; i < 20; i++){
                testClass.R_ClassLevels.Add(new ClassLevel(i){Id = i, R_Class = testClass, R_ClassId = testClass.Id});
            }
            Character character = new(){
                Id = 1,
                Name = "Test character",
                R_CharacterHasLevelsInClass = [.. testClass.R_ClassLevels],
            };
            Character target = new(){
                Id = 2,
                Name = "Target character",
                R_CharacterHasLevelsInClass = [.. testClass.R_ClassLevels],
            };
            foreach(var classLevel in testClass.R_ClassLevels){
                classLevel.R_Characters.Add(character);
                classLevel.R_Characters.Add(target);
            }
            ItemFamily itemFamily = new()
            {
                Id = 1
            };
            Item item= new("Test item", "", itemFamily, 20){
                Id = 1,
                Price = new CoinSack(){
                    GoldPieces = 200,
                    SilverPieces = 100,
                    CopperPieces = 100
                }
            };
            Item item2= new("Test item", "", itemFamily, 20){
                Id = 1,
                Price = new CoinSack(){
                    GoldPieces = 5000,
                    SilverPieces = 100,
                    CopperPieces = 100
                }
            };
            Backpack backpack = new Backpack(){
                Id = 1,
                R_BackpackOfCharacter = character,
            };
            character.R_CharacterHasBackpack = backpack;
            character.R_CharacterHasBackpackId = backpack.Id;
            backpack.R_BackpackHasItems.Add(item);
            backpack.R_BackpackHasItems.Add(item2);
            Power power = new("Test power", Models.Enums.ActionType.Action, Models.Enums.CastableBy.Character, Models.Enums.PowerType.Saveable, Models.Enums.TargetType.Character){
                SavingThrowAbility = Ability.STRENGTH,
                SavingThrowBehaviour = SavingThrowBehaviour.Breaks,
                SavingThrowRoll = SavingThrowRoll.TakenOnce,
                UpcastBy = UpcastBy.ResourceLevel
            };
            ItemCostRequirement itemCostRequirement = new()
            {
                Id = 1,
                R_ItemFamilyId = itemFamily.Id,
                R_ItemFamily = itemFamily,
                Worth = new CoinSack(){
                    GoldPieces = 100,
                    SilverPieces = 100,
                    CopperPieces = 100,
                },
                R_Power = power,
                PowerId = power.Id
            };
            ItemCostRequirement itemCostRequirement2 = new()
            {
                Id = 1,
                R_ItemFamilyId = itemFamily.Id,
                R_ItemFamily = itemFamily,
                Worth = new CoinSack(){
                    GoldPieces = 1000,
                    SilverPieces = 100,
                    CopperPieces = 100,
                },
                R_Power = power,
                PowerId = power.Id
            };
            power.R_ItemsCostRequirement.Add(itemCostRequirement);
            power.R_ItemsCostRequirement.Add(itemCostRequirement2);

            ImmaterialResourceBlueprint immaterialResourceBlueprint = new(){
                Id = 1,
                Name = "Test resource"
            };
            ImmaterialResourceInstance immaterialResourceInstance = new(){
                Id = 1,
                R_Blueprint = immaterialResourceBlueprint,
                R_BlueprintId = immaterialResourceBlueprint.Id,
                Level = 2,
                R_Character = character,
                R_CharacterId = character.Id,
            };
            character.R_ImmaterialResourceInstances.Add(immaterialResourceInstance);
            power.R_UsesImmaterialResource = immaterialResourceBlueprint;
            EffectBlueprint effectBlueprint = new DamageEffectBlueprint("Test effect", 0, Models.Enums.RollMoment.OnCast, Models.Enums.EffectOptions.DamageEffect.DamageTaken, Models.Enums.DamageType.force){
                Id = 1,
                R_Power = power,
                R_PowerId = power.Id,
                Level = 1,
                Saved = false
            };
            EffectBlueprint effectBlueprint2 = new AbilityEffectBlueprint("Test effect", 0, Models.Enums.RollMoment.OnCast){
                Id = 1,
                R_Power = power,
                R_PowerId = power.Id,
                Level = 2,
                Saved = false
            };
            
            power.R_EffectBlueprints.Add(effectBlueprint);
            power.R_EffectBlueprints.Add(effectBlueprint2);
            
            var outcome = character.ApplyPowerEffects(power, new Dictionary<Character, HitType>(){{target, HitType.Hit}}, 2);
            
            var effectInstances = target.R_AffectedBy;
            Assert.True(effectInstances.Count == 2);
            Assert.True(outcome == Models.Entities.Interfaces.Outcome.Success);
        }
        
        [Fact]
        public void ShouldNotUpcast()
        {
            User user = new()
            {
                Id = 1
            };
            Class testClass = new("Test class")
            {
                Id = 1
            };
            for (int i = 0; i < 20; i++){
                testClass.R_ClassLevels.Add(new ClassLevel(i){Id = i, R_Class = testClass, R_ClassId = testClass.Id});
            }
            Character character = new(){
                Id = 1,
                Name = "Test character",
                R_CharacterHasLevelsInClass = [.. testClass.R_ClassLevels],
            };
            Character target = new(){
                Id = 2,
                Name = "Target character",
                R_CharacterHasLevelsInClass = [.. testClass.R_ClassLevels],
            };
            foreach(var classLevel in testClass.R_ClassLevels){
                classLevel.R_Characters.Add(character);
                classLevel.R_Characters.Add(target);
            }
            ItemFamily itemFamily = new()
            {
                Id = 1
            };
            Item item= new("Test item", "", itemFamily, 20){
                Id = 1,
                Price = new CoinSack(){
                    GoldPieces = 200,
                    SilverPieces = 100,
                    CopperPieces = 100
                }
            };
            Item item2= new("Test item", "", itemFamily, 20){
                Id = 1,
                Price = new CoinSack(){
                    GoldPieces = 5000,
                    SilverPieces = 100,
                    CopperPieces = 100
                }
            };
            Backpack backpack = new Backpack(){
                Id = 1,
                R_BackpackOfCharacter = character,
            };
            character.R_CharacterHasBackpack = backpack;
            character.R_CharacterHasBackpackId = backpack.Id;
            backpack.R_BackpackHasItems.Add(item);
            backpack.R_BackpackHasItems.Add(item2);
            Power power = new("Test power", Models.Enums.ActionType.Action, Models.Enums.CastableBy.Character, Models.Enums.PowerType.Saveable, Models.Enums.TargetType.Character){
                SavingThrowAbility = Ability.STRENGTH,
                SavingThrowBehaviour = SavingThrowBehaviour.Breaks,
                SavingThrowRoll = SavingThrowRoll.TakenOnce,
                UpcastBy = UpcastBy.ResourceLevel
            };
            ItemCostRequirement itemCostRequirement = new()
            {
                Id = 1,
                R_ItemFamilyId = itemFamily.Id,
                R_ItemFamily = itemFamily,
                Worth = new CoinSack(){
                    GoldPieces = 100,
                    SilverPieces = 100,
                    CopperPieces = 100,
                },
                R_Power = power,
                PowerId = power.Id
            };
            ItemCostRequirement itemCostRequirement2 = new()
            {
                Id = 1,
                R_ItemFamilyId = itemFamily.Id,
                R_ItemFamily = itemFamily,
                Worth = new CoinSack(){
                    GoldPieces = 1000,
                    SilverPieces = 100,
                    CopperPieces = 100,
                },
                R_Power = power,
                PowerId = power.Id
            };
            power.R_ItemsCostRequirement.Add(itemCostRequirement);
            power.R_ItemsCostRequirement.Add(itemCostRequirement2);

            ImmaterialResourceBlueprint immaterialResourceBlueprint = new(){
                Id = 1,
                Name = "Test resource"
            };
            ImmaterialResourceInstance immaterialResourceInstance = new(){
                Id = 1,
                R_Blueprint = immaterialResourceBlueprint,
                R_BlueprintId = immaterialResourceBlueprint.Id,
                Level = 1,
                R_Character = character,
                R_CharacterId = character.Id,
            };
            character.R_ImmaterialResourceInstances.Add(immaterialResourceInstance);
            power.R_UsesImmaterialResource = immaterialResourceBlueprint;
            EffectBlueprint effectBlueprint = new DamageEffectBlueprint("Test effect", 0, Models.Enums.RollMoment.OnCast, Models.Enums.EffectOptions.DamageEffect.DamageTaken, Models.Enums.DamageType.force){
                Id = 1,
                R_Power = power,
                R_PowerId = power.Id,
                Level = 1,
                Saved = false
            };
            EffectBlueprint effectBlueprint2 = new AbilityEffectBlueprint("Test effect", 0, Models.Enums.RollMoment.OnCast){
                Id = 1,
                R_Power = power,
                R_PowerId = power.Id,
                Level = 2,
                Saved = false
            };
            
            power.R_EffectBlueprints.Add(effectBlueprint);
            power.R_EffectBlueprints.Add(effectBlueprint2);
            
            var outcome = character.ApplyPowerEffects(power, new Dictionary<Character, HitType>(){{target, HitType.Hit}}, 1);
            
            var effectInstances = target.R_AffectedBy;
            Assert.Equal(Outcome.Success, outcome);
            Assert.Single(effectInstances);
            Assert.True(effectInstances[0] is DamageEffectInstance);
        }
        
        [Fact]
        public void ShouldAssignEffectsOnlyForPassedSavingThrow()
        {
            User user = new()
            {
                Id = 1
            };
            Class testClass = new("Test class")
            {
                Id = 1
            };
            for (int i = 0; i < 20; i++){
                testClass.R_ClassLevels.Add(new ClassLevel(i){Id = i, R_Class = testClass, R_ClassId = testClass.Id});
            }
            Character character = new(){
                Id = 1,
                Name = "Test character",
                R_CharacterHasLevelsInClass = [.. testClass.R_ClassLevels],
            };
            Character target = new(){
                Id = 2,
                Name = "Target character",
                R_CharacterHasLevelsInClass = [.. testClass.R_ClassLevels],
            };
            foreach(var classLevel in testClass.R_ClassLevels){
                classLevel.R_Characters.Add(character);
                classLevel.R_Characters.Add(target);
            }
            ItemFamily itemFamily = new()
            {
                Id = 1
            };
            Item item= new("Test item", "", itemFamily, 20){
                Id = 1,
                Price = new CoinSack(){
                    GoldPieces = 200,
                    SilverPieces = 100,
                    CopperPieces = 100
                }
            };
            Item item2= new("Test item", "", itemFamily, 20){
                Id = 1,
                Price = new CoinSack(){
                    GoldPieces = 5000,
                    SilverPieces = 100,
                    CopperPieces = 100
                }
            };
            Backpack backpack = new Backpack(){
                Id = 1,
                R_BackpackOfCharacter = character,
            };
            character.R_CharacterHasBackpack = backpack;
            character.R_CharacterHasBackpackId = backpack.Id;
            backpack.R_BackpackHasItems.Add(item);
            backpack.R_BackpackHasItems.Add(item2);
            Power power = new("Test power", Models.Enums.ActionType.Action, Models.Enums.CastableBy.Character, Models.Enums.PowerType.Saveable, Models.Enums.TargetType.Character){
                SavingThrowAbility = Ability.STRENGTH,
                SavingThrowBehaviour = SavingThrowBehaviour.Modifies,
                SavingThrowRoll = SavingThrowRoll.TakenOnce,
                UpcastBy = UpcastBy.ResourceLevel
            };
            ItemCostRequirement itemCostRequirement = new()
            {
                Id = 1,
                R_ItemFamilyId = itemFamily.Id,
                R_ItemFamily = itemFamily,
                Worth = new CoinSack(){
                    GoldPieces = 100,
                    SilverPieces = 100,
                    CopperPieces = 100,
                },
                R_Power = power,
                PowerId = power.Id
            };
            ItemCostRequirement itemCostRequirement2 = new()
            {
                Id = 1,
                R_ItemFamilyId = itemFamily.Id,
                R_ItemFamily = itemFamily,
                Worth = new CoinSack(){
                    GoldPieces = 1000,
                    SilverPieces = 100,
                    CopperPieces = 100,
                },
                R_Power = power,
                PowerId = power.Id
            };
            power.R_ItemsCostRequirement.Add(itemCostRequirement);
            power.R_ItemsCostRequirement.Add(itemCostRequirement2);

            ImmaterialResourceBlueprint immaterialResourceBlueprint = new(){
                Id = 1,
                Name = "Test resource"
            };
            ImmaterialResourceInstance immaterialResourceInstance = new(){
                Id = 1,
                R_Blueprint = immaterialResourceBlueprint,
                R_BlueprintId = immaterialResourceBlueprint.Id,
                Level = 1,
                R_Character = character,
                R_CharacterId = character.Id,
            };
            character.R_ImmaterialResourceInstances.Add(immaterialResourceInstance);
            power.R_UsesImmaterialResource = immaterialResourceBlueprint;
            EffectBlueprint effectBlueprint = new DamageEffectBlueprint("Test effect", 0, Models.Enums.RollMoment.OnCast, Models.Enums.EffectOptions.DamageEffect.DamageTaken, Models.Enums.DamageType.force){
                Id = 1,
                R_Power = power,
                R_PowerId = power.Id,
                Level = 1,
                Saved = false
            };
            EffectBlueprint effectBlueprint2 = new AbilityEffectBlueprint("Test effect", 0, Models.Enums.RollMoment.OnCast){
                Id = 1,
                R_Power = power,
                R_PowerId = power.Id,
                Level = 1,
                Saved = true
            };
            
            power.R_EffectBlueprints.Add(effectBlueprint);
            power.R_EffectBlueprints.Add(effectBlueprint2);
            
            var outcome = character.ApplyPowerEffects(power, new Dictionary<Character, HitType>(){{target, HitType.Miss}}, 1);
            
            var effectInstances = target.R_AffectedBy;
            Assert.Equal(Outcome.Success, outcome);
            Assert.Single(effectInstances);
            Assert.True(effectInstances[0] is AbilityEffectInstance);
        }
    }
}