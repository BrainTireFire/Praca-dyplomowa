using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Npgsql.Replication;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Items;
using pracadyplomowa.Models.Entities.Powers;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;
using pracadyplomowa.Models.Entities.Powers.EffectInstances;
using pracadyplomowa.Models.Enums;
using pracadyplomowa.Models.Enums.EffectOptions;
using pracadyplomowa.Utility;
using static pracadyplomowa.Models.DTOs.CharacterFullDto;

namespace pracadyplomowa.Models.DTOs
{
    public class CharacterFullDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; } = "";
        public List<Attribute> Attributes { get; set; } = null!;
        public List<SavingThrow> SavingThrows { get; set; } = null!;
        public List<Skill> Skills { get; set; } = null!;
        public List<Language> Languages { get; set; } = null!;
        public List<ItemFamilyDto> ToolProficiencies { get; set; } = null!;
        public List<ItemFamilyDto> WeaponAndArmorProficiencies { get; set; } = null!;
        public RaceClass Race { get; set; } = null!;
        public SizeWithName Size { get; set; }
        public List<ClassWithLevel> Classes { get; set; } = null!;
        public Hitpoints HitPoints { get; set; } = null!;
        public int Initiative { get; set; } = 0;
        public int Speed { get; set; } = 0;
        public int ArmorClass { get; set; } = 0;
        public DeathSavingThrows DeathSaves { get; set; } = null!;
        public HitDiceClass HitDice { get; set; } = null!;
        public List<WeaponAttack> WeaponAttacks { get; set; } = null!;
        public List<Item> Equipment { get; set; } = null!;
        public List<Power> PreparedPowers { get; set; } = null!;
        public List<Power> KnownPowers { get; set; } = null!;
        public List<Effect> ConstantEffects { get; set; } = null!;
        public List<Effect> Effects { get; set; } = null!;
        public List<Resource> Resources { get; set; } = null!;
        public List<ChoiceGroup> ChoiceGroups { get; set; } = null!;
        public int ProficiencyBonus { get; set; }
        public List<Character.AccessLevels> AccessLevels { get; set; } = [];
        [JsonPropertyName("xp")]
        public int ExperiencePoints { get; set; }
        public bool CanLevelUp {get; set;}

        public CharacterFullDto(Character character)
        {
            Id = character.Id;
            Name = character.Name;
            Description = character.Description ?? "";
            Attributes = GetAttributes(character);
            SavingThrows = GetSavingThrows(character);
            Skills = GetSkills(character);
            Languages = GetLanguages(character);
            ToolProficiencies = GetItemProficiencies(character, ItemType.Tool);
            WeaponAndArmorProficiencies =
            [
                .. GetItemProficiencies(character, ItemType.HeavyArmor),
                .. GetItemProficiencies(character, ItemType.MediumArmor),
                .. GetItemProficiencies(character, ItemType.LightArmor),
                .. GetItemProficiencies(character, ItemType.Shield),
                .. GetItemProficiencies(character, ItemType.Clothing),
                .. GetItemProficiencies(character, ItemType.SimpleWeapon),
                .. GetItemProficiencies(character, ItemType.MartialWeapon),
            ];
            Race = GetRace(character);
            Size = GetSize(character);
            Classes = GetClasses(character);
            HitPoints = GetHitpoints(character);
            Initiative = character.Initiative;
            Speed = character.Speed;
            ArmorClass = character.ArmorClass;
            DeathSaves = GetDeathSaves(character);
            HitDice = GetHitDice(character);
            WeaponAttacks = GetAttacks(character);
            Equipment = GetItems(character);
            PreparedPowers = GetPreparedPowers(character);
            KnownPowers = GetKnownPowers(character);
            ConstantEffects = GetConstantEffects(character);
            Effects = GetTemporaryEffects(character);
            Resources = GetResources(character);
            ChoiceGroups = GetChoiceGroups(character);
            ProficiencyBonus = character.ProficiencyBonus;
            ExperiencePoints = character.ExperiencePoints;
            CanLevelUp = character.CanLevelUp;
        }



        public class Attribute
        {
            public string Name { get; set; } = null!;
            public int Value { get; set; }
            public int Modifier { get; set; }
        }
        public static List<Attribute> GetAttributes(Character character)
        {
            var attributes = new List<Attribute>();
            foreach (int i in Enum.GetValues(typeof(Enums.Ability)))
            {
#pragma warning disable CS8601 // Possible null reference assignment.
                attributes.Add(new Attribute()
                {
                    Name = Enum.GetName(typeof(Enums.Ability), i),
                    Value = character.AbilityValue((Enums.Ability)i),
                    Modifier = Character.AbilityModifier(character.AbilityValue((Enums.Ability)i))
                });
#pragma warning restore CS8601 // Possible null reference assignment.
            }
            return attributes;
        }

        public class SavingThrow
        {
            public string Name { get; set; } = null!;
            public int Value { get; set; }
            public bool Proficient { get; set; }
        }
        public static List<SavingThrow> GetSavingThrows(Character character)
        {
            var savingThrows = new List<SavingThrow>();
            foreach (int i in Enum.GetValues(typeof(Enums.Ability)))
            {
#pragma warning disable CS8601 // Possible null reference assignment.
                savingThrows.Add(new SavingThrow()
                {
                    Name = Enum.GetName(typeof(Enums.Ability), i),
                    Value = character.SavingThrowValue((Enums.Ability)i),
                    Proficient = character.SavingThrowProficiency((Enums.Ability)i)
                });
#pragma warning restore CS8601 // Possible null reference assignment.
            }

            return savingThrows;
        }

        public class Skill
        {
            public string Name { get; set; } = null!;
            public string? Ability { get; set; } = null!;
            public int Value { get; set; }
            public bool Proficient { get; set; }
        }
        public static List<Skill> GetSkills(Character character)
        {
            var skills = new List<Skill>();
            foreach (int i in Enum.GetValues(typeof(Enums.Skill)))
            {
#pragma warning disable CS8601 // Possible null reference assignment.
                skills.Add(new Skill()
                {
                    Name = Enum.GetName(typeof(Enums.Skill), i),
                    Ability = Enum.GetName(typeof(Ability), Utils.SkillToAbility((Enums.Skill)i)),
                    Value = character.SkillValue((Enums.Skill)i),
                    Proficient = character.SkillProficiency((Enums.Skill)i)
                });
#pragma warning restore CS8601 // Possible null reference assignment.
            }

            return skills;
        }

        public class Language
        {
            public int Id { get; set; }
            public string Name { get; set; } = "";
        }

        public List<Language> GetLanguages(Character character)
        {
            var languages = character.R_AffectedBy.OfType<LanguageEffectInstance>().Select(effect => new Language { Id = effect.R_LanguageId, Name = effect.R_Language.Name }).ToList();
            return languages;
        }

        public static List<ItemFamilyDto> GetItemProficiencies(Character character, ItemType itemType)
        {
            var itemProficiencies = character.R_AffectedBy
                                .OfType<ProficiencyEffectInstance>()
                                // .Where(ei => ei.ProficiencyEffectType.ProficiencyEffect == proficiency)
                                .Select(ei => ei.R_GrantsProficiencyInItemFamily)
                                .Where(it => it.ItemType == itemType)
                                .Select(itemFamily => new ItemFamilyDto
                                {
                                    Id = itemFamily.Id,
                                    Name = itemFamily.Name,
                                })
                                .Distinct()
                                .ToList();
            return itemProficiencies;
        }

        public class RaceClass
        {
            public int Id { get; set; }
            public string Name { get; set; } = "";
            public List<Slot> Slots { get; set; } = [];
        }
        public RaceClass GetRace(Character character)
        {
            return new RaceClass()
            {
                Id = character.R_CharacterBelongsToRace.Id,
                Name = character.R_CharacterBelongsToRace.Name,
                Slots = character.R_CharacterBelongsToRace.R_EquipmentSlots.Select(slot => new Slot()
                {
                    Id = slot.Id,
                    Name = slot.Name,
                }).ToList()
            };
        }

        public class ClassWithLevel
        {
            public int Id { get; set; }
            public string Name { get; set; } = null!;
            public int Level { get; set; }
        }
        public static List<ClassWithLevel> GetClasses(Character character)
        {
            var classes = character.R_CharacterHasLevelsInClass
                            .GroupBy(cl => cl.R_ClassId)
                            .Select(g => new ClassWithLevel
                            {
                                Id = g.Key,
                                Name = g.First().R_Class.Name,
                                Level = g.Max(o => o.Level)
                            })
                            .ToList();
            return classes;
        }

        public class Hitpoints
        {
            public int Current { get; set; }
            public int Maximum { get; set; }
            public int Temporary { get; set; }
        }
        public static Hitpoints GetHitpoints(Character character)
        {
            return new Hitpoints
            {
                Current = character.Hitpoints,
                Temporary = character.TemporaryHitpoints,
                Maximum = character.MaxHealth
            };
        }


        public static int GetFlatValue<T>(Character character) where T : ValueEffectInstance
        {
            int value = character.R_AffectedBy
                                .OfType<T>()
                                .Sum(i => i.DiceSet.flat);
            return value;
        }
        // public static int GetInitiative(Character character){
        //     int initiative = character.Initiative;
        //     return initiative;
        // }

        // public static int GetSpeed(Character character){
        //     int speed = character.R_CharacterBelongsToRace.Speed;
        //     int multiplier =  character.R_AffectedBy
        //                     .OfType<MovementEffectInstance>()
        //                     .Where(m => m.MovementEffectType.MovementEffect == MovementEffect.Multiplier)
        //                     .Sum(m => m.DiceSet.flat);
        //     int bonus = character.R_AffectedBy
        //                     .OfType<MovementEffectInstance>()
        //                     .Where(m => m.MovementEffectType.MovementEffect == MovementEffect.Bonus)
        //                     .Sum(m => m.DiceSet.flat);

        //     return speed*multiplier+bonus;
        // }

        // public static int GetArmorClass(Character character) {
        //     int armorClassFromEffects = GetFlatValue<ArmorClassEffectInstance>(character);
        //     int armorClassFromItems = character.R_EquippedItems
        //     .Where(ei => ei.Type == SlotType.Apparel)
        //     .Select(ei => ei.R_Item)
        //     .OfType<Apparel>()
        //     .Distinct()
        //     .Sum(i => i.ArmorClass);

        //     return armorClassFromItems + armorClassFromEffects;
        // }

        public class DeathSavingThrows
        {
            public int Successes { get; set; }
            public int Failures { get; set; }
        }
        public static DeathSavingThrows GetDeathSaves(Character character)
        {
            return new DeathSavingThrows
            {
                Successes = character.SucceededDeathSavingThrows,
                Failures = character.FailedDeathSavingThrows
            };
        }

        public class HitDiceClass
        {
            public DiceSet Total { get; set; } = null!;
            public DiceSet Left { get; set; } = null!;
        }
        public static HitDiceClass GetHitDice(Character character)
        {
            DiceSet total = new DiceSet(character.HitDiceTotal);
            DiceSet left = new()
            {
                d20 = total.d20 - character.UsedHitDice.d20,
                d12 = total.d12 - character.UsedHitDice.d12,
                d10 = total.d10 - character.UsedHitDice.d10,
                d8 = total.d8 - character.UsedHitDice.d8,
                d6 = total.d6 - character.UsedHitDice.d6,
                d4 = total.d4 - character.UsedHitDice.d4,
                d100 = total.d100 - character.UsedHitDice.d100,
                flat = 0,
            };
            return new HitDiceClass()
            {
                Total = total,
                Left = left
            };
        }

        public class WeaponAttack
        {
            public int Id { get; set; }
            public bool Main { get; set; } = false;

            public DiceSet Damage { get; set; } = null!;
            public DiceSet AttackBonus { get; set; } = null!;
            public int DamageType { get; set; }
            public int? Reach { get; set; }
            public int? Range { get; set; }

        }

        public class DiceSet
        {
            public int d4 { get; set; }
            public int d6 { get; set; }
            public int d8 { get; set; }
            public int d10 { get; set; }
            public int d12 { get; set; }
            public int d20 { get; set; }
            public int d100 { get; set; }
            public int flat { get; set; }

            public DiceSet(pracadyplomowa.Models.Entities.Characters.DiceSet diceSet)
            {
                this.d4 = diceSet.d4;
                this.d6 = diceSet.d6;
                this.d8 = diceSet.d8;
                this.d10 = diceSet.d10;
                this.d12 = diceSet.d12;
                this.d20 = diceSet.d20;
                this.flat = diceSet.flat;
            }
            public DiceSet()
            {
            }
        }


        public static List<WeaponAttack> GetAttacks(Character character)
        {
            return character.R_EquippedItems.Where(ei => ei.R_Slots.Select(s => s.Type).Contains(SlotType.MainHand) || ei.R_Slots.Select(s => s.Type).Contains(SlotType.OffHand)).Select(ei => ei.R_Item).OfType<Weapon>().Select(w => new WeaponAttack()
            {
                Id = w.Id,
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                Main = w.R_EquipData.R_Slots.Select(s => s.Type).Contains(SlotType.MainHand),
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                Damage = new DiceSet(w.GetBaseEquippedDamageDiceSet()),
                AttackBonus = new DiceSet(w.GetBaseEquippedAttackBonus()),
                DamageType = (int)w.DamageType,
                Range = w is RangedWeapon || (w is MeleeWeapon meleeWeapon && meleeWeapon.Thrown) ? w.Range : null,
                Reach = w is MeleeWeapon meleeWeapon2 ? (meleeWeapon2.Reach ? 10 : 5) : null,
            }).ToList();
        }
        // public static List<WeaponAttack> GetAttacks(Character character){
        //     var mainHandAttacks = character.R_CharacterHasBackpack.R_BackpackHasItems
        //             .OfType<Weapon>()
        //             .Where(i => i.R_EquipData != null && i.R_EquipData.Type == SlotType.MainHand)
        //             .SelectMany(i => i.R_PowersCastedOnHit)
        //             .Where(p => p.CastableBy == CastableBy.OnWeaponHit && p.RequiredActionType == ActionType.WeaponAttack)
        //             .ToList();

        //     var offHandAttacks = character.R_CharacterHasBackpack.R_BackpackHasItems
        //             .OfType<Weapon>()
        //             .Where(i => i.R_EquipData != null && i.R_EquipData.Type == SlotType.OffHand)
        //             .SelectMany(i => i.R_PowersCastedOnHit)
        //             .Where(p => p.CastableBy == CastableBy.OnWeaponHit && p.RequiredActionType == ActionType.WeaponAttack)
        //             .ToList();

        //     var attacks = new List<WeaponAttack>();

        //     attacks.AddRange(
        //         mainHandAttacks.Select(attack => new WeaponAttack{
        //             Id = attack.Id,
        //             Main = true,
        //             Damages = attack.R_EffectBlueprints
        //             .OfType<DamageEffectBlueprint>()
        //             .Where(effect => effect.DamageEffectType.DamageEffect == DamageEffect.DamageDealt)
        //             .Select(effect => new WeaponAttack.Damage{
        //                 DamageType = effect.DamageEffectType.DamageEffect_DamageType,
        //                 DamageValue = effect.DiceSet
        //             })
        //             .ToList(),
        //             Range = (int) attack.Range
        //         })
        //     );
        //     attacks.AddRange(
        //         offHandAttacks.Select(attack => new WeaponAttack{
        //             Id = attack.Id,
        //             Main = false,
        //             Damages = attack.R_EffectBlueprints
        //             .OfType<DamageEffectBlueprint>()
        //             .Where(effect => effect.DamageEffectType.DamageEffect == DamageEffect.DamageDealt)
        //             .Select(effect => new WeaponAttack.Damage{
        //                 DamageType = effect.DamageEffectType.DamageEffect_DamageType,
        //                 DamageValue = effect.DiceSet
        //             })
        //             .ToList(),
        //             Range = (int) attack.Range
        //         })
        //     );

        //     return attacks;
        // }


        public class Item
        {
            public int Id { get; set; }
            public string Name { get; set; } = "";
            public ItemFamily ItemFamily { get; set; } = new ItemFamily();
            public List<Slot> Slots { get; set; } = new List<Slot>();
            public List<Slot> EquippableInSlots { get; set; } = new List<Slot>();
            public bool Equipped { get; set; }
        }
        public class Slot
        {
            public int Id { get; set; }
            public string Name { get; set; } = "";
        }

        public static List<Item> GetItems(Character character)
        {
            List<Item> items = character.R_CharacterHasBackpack.R_BackpackHasItems.Select(item => new Item()
            {
                Id = item.Id,
                Name = item.Name,
                ItemFamily = new ItemFamily()
                {
                    Id = item.R_ItemInItemsFamily.Id,
                    Name = item.R_ItemInItemsFamily.Name
                },
                Slots = item.R_EquipData != null ? item.R_EquipData.R_Slots.Select(slot => new Slot()
                {
                    Id = slot.Id,
                    Name = slot.Name
                }).ToList() : [],
                EquippableInSlots = item.R_ItemIsEquippableInSlots.Select(slot => new Slot()
                {
                    Id = slot.Id,
                    Name = slot.Name
                }).ToList(),
                Equipped = item.R_EquipData != null
            }).ToList();

            return items;
        }

        public class Power
        {
            public int Id { get; set; }
            public string Name { get; set; } = "";
            public List<string?> Source { get; set; } = [];
        }
        public static List<Power> GetPreparedPowers(Character character)
        {
            List<Power> powers = character.R_PowersPrepared.SelectMany(ps => ps.R_PreparedPowers).Select(power => new Power()
            {
                Id = power.Id,
                Name = power.Name,
                Source = power.GetSourceNames(character.Id)
            }).ToList();
            return powers;
        }

        public static List<Power> GetKnownPowers(Character character)
        {
            var powersx = character.R_PowersKnown.Union(character.R_EquippedItems
            .Select(equipData => equipData.R_Item)
            .Distinct()
            .SelectMany(item => item.R_EquipItemGrantsAccessToPower)).Union(character.R_UsedChoiceGroups.SelectMany(ucg => ucg.R_PowersAlwaysAvailableGranted)).ToList();
            List<Power> powers = powersx.Select(power => new Power()
            {
                Id = power.Id,
                Name = power.Name,
                Source = power.GetSourceNames(character.Id)
            }).ToList();
            return powers;
        }

        public class Effect
        {
            public int Id { get; set; }
            public string Name { get; set; } = "";
            public string Source { get; set; } = "";
            public string Target { get; set; } = "";
            public int? TurnsLeft { get; set; }
        }
        public static List<Effect> GetConstantEffects(Character character)
        {
            List<Effect> effectsOnCharacter = character.R_AffectedBy
            .Where(effect => effect.R_OwnedByGroup == null || effect.R_OwnedByGroup.IsConstant == true)
            .Select(effect => new Effect()
            {
                Id = effect.Id,
                Name = effect.Name,
                Source = effect.Source,
                Target = "Character",
                TurnsLeft = null
            }).ToList();
            List<Effect> effectsOnItems = character.R_CharacterHasBackpack.R_BackpackHasItems.SelectMany(item => item.R_AffectedBy)
            .Where(effect => effect.R_OwnedByGroup == null || effect.R_OwnedByGroup.IsConstant == true)
            .Select(effect => new Effect()
            {
                Id = effect.Id,
                Name = effect.Name,
                Source = effect.Source,
                Target = effect.R_TargetedItem.Name,
                TurnsLeft = effect.R_OwnedByGroup.DurationLeft
            }).ToList();

            return effectsOnCharacter.Union(effectsOnItems).ToList();
        }

        public static List<Effect> GetTemporaryEffects(Character character)
        {
            List<Effect> effectsOnCharacter = character.R_AffectedBy
            .Where(effect => effect.R_OwnedByGroup != null && effect.R_OwnedByGroup.IsConstant == false)
            .Select(effect => new Effect()
            {
                Id = effect.Id,
                Name = effect.Name,
                Source = effect.Source,
                Target = "Character",
                TurnsLeft = effect.R_OwnedByGroup.DurationLeft
            }).ToList();
            List<Effect> effectsOnItems = character.R_CharacterHasBackpack.R_BackpackHasItems.SelectMany(item => item.R_AffectedBy)
            .Where(effect => effect.R_OwnedByGroup != null && effect.R_OwnedByGroup.IsConstant == false)
            .Select(effect => new Effect()
            {
                Id = effect.Id,
                Name = effect.Name,
                Source = effect.Source,
                Target = effect.R_TargetedItem.Name,
                TurnsLeft = effect.R_OwnedByGroup.DurationLeft
            }).ToList();

            return effectsOnCharacter.Union(effectsOnItems).ToList();
        }

        public class Resource
        {
            public int Id { get; set; }
            public string Name { get; set; } = "";
            public int Left { get; set; }
            public int Total { get; set; }
            public string Source { get; set; } = "";
            public string Refresh { get; set; } = "";
        }
        // public static List<Resource> GetResources(Character character){
        //     List<Resource> resources = character.R_ImmaterialResourceInstances
        //     .GroupBy(resource => new {resource.R_BlueprintId, resource.R_ChoiceGroupUsageId, resource.R_ItemId})
        //     .Select(group => {
        //         return new Resource() {
        //             Id = group.Key.R_BlueprintId,
        //             Name = group.Take(1).First().R_Blueprint.Name,
        //             Left = group.Where(resource => !resource.NeedsRefresh).Count(),
        //             Total = group.Count(),
        //             Source = group.Take(1).First().Source,
        //             Refresh = group.Take(1).First().R_Blueprint.RefreshesOn.ToString()
        //         };
        //     })
        //     .ToList();

        //     return resources;
        // }

        public static List<Resource> GetResources(Character character)
        {
            var resources = character.AllImmaterialResourceInstances.GroupBy(resource => new { resource.R_BlueprintId, resource.R_ChoiceGroupUsageId, resource.R_ItemId })
            .Select(group =>
            {
                return new Resource()
                {
                    Id = group.Key.R_BlueprintId,
                    Name = group.Take(1).First().R_Blueprint.Name,
                    Left = group.Where(resource => !resource.NeedsRefresh).Count(),
                    Total = group.Count(),
                    Source = group.Take(1).First().Source,
                    Refresh = group.Take(1).First().R_Blueprint.RefreshesOn.ToString()
                };
            })
            .ToList();
            return resources;
            // return [];
        }

        public class ChoiceGroup
        {
            public int Id { get; set; }
            public string Name { get; set; } = null!;
            public bool ContainsEffects { get; set; }
            public bool ContainsPowersAlwaysAvailable { get; set; }
            public bool ContainsPowersToPrepare { get; set; }
        }
        public static List<ChoiceGroup> GetChoiceGroups(Character character)
        {
            List<ChoiceGroup> choiceGroups = character.R_CharacterBelongsToRace.R_RaceLevels.SelectMany(raceLevel => raceLevel.R_ChoiceGroups)
            .Union(character.R_CharacterHasLevelsInClass.SelectMany(raceLevel => raceLevel.R_ChoiceGroups))
            .Where(choiceGroup => !character.R_UsedChoiceGroups
                .Select(used => used.R_ChoiceGroupId)
                .Contains(choiceGroup.Id)
                )
            .Select(choiceGroup =>
            {
                return new ChoiceGroup
                {
                    Id = choiceGroup.Id,
                    Name = choiceGroup.Name,
                    ContainsEffects = choiceGroup.R_Effects.Count > 0,
                    ContainsPowersAlwaysAvailable = choiceGroup.R_PowersAlwaysAvailable.Count > 0,
                    ContainsPowersToPrepare = choiceGroup.R_PowersToPrepare.Count > 0,
                };
            }).ToList();

            return choiceGroups;
        }

        public class SizeWithName
        {
            public int Order { get; set; }
            public string Name { get; set; } = null!;
        }
        public static SizeWithName GetSize(Character character)
        {
            return new()
            {
                Order = (int)character.Size,
                Name = Enum.GetName(typeof(Size), (int)character.Size)
            };
        }

    }
}