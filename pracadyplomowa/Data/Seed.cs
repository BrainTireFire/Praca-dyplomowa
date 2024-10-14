using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using pracadyplomowa.Repository.Race;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;
using pracadyplomowa.Models.Entities.Powers;
using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Enums.EffectOptions;
using pracadyplomowa.Models.Enums;
using pracadyplomowa.Models.Entities.Items;

namespace pracadyplomowa;

public class Seed
{
    public static async Task SeedUsers(UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        //if this continue thats mean you dont have any users in database!
        if (await userManager.Users.AnyAsync()) return;

        var userData = await System.IO.File.ReadAllTextAsync("Data/users.json");
        var users = JsonSerializer.Deserialize<List<User>>(userData);
        if (users == null) return;

        var roles = new List<Role>
            {
                new() {Name = "User"},
                new() {Name = "Admin"}
            };

        foreach (var role in roles)
        {
            await roleManager.CreateAsync(role);
        }

        foreach (var user in users)
        {
            user.UserName = user.UserName.ToLower();
            await userManager.CreateAsync(user, "Drewno1234");
            await userManager.AddToRoleAsync(user, "User");
        }

        var admin = new User
        {
            UserName = "admin"
        };

        await userManager.CreateAsync(admin, "Drewno1234");
        await userManager.AddToRoleAsync(admin, "Admin");
    }

    public static async Task SeedLanguages(AppDbContext context){
        context.Languages.AddRange(new List<Language>(){ new() { Name = "Common"},new() { Name = "Elvish"},new() { Name = "Dwarvish"}});
        await context.SaveChangesAsync();
    }

    public static async Task SeedItemFamilies(AppDbContext context){
        List<ItemFamily> newFamilies = new();

        //armors
        CreateItemFamily(context, newFamilies, "Heavy armor", ItemType.HeavyArmor);
        CreateItemFamily(context, newFamilies, "Medium armor", ItemType.MediumArmor);
        CreateItemFamily(context, newFamilies, "Light armor", ItemType.LightArmor);
        CreateItemFamily(context, newFamilies, "Shield", ItemType.Shield);
        CreateItemFamily(context, newFamilies, "Clothing", ItemType.Clothing);
        //simple weapons
        CreateItemFamily(context, newFamilies, "Club", ItemType.SimpleWeapon);
        CreateItemFamily(context, newFamilies, "Dagger", ItemType.SimpleWeapon);
        CreateItemFamily(context, newFamilies, "Greatclub", ItemType.SimpleWeapon);
        CreateItemFamily(context, newFamilies, "Handaxe", ItemType.SimpleWeapon);
        CreateItemFamily(context, newFamilies, "Javelin", ItemType.SimpleWeapon);
        CreateItemFamily(context, newFamilies, "Light hammer", ItemType.SimpleWeapon);
        CreateItemFamily(context, newFamilies, "Mace", ItemType.SimpleWeapon);
        CreateItemFamily(context, newFamilies, "Quarterstaff", ItemType.SimpleWeapon);
        CreateItemFamily(context, newFamilies, "Sickle", ItemType.SimpleWeapon);
        CreateItemFamily(context, newFamilies, "Spear", ItemType.SimpleWeapon);
        CreateItemFamily(context, newFamilies, "Light crossbow", ItemType.SimpleWeapon);
        CreateItemFamily(context, newFamilies, "Dart", ItemType.SimpleWeapon);
        CreateItemFamily(context, newFamilies, "Shortbow", ItemType.SimpleWeapon);
        CreateItemFamily(context, newFamilies, "Sling", ItemType.SimpleWeapon);
        //martial weapons
        CreateItemFamily(context, newFamilies, "Battleaxe", ItemType.MartialWeapon);
        CreateItemFamily(context, newFamilies, "Flail", ItemType.MartialWeapon);
        CreateItemFamily(context, newFamilies, "Glaive", ItemType.MartialWeapon);
        CreateItemFamily(context, newFamilies, "Greataxe", ItemType.MartialWeapon);
        CreateItemFamily(context, newFamilies, "Greatsword", ItemType.MartialWeapon);
        CreateItemFamily(context, newFamilies, "Halberd", ItemType.MartialWeapon);
        CreateItemFamily(context, newFamilies, "Lance", ItemType.MartialWeapon);
        CreateItemFamily(context, newFamilies, "Longsword", ItemType.MartialWeapon);
        CreateItemFamily(context, newFamilies, "Maul", ItemType.MartialWeapon);
        CreateItemFamily(context, newFamilies, "Morningstar", ItemType.MartialWeapon);
        CreateItemFamily(context, newFamilies, "Pike", ItemType.MartialWeapon);
        CreateItemFamily(context, newFamilies, "Rapier", ItemType.MartialWeapon);
        CreateItemFamily(context, newFamilies, "Scimitar", ItemType.MartialWeapon);
        CreateItemFamily(context, newFamilies, "Shortsword", ItemType.MartialWeapon);
        CreateItemFamily(context, newFamilies, "Trident", ItemType.MartialWeapon);
        CreateItemFamily(context, newFamilies, "War pick", ItemType.MartialWeapon);
        CreateItemFamily(context, newFamilies, "Warhammer", ItemType.MartialWeapon);
        CreateItemFamily(context, newFamilies, "Whip", ItemType.MartialWeapon);
        CreateItemFamily(context, newFamilies, "Blowgun", ItemType.MartialWeapon);
        CreateItemFamily(context, newFamilies, "Hand crossbow", ItemType.MartialWeapon);
        CreateItemFamily(context, newFamilies, "Heavy crossbow", ItemType.MartialWeapon);
        CreateItemFamily(context, newFamilies, "Longbow", ItemType.MartialWeapon);
        CreateItemFamily(context, newFamilies, "Net", ItemType.MartialWeapon);


        context.ItemFamilies.AddRange(newFamilies);
        await context.SaveChangesAsync();
    }
    private static void CreateItemFamily(AppDbContext context, List<ItemFamily> newFamilies, string name, ItemType itemType){
        if(!context.ItemFamilies.Where(itemFam => itemFam.Name == name).Any()){
            newFamilies.Add(new ItemFamily{Name = name, ItemType = itemType});
        }
    }

    public static async Task SeedRaces(AppDbContext context)
    {   

        List<Language> existingLanguages = context.Languages.ToList();
        if(context.Races.Where(race => race.Name == "Human").FirstOrDefault() == null){
            Race human = prepareRace("Human", Size.Medium, 30);

            ChoiceGroup grantedLanguage = new("Race language");
            Language commonLanguage = existingLanguages.Where(lang => lang.Name == "Common").First();
            LanguageEffectBlueprint commonLanguageKnown = new("Common language")
            {
                R_Language = commonLanguage
            };
            grantedLanguage.R_Effects.Add(commonLanguageKnown);

            ChoiceGroup abilityScoreIncrease = new("Human abilities");
            AbilityEffectBlueprint strengthBonus = new("Human strength", 1, RollMoment.OnCast);
            strengthBonus.AbilityEffectType.AbilityEffect = AbilityEffect.Bonus;
            strengthBonus.AbilityEffectType.AbilityEffect_Ability = Ability.STRENGTH;
            AbilityEffectBlueprint dexterityBonus = new("Human dexterity", 1, RollMoment.OnCast);
            dexterityBonus.AbilityEffectType.AbilityEffect = AbilityEffect.Bonus;
            dexterityBonus.AbilityEffectType.AbilityEffect_Ability = Ability.DEXTERITY;
            AbilityEffectBlueprint constitutionBonus = new("Human constitution", 1, RollMoment.OnCast);
            constitutionBonus.AbilityEffectType.AbilityEffect = AbilityEffect.Bonus;
            constitutionBonus.AbilityEffectType.AbilityEffect_Ability = Ability.CONSTITUTION;
            AbilityEffectBlueprint intelligenceBonus = new("Human intelligence", 1, RollMoment.OnCast);
            intelligenceBonus.AbilityEffectType.AbilityEffect = AbilityEffect.Bonus;
            intelligenceBonus.AbilityEffectType.AbilityEffect_Ability = Ability.INTELLIGENCE;
            AbilityEffectBlueprint wisdomBonus = new("Human wisdom", 1, RollMoment.OnCast);
            wisdomBonus.AbilityEffectType.AbilityEffect = AbilityEffect.Bonus;
            wisdomBonus.AbilityEffectType.AbilityEffect_Ability = Ability.WISDOM;
            AbilityEffectBlueprint charismaBonus = new("Human charisma", 1, RollMoment.OnCast);
            charismaBonus.AbilityEffectType.AbilityEffect = AbilityEffect.Bonus;
            charismaBonus.AbilityEffectType.AbilityEffect_Ability = Ability.CHARISMA;
            abilityScoreIncrease.R_Effects.Add(strengthBonus);
            abilityScoreIncrease.R_Effects.Add(dexterityBonus);
            abilityScoreIncrease.R_Effects.Add(constitutionBonus);
            abilityScoreIncrease.R_Effects.Add(intelligenceBonus);
            abilityScoreIncrease.R_Effects.Add(wisdomBonus);
            abilityScoreIncrease.R_Effects.Add(charismaBonus);

            ChoiceGroup additionalLanguageOfChoice = new ("Additional chosen language");
            foreach(Language language in existingLanguages){
                LanguageEffectBlueprint languageEffect = new(language.Name)
                {
                    R_Language = language
                };
                additionalLanguageOfChoice.R_Effects.Add(languageEffect);
            }
            additionalLanguageOfChoice.NumberToChoose = 1;
            
            RaceLevel firstLevel = human.R_RaceLevels.Where(rl => rl.Level == 1).First();
            firstLevel.R_ChoiceGroups.Add(grantedLanguage);
            firstLevel.R_ChoiceGroups.Add(abilityScoreIncrease);
            firstLevel.R_ChoiceGroups.Add(additionalLanguageOfChoice);
            
            context.Races.Add(human);
        }
        if(context.Races.Where(race => race.Name == "Elf").FirstOrDefault() == null){
            Race elf = prepareRace("Elf", Size.Medium, 30);

            ChoiceGroup languages = new("Race language");
            Language commonLanguage = existingLanguages.Where(lang => lang.Name == "Common").First();
            LanguageEffectBlueprint commonLanguageKnown = new("Common language")
            {
                R_Language = commonLanguage
            };
            Language elvishLanguage = existingLanguages.Where(lang => lang.Name == "Elvish").First();
            LanguageEffectBlueprint elvishLanguageKnown = new("Elvish language")
            {
                R_Language = elvishLanguage
            };
            languages.R_Effects.Add(commonLanguageKnown);
            languages.R_Effects.Add(elvishLanguageKnown);

            ChoiceGroup dexterityBonusGroup = new("Ability score increase");
            AbilityEffectBlueprint dexterityBonus = new("Elven dexterity", 2, RollMoment.OnCast);
            dexterityBonus.AbilityEffectType.AbilityEffect = AbilityEffect.Bonus;
            dexterityBonus.AbilityEffectType.AbilityEffect_Ability = Ability.DEXTERITY;
            dexterityBonus.DiceSet.flat = 2;
            dexterityBonusGroup.R_Effects.Add(dexterityBonus);

            ChoiceGroup keenSensesGroup = new("Keen senses");
            SkillEffectBlueprint perceptionProficienyEffect = new("Keen senses", 0, RollMoment.OnCast, SkillEffect.Proficiency, Skill.Perception);
            keenSensesGroup.R_Effects.Add(perceptionProficienyEffect);

            ChoiceGroup darkvisionGroup = new("Darkvision");
            DummyEffectBlueprint darkvision = new("Darkvision")
            {
                Description = @"Accustomed to twilit forests and the night 
                                                        sky, you have superior vision in dark and dim conditions. 
                                                        You can see in dim light within 60 feet of you as if it 
                                                        were bright light, and in darkness as if it were dim light. 
                                                        You can’t discern color in darkness, only shades of gray.",
                HasNoEffectInCombat = true
            };
            darkvisionGroup.R_Effects.Add(darkvision);

            ChoiceGroup feyAncestry = new("Fey Ancestry");
            SavingThrowEffectBlueprint feyAncestryA = new("Fey Ancestry A", 0, RollMoment.OnCast, SavingThrowEffect.Advantage, null, Condition.Charmed, null)
            {
                Conditional = true
            };
            SavingThrowEffectBlueprint feyAncestryB = new("Fey Ancestry B", 0, RollMoment.OnCast, SavingThrowEffect.AlwaysSucceed, null, Condition.Unconscious, AttackNature.Magical)
            {
                Conditional = true
            };
            feyAncestry.R_Effects.Add(feyAncestryA);
            feyAncestry.R_Effects.Add(feyAncestryB);


            RaceLevel firstLevel = elf.R_RaceLevels.Where(rl => rl.Level == 1).First();
            firstLevel.R_ChoiceGroups.Add(languages);
            firstLevel.R_ChoiceGroups.Add(dexterityBonusGroup);
            firstLevel.R_ChoiceGroups.Add(keenSensesGroup);
            firstLevel.R_ChoiceGroups.Add(darkvisionGroup);
            firstLevel.R_ChoiceGroups.Add(feyAncestry);

            context.Races.Add(elf);
        }
        if(context.Races.Where(race => race.Name == "Dwarf").FirstOrDefault() == null){
            Race dwarf = prepareRace("Dwarf", Size.Medium, 25);

            ChoiceGroup constitutionBonusGroup = new("Ability score increase");
            AbilityEffectBlueprint constitutionBonus = new("Dwarven constitution", 2, RollMoment.OnCast);
            constitutionBonus.AbilityEffectType.AbilityEffect = AbilityEffect.Bonus;
            constitutionBonus.AbilityEffectType.AbilityEffect_Ability = Ability.CONSTITUTION;
            constitutionBonusGroup.R_Effects.Add(constitutionBonus);

            ChoiceGroup languages = new("Race language");
            Language commonLanguage = existingLanguages.Where(lang => lang.Name == "Common").First();
            LanguageEffectBlueprint commonLanguageKnown = new("Common language")
            {
                R_Language = commonLanguage
            };
            Language dwarvishLanguage = existingLanguages.Where(lang => lang.Name == "Dwarvish").First(); //Tolkien spelling, best spelling
            LanguageEffectBlueprint dwarvishLanguageKnown = new("Dwarvish language")
            {
                R_Language = dwarvishLanguage
            };
            languages.R_Effects.Add(commonLanguageKnown);
            languages.R_Effects.Add(dwarvishLanguageKnown);

            ChoiceGroup darkvisionGroup = new("Darkvision");
            DummyEffectBlueprint darkvision = new("Darkvision")
            {
                Description = @"Accustomed to twilit forests and the night 
                                                        sky, you have superior vision in dark and dim conditions. 
                                                        You can see in dim light within 60 feet of you as if it 
                                                        were bright light, and in darkness as if it were dim light. 
                                                        You can’t discern color in darkness, only shades of gray.",
                HasNoEffectInCombat = true
            };
            darkvisionGroup.R_Effects.Add(darkvision);

            ChoiceGroup dwarvenResilience = new("Dwarven Resilience");
            SavingThrowEffectBlueprint dwarvenResilienceA = new("Dwarven Resilience A", 0, RollMoment.OnCast, SavingThrowEffect.Advantage, null, Condition.Poisoned, null);
            ResistanceEffectBlueprint dwarvenResilienceB = new("Dwarven Resilience B", ResistanceEffect.Resistance, DamageType.poison);
            dwarvenResilience.R_Effects.Add(dwarvenResilienceA);
            dwarvenResilience.R_Effects.Add(dwarvenResilienceB);

            ChoiceGroup dwarvenCombatTraining = new("Dwarven combat training");
            ProficiencyEffectBlueprint dwarvenCombatTraining_Battleaxe = new(
                context.ItemFamilies.Where(itemFam => itemFam.Name == "Battleaxe").First()
            );
            ProficiencyEffectBlueprint dwarvenCombatTraining_Handaxe = new(
                context.ItemFamilies.Where(itemFam => itemFam.Name == "Handaxe").First()
            );
            ProficiencyEffectBlueprint dwarvenCombatTraining_ThrowingHammer = new(
                context.ItemFamilies.Where(itemFam => itemFam.Name == "Light hammer").First()
            );
            ProficiencyEffectBlueprint dwarvenCombatTraining_Warhammer = new(
                context.ItemFamilies.Where(itemFam => itemFam.Name == "Warhammer").First()
            );
            dwarvenCombatTraining.R_Effects.Add(dwarvenCombatTraining_Battleaxe);
            dwarvenCombatTraining.R_Effects.Add(dwarvenCombatTraining_Handaxe);
            dwarvenCombatTraining.R_Effects.Add(dwarvenCombatTraining_ThrowingHammer);
            dwarvenCombatTraining.R_Effects.Add(dwarvenCombatTraining_Warhammer);

            ChoiceGroup stoneCunning = new("Stonecunning");
            SkillEffectBlueprint stonecunningA = new("Stonecunning A", 0, RollMoment.OnCast, SkillEffect.Expertise, Skill.History)
            {
                Conditional = true,
                HasNoEffectInCombat = true
            };
            stoneCunning.R_Effects.Add(stonecunningA);

            RaceLevel firstLevel = dwarf.R_RaceLevels.Where(rl => rl.Level == 1).First();
            firstLevel.R_ChoiceGroups.Add(constitutionBonusGroup);
            firstLevel.R_ChoiceGroups.Add(languages);
            firstLevel.R_ChoiceGroups.Add(darkvisionGroup);
            firstLevel.R_ChoiceGroups.Add(dwarvenResilience);
            firstLevel.R_ChoiceGroups.Add(dwarvenCombatTraining);
            firstLevel.R_ChoiceGroups.Add(stoneCunning);

            context.Races.Add(dwarf);
        }
        await context.SaveChangesAsync();
    }

    private static Race prepareRace(string name, Models.Enums.Size size, int speed){
        Race race = new() {Name = name, Size = size, Speed = speed};
        for(int i = 1; i <= 20; i++){
            race.R_RaceLevels.Add(new RaceLevel{Level = i, R_Race = race});
        }
        return race;
    }

    public static async Task SeedClasses(AppDbContext context){


        if(context.Classes.Where(characterClass => characterClass.Name == "Fighter").FirstOrDefault() == null){
            Class fighterClass = new("Fighter");
            for(int i = 1; i <= 20; i++){
                fighterClass.R_ClassLevels.Add(new(i){
                    HitDie = new DiceSet(){d10 = 1},
                    HitPoints = 6
                });
            }
            fighterClass.R_ClassLevels.Where(cl => cl.Level == 1).First().HitPoints = 10;

            ChoiceGroup savingThrowProficiency = new("Fighter saving throw proficiency");
            savingThrowProficiency.R_Effects.Add(new SavingThrowEffectBlueprint("Constitution saving throw proficiency", 0, RollMoment.OnCast, SavingThrowEffect.Proficiency, Ability.CONSTITUTION, null, null));
            savingThrowProficiency.R_Effects.Add(new SavingThrowEffectBlueprint("Strength saving throw proficiency", 0, RollMoment.OnCast, SavingThrowEffect.Proficiency, Ability.STRENGTH, null, null));
            

            ChoiceGroup armorProficiency = new("Fighter armor proficiency");
            ProficiencyEffectBlueprint heavyArmorProficiency = new(ItemType.HeavyArmor);
            ProficiencyEffectBlueprint mediumArmorProficiency = new(ItemType.MediumArmor);
            ProficiencyEffectBlueprint lightArmorProficiency = new(ItemType.LightArmor);
            armorProficiency.R_Effects.Add(heavyArmorProficiency);
            armorProficiency.R_Effects.Add(mediumArmorProficiency);
            armorProficiency.R_Effects.Add(lightArmorProficiency);

            ChoiceGroup simpleWeaponProficiency = new("Fighter weapon proficiency");
            simpleWeaponProficiency.R_Effects.Add(new ProficiencyEffectBlueprint(ItemType.SimpleWeapon));
            simpleWeaponProficiency.R_Effects.Add(new ProficiencyEffectBlueprint(ItemType.MartialWeapon));

            ChoiceGroup fighterSkillProficiency = new("Fighter skill proficiency");
            fighterSkillProficiency.R_Effects.Add(new SkillEffectBlueprint("Acrobatics", 0, RollMoment.OnCast, SkillEffect.Proficiency, Skill.Acrobatics));
            fighterSkillProficiency.R_Effects.Add(new SkillEffectBlueprint("Animal Handling", 0, RollMoment.OnCast, SkillEffect.Proficiency, Skill.Animal_Handling));
            fighterSkillProficiency.R_Effects.Add(new SkillEffectBlueprint("Athletics", 0, RollMoment.OnCast, SkillEffect.Proficiency, Skill.Athletics));
            fighterSkillProficiency.R_Effects.Add(new SkillEffectBlueprint("History", 0, RollMoment.OnCast, SkillEffect.Proficiency, Skill.History));
            fighterSkillProficiency.R_Effects.Add(new SkillEffectBlueprint("Insight", 0, RollMoment.OnCast, SkillEffect.Proficiency, Skill.Insight));
            fighterSkillProficiency.R_Effects.Add(new SkillEffectBlueprint("Intimidation", 0, RollMoment.OnCast, SkillEffect.Proficiency, Skill.Intimidation));
            fighterSkillProficiency.R_Effects.Add(new SkillEffectBlueprint("Perception", 0, RollMoment.OnCast, SkillEffect.Proficiency, Skill.Perception));
            fighterSkillProficiency.R_Effects.Add(new SkillEffectBlueprint("Survival", 0, RollMoment.OnCast, SkillEffect.Proficiency, Skill.Survival));
            fighterSkillProficiency.NumberToChoose = 2;

            ChoiceGroup fightingStyle = new("Fighting style");
            fightingStyle.R_Effects.Add(new AttackRollEffectBlueprint("Archery", 2, RollMoment.OnCast, AttackRollEffect_Type.Bonus, AttackRollEffect_Source.Weapon, AttackRollEffect_Range.Ranged){Description = "You gain a +2 bonus to attack rolls you make with ranged weapons."});
            fightingStyle.R_Effects.Add(new ArmorClassEffectBlueprint("Defense", 1, RollMoment.OnCast){Conditional = true, Description = "While you are wearing armor, you gain a +1 bonus to AC."});
            fightingStyle.R_Effects.Add(new AttackRollEffectBlueprint("Dueling", 1, RollMoment.OnCast, AttackRollEffect_Type.Bonus, AttackRollEffect_Source.Weapon, AttackRollEffect_Range.Melee){Conditional = true, Description = @"When you are wielding a melee weapon in one hand and 
                                                                                                                        no other weapons, you gain a +2 bonus to damage rolls 
                                                                                                                        with that weapon."});
            fightingStyle.R_Effects.Add(new AttackRollEffectBlueprint("Great Weapon Fighting", 3, RollMoment.OnCast, AttackRollEffect_Type.RerollLowerThan, AttackRollEffect_Source.Weapon, AttackRollEffect_Range.Melee){
                Conditional = true,
                Description = @"When you roll a 1 or 2 on a damage die for an attack you 
                                make with a melee weapon that you are wielding with 
                                two hands, you can reroll the die and must use the new 
                                roll, even if the new roll is a 1 or a 2. The weapon must 
                                have the two-handed or versatile property for you to gain 
                                this benefit."
                }
            );
            Power protection = new("Protection", ActionType.Reaction, CastableBy.Character, PowerType.PassiveEffect, TargetType.Character)
            {
                IsImplemented = false,
                Description = @"When a creature you can see attacks a target other 
                                than you that is within 5 feet of you, you can use your 
                                reaction to impose disadvantage on the attack roll. You 
                                must be wielding a shield."
            };
            fightingStyle.R_Powers.Add(protection);
            fightingStyle.R_Effects.Add(new OffHandAttackEffectBlueprint("Two-Weapon Fighting"));
            fightingStyle.NumberToChoose = 1;


            ChoiceGroup features = new("Fighter features");

            ImmaterialResourceBlueprint secondWindResource = new(){
                Name = "Second wind charge",
                RefreshesOn = RefreshType.ShortRest
            };
            fighterClass.R_ClassLevels.Where(cl => cl.Level == 1).First().R_ImmaterialResourceAmounts.Add(new ImmaterialResourceAmount(){Count = 1, Level = 1, R_Blueprint = secondWindResource});
            Power secondWind = new("Second wind", ActionType.BonusAction, CastableBy.Character, PowerType.PassiveEffect, TargetType.Caster){
                R_UsesImmaterialResource = secondWindResource
            };
            HealingEffectBlueprint secondWindHealing = new("Second wind", new DiceSet(){d10 = 1, additionalValues = [new(){additionalValueType = DiceSet.AdditionalValue.AdditionalValueType.LevelsInClass, R_LevelsInClass = fighterClass}]}, RollMoment.OnCast);// = DiceSet.AdditionalValue.LevelsInClass, R_LevelsInClass = fighterClass
            secondWind.R_EffectBlueprints.Add(secondWindHealing);
            
            Power actionSurge = new("Action surge", ActionType.None, CastableBy.Character, PowerType.PassiveEffect, TargetType.Caster){Duration = 1};
            ActionEffectBlueprint actionSurgeEffect = new("Action surge", 1, RollMoment.OnCast, ActionEffect.Action);
            actionSurge.R_EffectBlueprints.Add(actionSurgeEffect);
            
            AttackPerAttackActionEffectBlueprint extraAttack = new("Extra attack", 2, RollMoment.OnCast, AttackPerActionEffect.AttacksTotal);

            

            fighterClass.R_ClassLevels.Where(cl => cl.Level == 1).First().R_ChoiceGroups.AddRange(
                [savingThrowProficiency, armorProficiency, simpleWeaponProficiency, fighterSkillProficiency, fightingStyle, features]
                );

            context.Classes.Add(fighterClass);
            
        }
        if(context.Classes.Where(characterClass => characterClass.Name == "Wizard").FirstOrDefault() == null){
            Class wizardClass = new("Wizard");
            for(int i = 1; i <= 20; i++){
                wizardClass.R_ClassLevels.Add(new(i){
                    HitDie = new DiceSet(){d6 = 1},
                    HitPoints = 4
                });
            }
            wizardClass.R_ClassLevels.Where(cl => cl.Level == 1).First().HitPoints = 6;

            wizardClass.MaximumPreparedSpellsFormula = new DiceSet(){
                additionalValues = [
                    new DiceSet.AdditionalValue(){
                        additionalValueType = DiceSet.AdditionalValue.AdditionalValueType.AbilityScoreModifier, 
                        Ability = Ability.INTELLIGENCE
                    },
                    new DiceSet.AdditionalValue(){
                        additionalValueType = DiceSet.AdditionalValue.AdditionalValueType.LevelsInClass,
                        R_LevelsInClass = wizardClass
                    }
                ]
            };

            wizardClass.SpellcastingAbility = Ability.INTELLIGENCE;

            ChoiceGroup wizardSavingThrowProficiency = new("Wizard saving throw proficiency");
            wizardSavingThrowProficiency.R_Effects.Add(new SavingThrowEffectBlueprint("Intelligence saving throw proficiency", 0, RollMoment.OnCast, SavingThrowEffect.Proficiency, Ability.INTELLIGENCE, null, null));
            wizardSavingThrowProficiency.R_Effects.Add(new SavingThrowEffectBlueprint("Wisdom saving throw proficiency", 0, RollMoment.OnCast, SavingThrowEffect.Proficiency, Ability.WISDOM, null, null));

            
            ChoiceGroup wizardSkillProficiency = new("Wizard skill proficiency");
            wizardSkillProficiency.R_Effects.Add(new SkillEffectBlueprint("Arcana", 0, RollMoment.OnCast, SkillEffect.Proficiency, Skill.Arcana));
            wizardSkillProficiency.R_Effects.Add(new SkillEffectBlueprint("History", 0, RollMoment.OnCast, SkillEffect.Proficiency, Skill.History));
            wizardSkillProficiency.R_Effects.Add(new SkillEffectBlueprint("Insight", 0, RollMoment.OnCast, SkillEffect.Proficiency, Skill.Insight));
            wizardSkillProficiency.R_Effects.Add(new SkillEffectBlueprint("Investigation", 0, RollMoment.OnCast, SkillEffect.Proficiency, Skill.Investigation));
            wizardSkillProficiency.R_Effects.Add(new SkillEffectBlueprint("Medicine", 0, RollMoment.OnCast, SkillEffect.Proficiency, Skill.Medicine));
            wizardSkillProficiency.R_Effects.Add(new SkillEffectBlueprint("Religion", 0, RollMoment.OnCast, SkillEffect.Proficiency, Skill.Religion));
            wizardSkillProficiency.NumberToChoose = 2;

            ChoiceGroup wizardWeaponProficiency = new("Wizard weapon proficiency");
            wizardWeaponProficiency.R_Effects.Add(CreateProficiencyEffectBlueprint(context, "Dagger"));
            wizardWeaponProficiency.R_Effects.Add(CreateProficiencyEffectBlueprint(context, "Dart"));
            wizardWeaponProficiency.R_Effects.Add(CreateProficiencyEffectBlueprint(context, "Sling"));
            wizardWeaponProficiency.R_Effects.Add(CreateProficiencyEffectBlueprint(context, "Quarterstaff"));
            wizardWeaponProficiency.R_Effects.Add(CreateProficiencyEffectBlueprint(context, "Light crossbow"));

            ChoiceGroup wizardFeatures = new("Wizard features");
            ImmaterialResourceBlueprint arcaneRecoveryCharge = new()
            {
                Name = "Arcane recovery charge",
                RefreshesOn = RefreshType.LongRest
            };
            ImmaterialResourceAmount arcaneRecoveryChargeAmount = new()
            {
                Count = 1,
                Level = 0
            };
            arcaneRecoveryChargeAmount.R_Blueprint = arcaneRecoveryCharge;
            wizardClass.R_ClassLevels.Where(cl => cl.Level == 1).First().R_ImmaterialResourceAmounts.Add(arcaneRecoveryChargeAmount);
            Power arcaneRecoveryLevel1 = PrepareArcaneRecoveryPower(1, arcaneRecoveryCharge);
            Power arcaneRecoveryLevel2 = PrepareArcaneRecoveryPower(2, arcaneRecoveryCharge);
            Power arcaneRecoveryLevel3 = PrepareArcaneRecoveryPower(3, arcaneRecoveryCharge);
            Power arcaneRecoveryLevel4 = PrepareArcaneRecoveryPower(4, arcaneRecoveryCharge);
            Power arcaneRecoveryLevel5 = PrepareArcaneRecoveryPower(5, arcaneRecoveryCharge);
            Power arcaneRecoveryLevel6 = PrepareArcaneRecoveryPower(6, arcaneRecoveryCharge);
            wizardFeatures.R_Powers.Add(arcaneRecoveryLevel1);
            wizardFeatures.R_Powers.Add(arcaneRecoveryLevel2);
            wizardFeatures.R_Powers.Add(arcaneRecoveryLevel3);
            wizardFeatures.R_Powers.Add(arcaneRecoveryLevel4);
            wizardFeatures.R_Powers.Add(arcaneRecoveryLevel5);
            wizardFeatures.R_Powers.Add(arcaneRecoveryLevel6);
            wizardClass.R_ClassLevels.Where(cl => cl.Level == 1).First().R_ChoiceGroups.Add(wizardFeatures);

            ImmaterialResourceBlueprint spellSlot = new()
            {
                Name = "Spell slot",
                RefreshesOn = RefreshType.LongRest
            };
            ImmaterialResourceAmount spellSlot1Amount = new()
            {
                Level = 1,
                Count = 2,
                R_Blueprint = spellSlot
            };
            wizardClass.R_ClassLevels.Where(cl => cl.Level == 1).First().R_ImmaterialResourceAmounts.Add(spellSlot1Amount);

            context.Classes.Add(wizardClass);
        }
        if(context.Classes.Where(characterClass => characterClass.Name == "Rogue").FirstOrDefault() == null){

            Class rogueClass = new("Rogue");
            for(int i = 1; i <= 20; i++){
                rogueClass.R_ClassLevels.Add(new(i){
                    HitDie = new DiceSet(){d8 = 1},
                    HitPoints = 8
                });
            }
            rogueClass.R_ClassLevels.Where(cl => cl.Level == 1).First().HitPoints = 8;

            ChoiceGroup rogueArmorProficiency = new("Rogue armor proficiency");
            ProficiencyEffectBlueprint rogueLightArmorProficiency = CreateProficiencyEffectBlueprint(context, "Light armor");
            rogueArmorProficiency.R_Effects.Add(rogueLightArmorProficiency);

            ChoiceGroup rogueSimpleWeaponProficiency = new("Rogue simple weapon proficiency");
            //rogue simple weapon proficiency
            rogueSimpleWeaponProficiency.R_Effects.Add(CreateProficiencyEffectBlueprint(context, "Club"));
            rogueSimpleWeaponProficiency.R_Effects.Add(CreateProficiencyEffectBlueprint(context, "Dagger"));
            rogueSimpleWeaponProficiency.R_Effects.Add(CreateProficiencyEffectBlueprint(context, "Greatclub"));
            rogueSimpleWeaponProficiency.R_Effects.Add(CreateProficiencyEffectBlueprint(context, "Handaxe"));
            rogueSimpleWeaponProficiency.R_Effects.Add(CreateProficiencyEffectBlueprint(context, "Javelin"));
            rogueSimpleWeaponProficiency.R_Effects.Add(CreateProficiencyEffectBlueprint(context, "Light hammer"));
            rogueSimpleWeaponProficiency.R_Effects.Add(CreateProficiencyEffectBlueprint(context, "Mace"));
            rogueSimpleWeaponProficiency.R_Effects.Add(CreateProficiencyEffectBlueprint(context, "Quarterstaff"));
            rogueSimpleWeaponProficiency.R_Effects.Add(CreateProficiencyEffectBlueprint(context, "Sickle"));
            rogueSimpleWeaponProficiency.R_Effects.Add(CreateProficiencyEffectBlueprint(context, "Spear"));
            rogueSimpleWeaponProficiency.R_Effects.Add(CreateProficiencyEffectBlueprint(context, "Light crossbow"));
            rogueSimpleWeaponProficiency.R_Effects.Add(CreateProficiencyEffectBlueprint(context, "Dart"));
            rogueSimpleWeaponProficiency.R_Effects.Add(CreateProficiencyEffectBlueprint(context, "Shortbow"));
            rogueSimpleWeaponProficiency.R_Effects.Add(CreateProficiencyEffectBlueprint(context, "Sling"));

            ChoiceGroup rogueWeaponProficiency = new("Rogue weapon proficiency");
            rogueWeaponProficiency.R_Effects.Add(CreateProficiencyEffectBlueprint(context, "Hand crossbow"));
            rogueWeaponProficiency.R_Effects.Add(CreateProficiencyEffectBlueprint(context, "Longsword"));
            rogueWeaponProficiency.R_Effects.Add(CreateProficiencyEffectBlueprint(context, "Rapier"));
            rogueWeaponProficiency.R_Effects.Add(CreateProficiencyEffectBlueprint(context, "Shortsword"));

            ChoiceGroup rogueSavingThrowProficiency = new("Rogue saving throw proficiency");
            rogueSavingThrowProficiency.R_Effects.Add(new SavingThrowEffectBlueprint("Intelligence saving throw proficiency", 0, RollMoment.OnCast, SavingThrowEffect.Proficiency, Ability.INTELLIGENCE, null, null));
            rogueSavingThrowProficiency.R_Effects.Add(new SavingThrowEffectBlueprint("Dexterity saving throw proficiency", 0, RollMoment.OnCast, SavingThrowEffect.Proficiency, Ability.DEXTERITY, null, null));

            ChoiceGroup rogueSkillProficiency = new("Rogue skill proficiency");
            rogueSkillProficiency.R_Effects.Add(new SkillEffectBlueprint("Acrobatics", 0, RollMoment.OnCast, SkillEffect.Proficiency, Skill.Acrobatics));
            rogueSkillProficiency.R_Effects.Add(new SkillEffectBlueprint("Athletics", 0, RollMoment.OnCast, SkillEffect.Proficiency, Skill.Athletics));
            rogueSkillProficiency.R_Effects.Add(new SkillEffectBlueprint("Deception", 0, RollMoment.OnCast, SkillEffect.Proficiency, Skill.Deception));
            rogueSkillProficiency.R_Effects.Add(new SkillEffectBlueprint("Insight", 0, RollMoment.OnCast, SkillEffect.Proficiency, Skill.Insight));
            rogueSkillProficiency.R_Effects.Add(new SkillEffectBlueprint("Intimidation", 0, RollMoment.OnCast, SkillEffect.Proficiency, Skill.Intimidation));
            rogueSkillProficiency.R_Effects.Add(new SkillEffectBlueprint("Performance", 0, RollMoment.OnCast, SkillEffect.Proficiency, Skill.Performance));
            rogueSkillProficiency.R_Effects.Add(new SkillEffectBlueprint("Persuasion", 0, RollMoment.OnCast, SkillEffect.Proficiency, Skill.Persuasion));
            rogueSkillProficiency.R_Effects.Add(new SkillEffectBlueprint("Sleight_of_Hand", 0, RollMoment.OnCast, SkillEffect.Proficiency, Skill.Sleight_of_Hand));
            rogueSkillProficiency.R_Effects.Add(new SkillEffectBlueprint("Stealth", 0, RollMoment.OnCast, SkillEffect.Proficiency, Skill.Stealth));
            rogueSkillProficiency.NumberToChoose = 4;

            ChoiceGroup rogueExpertisePick = new("Rogue skill expertise");
            rogueExpertisePick.R_Effects.Add(new SkillEffectBlueprint("Expertise"));
            rogueExpertisePick.R_Effects.Add(new SkillEffectBlueprint("Expertise"));

            ChoiceGroup rogueFeatures = new("Rogue features");
            ImmaterialResourceBlueprint sneakAttackCharge = new()
            {
                Name = "Sneak attack charge",
                RefreshesOn = RefreshType.TurnStart
            };
            Power sneakAttack = new("Sneak attack", ActionType.WeaponAttack, CastableBy.OnWeaponHit, PowerType.PassiveEffect, TargetType.Character){UpcastByClassLevel = true, R_ClassForUpcasting = rogueClass, R_UsesImmaterialResource = sneakAttackCharge}; // since its castable on weapon attack, we dont need additional attack roll so power type is passive effect aka always hits and assigns its effects
            // this shouldnt count as an actual weapon attack. Lets do it so if character "casts" a power of OnWeaponHit type then whether it actually gets cast depends on whether automatically performed attack hits. This means we are going to need to go back to explicitly setting damage values on weapons
            DamageEffectBlueprint sneakAttackDamage1 = new("Sneak damage 1", new DiceSet(){d6 = 1}, RollMoment.OnCast){Level = 1};
            DamageEffectBlueprint sneakAttackDamage2 = new("Sneak damage 2", new DiceSet(){d6 = 2}, RollMoment.OnCast){Level = 3};
            DamageEffectBlueprint sneakAttackDamage3 = new("Sneak damage 3", new DiceSet(){d6 = 3}, RollMoment.OnCast){Level = 5};
            DamageEffectBlueprint sneakAttackDamage4 = new("Sneak damage 4", new DiceSet(){d6 = 4}, RollMoment.OnCast){Level = 7};
            DamageEffectBlueprint sneakAttackDamage5 = new("Sneak damage 5", new DiceSet(){d6 = 5}, RollMoment.OnCast){Level = 9};
            DamageEffectBlueprint sneakAttackDamage6 = new("Sneak damage 6", new DiceSet(){d6 = 6}, RollMoment.OnCast){Level = 11};
            DamageEffectBlueprint sneakAttackDamage7 = new("Sneak damage 7", new DiceSet(){d6 = 7}, RollMoment.OnCast){Level = 13};
            DamageEffectBlueprint sneakAttackDamage8 = new("Sneak damage 8", new DiceSet(){d6 = 8}, RollMoment.OnCast){Level = 15};
            DamageEffectBlueprint sneakAttackDamage9 = new("Sneak damage 9", new DiceSet(){d6 = 9}, RollMoment.OnCast){Level = 17};
            DamageEffectBlueprint sneakAttackDamage10 = new("Sneak damage 10", new DiceSet(){d6 = 10}, RollMoment.OnCast){Level = 19};
            sneakAttack.R_EffectBlueprints.AddRange(
                new List<DamageEffectBlueprint>{
                    sneakAttackDamage1,
                    sneakAttackDamage2,
                    sneakAttackDamage3,
                    sneakAttackDamage4,
                    sneakAttackDamage5,
                    sneakAttackDamage6,
                    sneakAttackDamage7,
                    sneakAttackDamage8,
                    sneakAttackDamage9,
                    sneakAttackDamage10,
                }
            );
            DummyEffectBlueprint cunningAction = new("Cunning action"){IsImplemented = false};
            DummyEffectBlueprint uncannyDodge = new("Uncanny dodge"){IsImplemented = false};
            DummyEffectBlueprint evasion = new("Evasion"){IsImplemented = false};
            DummyEffectBlueprint reliableTalent = new("Reliable talent"){IsImplemented = false};
            DummyEffectBlueprint blindsense = new("Blindsense"){IsImplemented = false};
            SavingThrowEffectBlueprint slipperyMind = new("Slippery mind", 0, RollMoment.OnCast, SavingThrowEffect.Proficiency, Ability.WISDOM, null, null);
            DummyEffectBlueprint elusive = new("Slippery mind"){IsImplemented = false};
            DummyEffectBlueprint strokeOfLuck = new("Stroke of luck"){IsImplemented = false};
            ImmaterialResourceBlueprint strokeOfLuckCharge = new()
            {
                Name = "Stroke of luck charge",
                RefreshesOn = RefreshType.ShortRest
            };
            ImmaterialResourceAmount strokeOfLuckAmount = new();
            strokeOfLuckAmount.R_Blueprint = strokeOfLuckCharge;
            strokeOfLuckAmount.Level = 1;
            strokeOfLuckAmount.Count = 1;

            context.Classes.Add(rogueClass);

        }
        await context.SaveChangesAsync();
    }
    private static ProficiencyEffectBlueprint CreateProficiencyEffectBlueprint(AppDbContext context, string name){
        return new(context.ItemFamilies.Where(itemFam => itemFam.Name == name).First());
    }

    private static Power PrepareArcaneRecoveryPower(int level, ImmaterialResourceBlueprint arcaneRecoveryCharge){
        Power arcaneRecoveryLevel = new("Arcane Recovery " + level, ActionType.Action, CastableBy.Character, PowerType.PassiveEffect, TargetType.Caster)
        {
            IsImplemented = false,
            R_UsesImmaterialResource = arcaneRecoveryCharge
        };
        DummyEffectBlueprint arcaneRecoveryEffect = new("Arcane Recovery " + level)
        {
            Level = 0,
            ResourceAmount = level
        };
        arcaneRecoveryLevel.R_EffectBlueprints.Add(arcaneRecoveryEffect);
        return arcaneRecoveryLevel;
    }
}
