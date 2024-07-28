using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Razor;
using Npgsql.Replication;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Items;
using pracadyplomowa.Models.Entities.Powers;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;
using pracadyplomowa.Models.Entities.Powers.EffectInstances;
using pracadyplomowa.Models.Enums;
using pracadyplomowa.Models.Enums.EffectOptions;

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
        public List<ItemFamily> ToolProficiencies { get; set; } = null!;
        public List<ItemFamily> WeaponAndArmorProficiencies { get; set; } = null!;
        public RaceClass Race { get; set; } = null!;
        public Size Size { get; set; }
        public List<ClassWithLevel> Classes { get; set; } = null!;
        public Hitpoints HitPoints { get; set; } = null!;
        public int Initiative { get; set; } = 0;
        public int Speed { get; set; } = 0;
        public int ArmorClass { get; set; } = 0;
        public DeathSavingThrows DeathSaves { get; set; } = null!;
        public HitDiceClass HitDice { get; set; } = null!;
        public List<WeaponAttack> WeaponAttacks { get; set;} = null!;
        public List<Item> Equipment { get; set; } = null!;
        public List<Power> PreparedPowers { get; set;} = null!;
        public List<Power> KnownPowers { get; set; } = null!;
        public List<Effect> ConstantEffects { get; set; } = null!;
        public List<Effect> Effects { get; set; } = null!;
        public List<Resource> Resources { get; set; } = null!;

        public CharacterFullDto(Character character){
            Id = character.Id;
            Name = character.Name;
            Description = character.Description;
            Attributes = GetAttributes(character);
            SavingThrows = GetSavingThrows(character);
            Skills = GetSkills(character);
            Languages = GetLanguages(character);
            ToolProficiencies = GetItemProficiencies(character, ProficiencyEffect.Tool);
            WeaponAndArmorProficiencies =
            [
                .. GetItemProficiencies(character, ProficiencyEffect.Armor),
                .. GetItemProficiencies(character, ProficiencyEffect.Weapon),
                .. GetItemProficiencies(character, ProficiencyEffect.Shields),
            ];
            Race = GetRace(character);
            Size = GetSize(character);
            Classes = GetClasses(character);
            HitPoints = GetHitpoints(character);
            Initiative = GetInitiative(character);
            Speed = GetSpeed(character);
            ArmorClass = GetArmorClass(character);
            DeathSaves = GetDeathSaves(character);
            HitDice = GetHitDice(character);
            WeaponAttacks = GetAttacks(character);
            Equipment = GetItems(character);
            PreparedPowers = GetPreparedPowers(character);
            KnownPowers = GetKnownPowers(character);
            ConstantEffects = GetConstantEffects(character);
            Effects = GetTemporaryEffects(character);
            Resources = GetResources(character);
        }



        public class Attribute
        {
            public string Name { get; set; } = null!;
            public int Value { get; set; }
        }
        public static List<Attribute> GetAttributes(Character character)
        {
            var attributes =  character.R_AffectedBy
                    .SelectMany(group => group.R_OwnedEffects)
                    .OfType<AbilityEffectInstance>()
                    .Where(ei => ei.AbilityEffectType.AbilityEffect == AbilityEffect.Bonus)
                    .GroupBy(ei => ei.AbilityEffectType.AbilityEffect_Ability)
                    .Select(g => new Attribute{
                        Name = g.Key.ToString(),
                        Value = g.Sum(ei => ei.DiceSet.flat)
                    })
                    .ToList();
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
            var savingThrows =  character.R_AffectedBy
                                .SelectMany(group => group.R_OwnedEffects)
                                .OfType<SavingThrowEffectInstance>()
                                .Where(ei => ei.SavingThrowEffectType.SavingThrowEffect == SavingThrowEffect.Bonus)
                                .GroupBy(ei => ei.SavingThrowEffectType.SavingThrowEffect_Ability)
                                .Select(g => new SavingThrow{
                                    Name = g.Key.ToString(),
                                    Value = g.Sum(ei => ei.DiceSet.flat)
                                })
                                .ToList();
            var proficiencies = character.R_AffectedBy
                                .SelectMany(group => group.R_OwnedEffects)
                                .OfType<SavingThrowEffectInstance>()
                                .Where(ei => ei.SavingThrowEffectType.SavingThrowEffect == SavingThrowEffect.Proficiency)
                                .Select(ei => ei.SavingThrowEffectType.SavingThrowEffect_Ability.ToString())
                                .Distinct()
                                .ToList();
            savingThrows.ForEach(savingThrow => {
                if(proficiencies.Contains(savingThrow.Name)){
                    savingThrow.Proficient = true;
                }
            });
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
            var skills =  character.R_AffectedBy
                                .SelectMany(group => group.R_OwnedEffects)
                                .OfType<SkillEffectInstance>()
                                .Where(ei => ei.SkillEffectType.SkillEffect == SkillEffect.Bonus)
                                .GroupBy(ei => ei.SkillEffectType.SkillEffect_Skill)
                                .Select(g => new Skill{
                                    Name = g.Key.ToString(),
                                    Value = g.Sum(ei => ei.DiceSet.flat)
                                })
                                .ToList();
            var proficiencies = character.R_AffectedBy
                                .SelectMany(group => group.R_OwnedEffects)
                                .OfType<SkillEffectInstance>()
                                .Where(ei => ei.SkillEffectType.SkillEffect == SkillEffect.Proficiency)
                                .Select(ei => ei.SkillEffectType.SkillEffect_Skill.ToString())
                                .Distinct()
                                .ToList();
            skills.ForEach(skill => {
                if(proficiencies.Contains(skill.Name)){
                    skill.Proficient = true;
                }
            });
            return skills;
        }

        public class Language {
            public int Id { get; set; }
            public string Name { get; set; } = "";
        }

        public List<Language> GetLanguages(Character character){
            var languages = new List<Language>();
            return languages;
        }

        public class ItemFamily {
            public int Id { get; set; }
            public string Name { get; set; } = null!;
        }
        public static List<ItemFamily> GetItemProficiencies(Character character, ProficiencyEffect proficiency){
            var itemProficiencies =  character.R_AffectedBy
                                .SelectMany(group => group.R_OwnedEffects)
                                .OfType<ProficiencyEffectInstance>()
                                .Where(ei => ei.ProficiencyEffectType.ProficiencyEffect == proficiency)
                                .Select(ei => ei.R_GrantsProficiencyInItemFamily)
                                .Select(itemFamily => new ItemFamily{
                                    Id = itemFamily.Id,
                                    Name = itemFamily.Name,
                                })
                                .Distinct()
                                .ToList();
            return itemProficiencies;
        }

        public class RaceClass {
            public int Id { get; set;}
            public string Name { get; set;} = "";
        }
        public RaceClass GetRace(Character character){
            return new RaceClass(){
                Id = character.R_CharacterBelongsToRace.Id,
                Name = character.R_CharacterBelongsToRace.Name
            };
        }

        public static Size GetSize(Character character){
            var size = character.R_CharacterBelongsToRace.Size;
            var setSizes = character.R_AffectedBy
                                .SelectMany(group => group.R_OwnedEffects)
                                .OfType<SizeEffectInstance>()
                                .Where(ei => ei.SizeEffectType.SizeEffect == SizeEffect.Change)
                                .Select(ei => ei.SizeEffectType.SizeEffect_SizeToSet)
                                .ToList();
            if(setSizes.Count != 0){
                size = setSizes.Max();
            }
            var sizeChanges = character.R_AffectedBy
                                .SelectMany(group => group.R_OwnedEffects)
                                .OfType<SizeEffectInstance>()
                                .Where(ei => ei.SizeEffectType.SizeEffect == SizeEffect.Bonus)
                                .Select(ei => ei.SizeEffectType.SizeBonus)
                                .Sum();
            var result = ((int)size) + sizeChanges;
            if (Enum.IsDefined(typeof(Size), result))
            {
                return (Size)result;
            }
            else
            {
                if(result < 0) return Size.Tiny;
                else return Size.Gargantuan;
            }
        }

        public class ClassWithLevel
        {
            public int Id { get; set; }
            public string Name { get; set; } = null!;
            public int Level { get; set; }
        }
        public static List<ClassWithLevel> GetClasses(Character character){
            var classes = character.R_CharacterHasLevelsInClass
                            .GroupBy(cl => cl.R_ClassId)
                            .Select(g => new ClassWithLevel{
                                Id = g.Key,
                                Name = g.First().R_Class.Name,
                                Level = g.Max(o => o.Level)
                            })
                            .ToList();
            return classes;
        }

        public class Hitpoints
        {
            public int Current {get; set;}
            public int Maximum {get;set;}
            public int Temporary {get; set;}
        }
        public static Hitpoints GetHitpoints(Character character){
            var current = character.Hitpoints;
            var temporary = character.R_AffectedBy
                    .SelectMany(g => g.R_OwnedEffects)
                    .OfType<HitpointEffectInstance>()
                    .Where(h => h.HitpointEffectType.HitpointEffect == HitpointEffect.TemporaryHitpoints)
                    .Sum(t => t.DiceSet.flat);
            var maximum = character.R_AffectedBy
                    .SelectMany(g => g.R_OwnedEffects)
                    .OfType<HitpointEffectInstance>()
                    .Where(h => h.HitpointEffectType.HitpointEffect == HitpointEffect.HitpointMaximumBonus)
                    .Sum(t => t.DiceSet.flat);
            return new Hitpoints{
                Current = current,
                Temporary = temporary,
                Maximum = maximum
            };
        }


        public static int GetFlatValue<T>(Character character) where T : ValueEffectInstance {
            int value = character.R_AffectedBy
                                .SelectMany(g => g.R_OwnedEffects)
                                .OfType<T>()
                                .Sum(i => i.DiceSet.flat);
            return value;
        }
        public static int GetInitiative(Character character){
            int initiative = GetFlatValue<InitiativeEffectInstance>(character);
            return initiative;
        }

        public static int GetSpeed(Character character){
            int speed = character.R_CharacterBelongsToRace.Speed;
            int multiplier =  character.R_AffectedBy
                            .SelectMany(g => g.R_OwnedEffects)
                            .OfType<MovementEffectInstance>()
                            .Where(m => m.MovementEffectType.MovementEffect == MovementEffect.Multiplier)
                            .Sum(m => m.DiceSet.flat);
            int bonus = character.R_AffectedBy
                            .SelectMany(g => g.R_OwnedEffects)
                            .OfType<MovementEffectInstance>()
                            .Where(m => m.MovementEffectType.MovementEffect == MovementEffect.Bonus)
                            .Sum(m => m.DiceSet.flat);

            return speed*multiplier+bonus;
        }

        public static int GetArmorClass(Character character) {
            int armorClassFromEffects = GetFlatValue<ArmorClassEffectInstance>(character);
            int armorClassFromItems = character.R_EquippedItems
            .Where(ei => ei.Type == SlotType.Apparel)
            .Select(ei => ei.R_Item)
            .OfType<Apparel>()
            .Distinct()
            .Sum(i => i.ArmorClass);

            return armorClassFromItems + armorClassFromEffects;
        }

        public class DeathSavingThrows
        {
            public int Successes { get; set; }
            public int Failures { get; set; }
        }
        public static DeathSavingThrows GetDeathSaves(Character character) {
            return new DeathSavingThrows{
                Successes = character.SucceededDeathSavingThrows,
                Failures = character.FailedDeathSavingThrows
            };
        }

        public class HitDiceClass
        {
            public DiceSet Total { get; set; } = null!;
            public DiceSet Left { get; set; } = null!;
        }
        public static HitDiceClass GetHitDice(Character character){
            DiceSet total = new()
            {
                d20 = character.R_CharacterHasLevelsInClass.Sum(cl => cl.HitDie.d20),
                d12 = character.R_CharacterHasLevelsInClass.Sum(cl => cl.HitDie.d12),
                d10 = character.R_CharacterHasLevelsInClass.Sum(cl => cl.HitDie.d10),
                d8 = character.R_CharacterHasLevelsInClass.Sum(cl => cl.HitDie.d8),
                d6 = character.R_CharacterHasLevelsInClass.Sum(cl => cl.HitDie.d6),
                d4 = character.R_CharacterHasLevelsInClass.Sum(cl => cl.HitDie.d4),
                d100 = character.R_CharacterHasLevelsInClass.Sum(cl => cl.HitDie.d100),
                flat = 0,
            };
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
            return new HitDiceClass(){
                Total = total,
                Left = left
            };
        }

        public class WeaponAttack
        {
            public int Id { get; set; }
            public bool Main { get; set; } = false;

            public List<Damage> Damages { get; set; } = [];

            public int Range { get; set; } = 0;
        
            public class Damage {
                public DamageType DamageType{ get; set; }
                public DiceSet DamageValue { get; set; } = null!;
            }

        }
        public static List<WeaponAttack> GetAttacks(Character character){
            var mainHandAttacks = character.R_CharacterHasBackpack.R_BackpackHasItems
                    .OfType<Weapon>()
                    .Where(i => i.R_EquipData != null && i.R_EquipData.Type == SlotType.MainHand)
                    .SelectMany(i => i.R_PowersCastedOnHit)
                    .Where(p => p.CastableBy == CastableBy.OnWeaponHit && p.RequiredActionType == ActionType.WeaponAttack)
                    .ToList();
            
            var offHandAttacks = character.R_CharacterHasBackpack.R_BackpackHasItems
                    .OfType<Weapon>()
                    .Where(i => i.R_EquipData != null && i.R_EquipData.Type == SlotType.OffHand)
                    .SelectMany(i => i.R_PowersCastedOnHit)
                    .Where(p => p.CastableBy == CastableBy.OnWeaponHit && p.RequiredActionType == ActionType.WeaponAttack)
                    .ToList();

            var attacks = new List<WeaponAttack>();

            attacks.AddRange(
                mainHandAttacks.Select(attack => new WeaponAttack{
                    Id = attack.Id,
                    Main = true,
                    Damages = attack.R_EffectBlueprints
                    .OfType<DamageEffectBlueprint>()
                    .Where(effect => effect.DamageEffectType.DamageEffect == DamageEffect.DamageDealt)
                    .Select(effect => new WeaponAttack.Damage{
                        DamageType = effect.DamageEffectType.DamageEffect_DamageType,
                        DamageValue = effect.DiceSet
                    })
                    .ToList(),
                    Range = attack.Range
                })
            );
            attacks.AddRange(
                offHandAttacks.Select(attack => new WeaponAttack{
                    Id = attack.Id,
                    Main = false,
                    Damages = attack.R_EffectBlueprints
                    .OfType<DamageEffectBlueprint>()
                    .Where(effect => effect.DamageEffectType.DamageEffect == DamageEffect.DamageDealt)
                    .Select(effect => new WeaponAttack.Damage{
                        DamageType = effect.DamageEffectType.DamageEffect_DamageType,
                        DamageValue = effect.DiceSet
                    })
                    .ToList(),
                    Range = attack.Range
                })
            );

            return attacks;
        }

        
        public class Item {
            public int Id { get; set;}
            public string Name { get; set;} = "";
            public ItemFamily ItemFamily { get; set;} = new ItemFamily();
            public List<Slot> Slots { get; set;} = new List<Slot>();
            public bool Equipped { get; set;}

            public class Slot {
                public int Id { get; set;}
                public string Name {get; set;} = "";
            }
        }
        public static List<Item> GetItems(Character character){
            List<Item> items = character.R_CharacterHasBackpack.R_BackpackHasItems.Select(item => new Item() {
                Id = item.Id,
                Name = item.Name,
                ItemFamily = new ItemFamily() {
                    Id = item.R_ItemInItemsFamily.Id,
                    Name = item.R_ItemInItemsFamily.Name
                },
                Slots = item.R_ItemIsEquippableInSlots.Select(slot => new Item.Slot() {
                    Id = slot.Id,
                    Name = slot.Name
                }).ToList(),
                Equipped = item.R_EquipData != null
            }).ToList();

            return items;
        }

        public class Power {
            public int Id { get; set;}
            public string Name { get; set;} = "";
            public string Source { get; set;} = "";
        }
        public static List<Power> GetPreparedPowers(Character character){
            List<Power> powers = character.R_PowersPrepared.Select(power => new Power() {
                Id = power.Id,
                Name = power.Name,
                Source = ""
            }).ToList();
            return powers;
        }

        public static List<Power> GetKnownPowers(Character character){
            List<Power> powers = character.R_PowersKnown.Select(power => new Power() {
                Id = power.Id,
                Name = power.Name,
                Source = "Known"
            }).ToList();

            powers.AddRange(character.R_EquippedItems
            .Select(equipData => equipData.R_Item)
            .Distinct()
            .SelectMany(item => item.R_EquipItemGrantsAccessToPower)
            .Select(power => new Power() {
                Id = power.Id,
                Name = power.Name,
                Source = "Item"
                }
            )); //TODO: how to put item name here?


            return powers;
        }

        public class Effect {
            public int Id { get; set; }
            public string Name { get; set; } = "";
            public string Source { get; set; } = "";
            public string Target { get; set; } = "";
            public int TurnsLeft { get; set; }
        }
        public static List<Effect> GetConstantEffects(Character character){
            List<Effect> effects = character.R_AffectedBy
            .Where(group => group.IsConstant)
            .SelectMany(group => group.R_OwnedEffects)
            .Select(effect => new Effect() {
                Id = effect.Id,
                Name = effect.Name,
                Source = "Placeholder",
                Target = "Placeholder",
                TurnsLeft = 1
            }).ToList();

            return effects;
        }

        public static List<Effect> GetTemporaryEffects(Character character){
            List<Effect> effects = character.R_AffectedBy
            .Where(group => !group.IsConstant)
            .SelectMany(group => group.R_OwnedEffects)
            .Select(effect => new Effect() {
                Id = effect.Id,
                Name = effect.Name,
                Source = "Placeholder",
                Target = "Placeholder",
                TurnsLeft = effect.R_OwnedByGroup.DurationLeft
            }).ToList();

            return effects;
        }

        public class Resource {
            public int Id { get; set; }
            public string Name { get; set; } = "";
            public int Left { get; set; }
            public int Total { get; set; }
            public string Source { get; set; } = "";
            public string Refresh { get; set; } = "";
        }
        public static List<Resource> GetResources(Character character){
            List<Resource> resources = character.R_ImmaterialResourceInstances
            .GroupBy(resource => resource.R_BlueprintId)
            .Select(group => new Resource() {
                Id = group.Key,
                Name = group.Take(1).First().R_Blueprint.Name,
                Left = group.Where(resource => !resource.NeedsRefresh).Count(),
                Total = group.Count(),
                Source = "Placeholder",
                Refresh = group.Take(1).First().R_Blueprint.RefreshesOn.ToString()
            })
            .ToList();

            return resources;
        }
        
    }
}