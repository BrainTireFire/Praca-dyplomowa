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

namespace pracadyplomowa.UnitTests.obj.CharacterTests
{
    public class CharacterTakingDamage
    {
        [Fact]
        public void ShouldTakeFullDamage()
        {
            Class testClass = new("Test class")
            {
                Id = 1
            };
            for (int i = 0; i < 20; i++){
                testClass.R_ClassLevels.Add(new ClassLevel(i){Id = i, R_Class = testClass, R_ClassId = testClass.Id, HitPoints = 5});
            }
            Character character = new("Test", false, 5, 5, 5, 5, 5, 5, testClass.R_ClassLevels[0], new Race(){Name = "test", Size = Size.Medium, Speed = 30}, -1);

            EffectGroup effectGroup = new(){
                IsConstant = true,
                Name = "Test group",
            };
            EffectInstance damageEffectInstance = new DamageEffectBlueprint("test", 6, RollMoment.OnCast, Models.Enums.EffectOptions.DamageEffect.DamageTaken, DamageType.acid).Generate(character, character);
            effectGroup.R_OwnedEffects.Add(damageEffectInstance);
            int charactersStartingHealth = character.Hitpoints;
            foreach(var effect in effectGroup.R_OwnedEffects){
                effect.Resolve();
            }
            int characterFinalHealth = character.Hitpoints;

            Assert.Equal(6, charactersStartingHealth - characterFinalHealth);
        }

        [Fact]
        public void ShouldTakeHalvedDamage()
        {
            Class testClass = new("Test class")
            {
                Id = 1
            };
            for (int i = 0; i < 20; i++){
                testClass.R_ClassLevels.Add(new ClassLevel(i){Id = i, R_Class = testClass, R_ClassId = testClass.Id, HitPoints = 5});
            }
            Character character = new("Test", false, 5, 5, 5, 5, 5, 5, testClass.R_ClassLevels[0], new Race(){Name = "test", Size = Size.Medium, Speed = 30}, -1);

            EffectGroup effectGroup = new(){
                IsConstant = true,
                Name = "Test group",
            };
            EffectInstance damageEffectInstance = new DamageEffectBlueprint("test", 6, RollMoment.OnCast, Models.Enums.EffectOptions.DamageEffect.DamageTaken, DamageType.acid).Generate(character, character);
            EffectInstance resistanceEffectInstance = new ResistanceEffectBlueprint("test", Models.Enums.EffectOptions.ResistanceEffect.Resistance, DamageType.acid).Generate(character, character);
            EffectInstance resistanceEffectInstance2 = new ResistanceEffectBlueprint("test", Models.Enums.EffectOptions.ResistanceEffect.Resistance, DamageType.acid).Generate(character, character);
            effectGroup.R_OwnedEffects.Add(damageEffectInstance);
            effectGroup.R_OwnedEffects.Add(resistanceEffectInstance);
            effectGroup.R_OwnedEffects.Add(resistanceEffectInstance2);
            int charactersStartingHealth = character.Hitpoints;
            foreach(var effect in effectGroup.R_OwnedEffects){
                effect.Resolve();
            }
            int characterFinalHealth = character.Hitpoints;

            Assert.Equal(3, charactersStartingHealth - characterFinalHealth);
        }

        [Fact]
        public void ShouldTakeDoubledDamage()
        {
            Class testClass = new("Test class")
            {
                Id = 1
            };
            for (int i = 0; i < 20; i++){
                testClass.R_ClassLevels.Add(new ClassLevel(i){Id = i, R_Class = testClass, R_ClassId = testClass.Id, HitPoints = 5});
            }
            Character character = new("Test", false, 5, 5, 5, 5, 5, 5, testClass.R_ClassLevels[0], new Race(){Name = "test", Size = Size.Medium, Speed = 30}, -1);

            EffectGroup effectGroup = new(){
                IsConstant = true,
                Name = "Test group",
            };
            EffectInstance damageEffectInstance = new DamageEffectBlueprint("test", 6, RollMoment.OnCast, Models.Enums.EffectOptions.DamageEffect.DamageTaken, DamageType.acid).Generate(character, character);
            EffectInstance resistanceEffectInstance = new ResistanceEffectBlueprint("test", Models.Enums.EffectOptions.ResistanceEffect.Vulnerability, DamageType.acid).Generate(character, character);
            EffectInstance resistanceEffectInstance2 = new ResistanceEffectBlueprint("test", Models.Enums.EffectOptions.ResistanceEffect.Vulnerability, DamageType.acid).Generate(character, character);
            effectGroup.R_OwnedEffects.Add(damageEffectInstance);
            effectGroup.R_OwnedEffects.Add(resistanceEffectInstance);
            effectGroup.R_OwnedEffects.Add(resistanceEffectInstance2);
            int charactersStartingHealth = character.Hitpoints;
            foreach(var effect in effectGroup.R_OwnedEffects){
                effect.Resolve();
            }
            int characterFinalHealth = character.Hitpoints;

            Assert.Equal(12, charactersStartingHealth - characterFinalHealth);
        }

        [Fact]
        public void ShouldTakeNoDamage()
        {
            Class testClass = new("Test class")
            {
                Id = 1
            };
            for (int i = 0; i < 20; i++){
                testClass.R_ClassLevels.Add(new ClassLevel(i){Id = i, R_Class = testClass, R_ClassId = testClass.Id, HitPoints = 5});
            }
            Character character = new("Test", false, 5, 5, 5, 5, 5, 5, testClass.R_ClassLevels[0], new Race(){Name = "test", Size = Size.Medium, Speed = 30}, -1);

            EffectGroup effectGroup = new(){
                IsConstant = true,
                Name = "Test group",
            };
            EffectInstance damageEffectInstance = new DamageEffectBlueprint("test", 6, RollMoment.OnCast, Models.Enums.EffectOptions.DamageEffect.DamageTaken, DamageType.acid).Generate(character, character);
            EffectInstance resistanceEffectInstance = new ResistanceEffectBlueprint("test", Models.Enums.EffectOptions.ResistanceEffect.Vulnerability, DamageType.acid).Generate(character, character);
            EffectInstance resistanceEffectInstance2 = new ResistanceEffectBlueprint("test", Models.Enums.EffectOptions.ResistanceEffect.Immunity, DamageType.acid).Generate(character, character);
            effectGroup.R_OwnedEffects.Add(damageEffectInstance);
            effectGroup.R_OwnedEffects.Add(resistanceEffectInstance);
            effectGroup.R_OwnedEffects.Add(resistanceEffectInstance2);
            int charactersStartingHealth = character.Hitpoints;
            foreach(var effect in effectGroup.R_OwnedEffects){
                effect.Resolve();
            }
            int characterFinalHealth = character.Hitpoints;

            Assert.Equal(0, charactersStartingHealth - characterFinalHealth);
        }

        [Fact]
        public void ShouldTakePaddedDamage()
        {
            Class testClass = new("Test class")
            {
                Id = 1
            };
            for (int i = 0; i < 20; i++){
                testClass.R_ClassLevels.Add(new ClassLevel(i){Id = i, R_Class = testClass, R_ClassId = testClass.Id, HitPoints = 5});
            }
            Character character = new("Test", false, 5, 5, 5, 5, 5, 5, testClass.R_ClassLevels[0], new Race(){Name = "test", Size = Size.Medium, Speed = 30}, -1);

            EffectGroup effectGroup = new(){
                IsConstant = true,
                Name = "Test group",
            };
            EffectInstance damageEffectInstance = new DamageEffectBlueprint("test", 6, RollMoment.OnCast, Models.Enums.EffectOptions.DamageEffect.DamageTaken, DamageType.acid).Generate(character, character);
            EffectInstance resistanceEffectInstance = new ResistanceEffectBlueprint("test", Models.Enums.EffectOptions.ResistanceEffect.Vulnerability, DamageType.acid).Generate(character, character);
            EffectInstance temporaryHitpoints = new HitpointEffectBlueprint("test", 2, RollMoment.OnCast){
                HitpointEffectType = new Models.ComplexTypes.Effects.HitpointEffectType(){
                    HitpointEffect = Models.Enums.EffectOptions.HitpointEffect.TemporaryHitpoints
                }
            }.Generate(character, character);
            effectGroup.R_OwnedEffects.Add(temporaryHitpoints);
            effectGroup.R_OwnedEffects.Add(damageEffectInstance);
            effectGroup.R_OwnedEffects.Add(resistanceEffectInstance);
            int charactersStartingHealth = character.Hitpoints;
            foreach(var effect in effectGroup.R_OwnedEffects){
                effect.Resolve();
            }
            int characterFinalHealth = character.Hitpoints;

            Assert.Equal(10, charactersStartingHealth - characterFinalHealth);
        }
    }
}