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

    public static async Task SeedItemFamilies(AppDbContext context){
        List<ItemFamily> newFamilies = new();
        CreateItemFamily(context, newFamilies, "Battleaxe");
        CreateItemFamily(context, newFamilies, "Handaxe");
        CreateItemFamily(context, newFamilies, "Throwing hammer");
        CreateItemFamily(context, newFamilies, "Warhammer");
        context.ItemFamilies.AddRange(newFamilies);
        await context.SaveChangesAsync();
    }
    private static void CreateItemFamily(AppDbContext context, List<ItemFamily> newFamilies, string name){
        if(!context.ItemFamilies.Where(itemFam => itemFam.Name == name).Any()){
            newFamilies.Add(new ItemFamily{Name = name});
        }
    }

    public static async Task SeedRaces(AppDbContext context)
    {   

        List<Language> existingLanguages = context.Languages.ToList();
        if(context.Races.Where(race => race.Name == "Human").FirstOrDefault() == null){
            Race human = prepareRace("Human", Size.Medium, 30);

            ChoiceGroup grantedLanguage = new();
            Language commonLanguage = existingLanguages.Where(lang => lang.Name == "Common").FirstOrDefault() ?? new Language{Name = "Common"};
            LanguageEffectBlueprint commonLanguageKnown = new()
            {
                R_Language = commonLanguage,
                Name = "Common language"
            };
            grantedLanguage.R_Effects.Add(commonLanguageKnown);

            ChoiceGroup abilityScoreIncrease = new();
            AbilityEffectBlueprint strengthBonus = new()
            {
                Name = "Ability score increase"
            };
            strengthBonus.AbilityEffectType.AbilityEffect = AbilityEffect.Bonus;
            strengthBonus.AbilityEffectType.AbilityEffect_Ability = Ability.STRENGTH;
            strengthBonus.DiceSet.flat = 1;
            AbilityEffectBlueprint dexterityBonus = new()
            {
                Name = "Ability score increase"
            };
            dexterityBonus.AbilityEffectType.AbilityEffect = AbilityEffect.Bonus;
            dexterityBonus.AbilityEffectType.AbilityEffect_Ability = Ability.DEXTERITY;
            dexterityBonus.DiceSet.flat = 1;
            AbilityEffectBlueprint constitutionBonus = new()
            {
                Name = "Ability score increase"
            };
            constitutionBonus.AbilityEffectType.AbilityEffect = AbilityEffect.Bonus;
            constitutionBonus.AbilityEffectType.AbilityEffect_Ability = Ability.CONSTITUTION;
            constitutionBonus.DiceSet.flat = 1;
            AbilityEffectBlueprint intelligenceBonus = new()
            {
                Name = "Ability score increase"
            };
            intelligenceBonus.AbilityEffectType.AbilityEffect = AbilityEffect.Bonus;
            intelligenceBonus.AbilityEffectType.AbilityEffect_Ability = Ability.INTELLIGENCE;
            intelligenceBonus.DiceSet.flat = 1;
            AbilityEffectBlueprint wisdomBonus = new()
            {
                Name = "Ability score increase"
            };
            wisdomBonus.AbilityEffectType.AbilityEffect = AbilityEffect.Bonus;
            wisdomBonus.AbilityEffectType.AbilityEffect_Ability = Ability.WISDOM;
            wisdomBonus.DiceSet.flat = 1;
            AbilityEffectBlueprint charismaBonus = new()
            {
                Name = "Ability score increase"
            };
            charismaBonus.AbilityEffectType.AbilityEffect = AbilityEffect.Bonus;
            charismaBonus.AbilityEffectType.AbilityEffect_Ability = Ability.CHARISMA;
            charismaBonus.DiceSet.flat = 1;
            abilityScoreIncrease.R_Effects.Add(strengthBonus);
            abilityScoreIncrease.R_Effects.Add(dexterityBonus);
            abilityScoreIncrease.R_Effects.Add(constitutionBonus);
            abilityScoreIncrease.R_Effects.Add(intelligenceBonus);
            abilityScoreIncrease.R_Effects.Add(wisdomBonus);
            abilityScoreIncrease.R_Effects.Add(charismaBonus);

            ChoiceGroup additionalLanguageOfChoice = new ();
            foreach(Language language in existingLanguages){
                LanguageEffectBlueprint languageEffect = new()
                {
                    R_Language = language,
                    Name = "Additional chosen language"
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

            ChoiceGroup languages = new();
            Language commonLanguage = existingLanguages.Where(lang => lang.Name == "Common").FirstOrDefault() ?? new Language{Name = "Common"};
            LanguageEffectBlueprint commonLanguageKnown = new()
            {
                R_Language = commonLanguage,
                Name = "Common language"
            };
            Language elvishLanguage = existingLanguages.Where(lang => lang.Name == "Elvish").FirstOrDefault() ?? new Language{Name = "Elvish"};
            LanguageEffectBlueprint elvishLanguageKnown = new()
            {
                R_Language = elvishLanguage,
                Name = "Elvish language"
            };
            languages.R_Effects.Add(commonLanguageKnown);
            languages.R_Effects.Add(elvishLanguageKnown);

            ChoiceGroup dexterityBonusGroup = new();
            AbilityEffectBlueprint dexterityBonus = new()
            {
                Name = "Ability score increase"
            };
            dexterityBonus.AbilityEffectType.AbilityEffect = AbilityEffect.Bonus;
            dexterityBonus.AbilityEffectType.AbilityEffect_Ability = Ability.DEXTERITY;
            dexterityBonus.DiceSet.flat = 2;
            dexterityBonusGroup.R_Effects.Add(dexterityBonus);

            ChoiceGroup keenSensesGroup = new();
            SkillEffectBlueprint perceptionProficienyEffect = new()
            {
                Name = "Keen senses"
            };
            perceptionProficienyEffect.SkillEffectType.SkillEffect = SkillEffect.Proficiency;
            perceptionProficienyEffect.SkillEffectType.SkillEffect_Skill = Skill.Perception;
            keenSensesGroup.R_Effects.Add(perceptionProficienyEffect);

            ChoiceGroup darkvisionGroup = new();
            EffectBlueprint darkvision = new()
            {
                Name = "Darkvision",
                Description = @"Accustomed to twilit forests and the night 
                                                        sky, you have superior vision in dark and dim conditions. 
                                                        You can see in dim light within 60 feet of you as if it 
                                                        were bright light, and in darkness as if it were dim light. 
                                                        You can’t discern color in darkness, only shades of gray.",
                HasNoEffectInCombat = true
            };
            darkvisionGroup.R_Effects.Add(darkvision);

            ChoiceGroup feyAncestry = new();
            SavingThrowEffectBlueprint feyAncestryA = new()
            {
                Name = "Fey Ancestry A",
                Conditional = true
            };
            feyAncestryA.SavingThrowEffectType.SavingThrowEffect = SavingThrowEffect.Advantage;
            feyAncestryA.SavingThrowEffectType.SavingThrowEffect_Ability = null;
            feyAncestryA.SavingThrowEffectType.SavingThrowEffect_Condition = Condition.Charmed;
            SavingThrowEffectBlueprint feyAncestryB = new()
            {
                Name = "Fey Ancestry B",
                Conditional = true
            };
            feyAncestryB.SavingThrowEffectType.SavingThrowEffect = SavingThrowEffect.AlwaysSucceed;
            feyAncestryB.SavingThrowEffectType.SavingThrowEffect_Ability = null;
            feyAncestryB.SavingThrowEffectType.SavingThrowEffect_Condition = Condition.Unconscious;
            feyAncestryB.SavingThrowEffectType.SavingThrowEffect_Nature = AttackNature.Magical;
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

            ChoiceGroup constitutionBonusGroup = new();
            AbilityEffectBlueprint constitutionBonus = new()
            {
                Name = "Ability score increase"
            };
            constitutionBonus.AbilityEffectType.AbilityEffect = AbilityEffect.Bonus;
            constitutionBonus.AbilityEffectType.AbilityEffect_Ability = Ability.CONSTITUTION;
            constitutionBonus.DiceSet.flat = 2;
            constitutionBonusGroup.R_Effects.Add(constitutionBonus);

            ChoiceGroup languages = new();
            Language commonLanguage = existingLanguages.Where(lang => lang.Name == "Common").FirstOrDefault() ?? new Language{Name = "Common"};
            EffectBlueprint commonLanguageKnown = new LanguageEffectBlueprint{
                R_Language = commonLanguage,
                Name = "Common language"
            };
            Language dwarvishLanguage = existingLanguages.Where(lang => lang.Name == "Dwarvish").FirstOrDefault() ?? new Language{Name = "Dwarvish"}; //Tolkien spelling, best spelling
            EffectBlueprint dwarvishLanguageKnown = new LanguageEffectBlueprint{
                R_Language = dwarvishLanguage,
                Name = "Dwarvish language"
            };
            languages.R_Effects.Add(commonLanguageKnown);
            languages.R_Effects.Add(dwarvishLanguageKnown);

            ChoiceGroup darkvisionGroup = new();
            EffectBlueprint darkvision = new()
            {
                Name = "Darkvision",
                Description = @"Accustomed to twilit forests and the night 
                                                        sky, you have superior vision in dark and dim conditions. 
                                                        You can see in dim light within 60 feet of you as if it 
                                                        were bright light, and in darkness as if it were dim light. 
                                                        You can’t discern color in darkness, only shades of gray.",
                HasNoEffectInCombat = true
            };
            darkvisionGroup.R_Effects.Add(darkvision);

            ChoiceGroup dwarvenResilience = new();
            SavingThrowEffectBlueprint dwarvenResilienceA = new()
            {
                Name = "Dwarven Resilience A"
            };
            dwarvenResilienceA.SavingThrowEffectType.SavingThrowEffect = SavingThrowEffect.Advantage;
            dwarvenResilienceA.SavingThrowEffectType.SavingThrowEffect_Condition = Condition.Poisoned;
            ResistanceEffectBlueprint dwarvenResilienceB = new()
            {
                Name = "Dwarven Resilience B"
            };
            dwarvenResilienceB.ResistanceEffectType.ResistanceEffect = ResistanceEffect.Resistance;
            dwarvenResilienceB.ResistanceEffectType.ResistanceEffect_DamageType = DamageType.poison;
            dwarvenResilience.R_Effects.Add(dwarvenResilienceA);
            dwarvenResilience.R_Effects.Add(dwarvenResilienceB);

            ChoiceGroup dwarvenCombatTraining = new();
            ProficiencyEffectBlueprint dwarvenCombatTraining_Battleaxe = new()
            {
                Name = "Dwarven Combat Training - Battleaxe"
            };
            dwarvenCombatTraining_Battleaxe.ProficiencyEffectType.ProficiencyEffect = ProficiencyEffect.Weapon;
            dwarvenCombatTraining_Battleaxe.R_GrantsProficiencyInItemFamily = context.ItemFamilies.Where(itemFam => itemFam.Name == "Battleaxe").First();
            ProficiencyEffectBlueprint dwarvenCombatTraining_Handaxe = new()
            {
                Name = "Dwarven Combat Training - Handaxe"
            };
            dwarvenCombatTraining_Handaxe.ProficiencyEffectType.ProficiencyEffect = ProficiencyEffect.Weapon;
            dwarvenCombatTraining_Handaxe.R_GrantsProficiencyInItemFamily = context.ItemFamilies.Where(itemFam => itemFam.Name == "Handaxe").First();
            ProficiencyEffectBlueprint dwarvenCombatTraining_ThrowingHammer = new()
            {
                Name = "Dwarven Combat Training - Handaxe"
            };
            dwarvenCombatTraining_ThrowingHammer.ProficiencyEffectType.ProficiencyEffect = ProficiencyEffect.Weapon;
            dwarvenCombatTraining_ThrowingHammer.R_GrantsProficiencyInItemFamily = context.ItemFamilies.Where(itemFam => itemFam.Name == "Throwing hammer").First();
            ProficiencyEffectBlueprint dwarvenCombatTraining_Warhammer = new()
            {
                Name = "Dwarven Combat Training - Warhammer"
            };
            dwarvenCombatTraining_Warhammer.ProficiencyEffectType.ProficiencyEffect = ProficiencyEffect.Weapon;
            dwarvenCombatTraining_Warhammer.R_GrantsProficiencyInItemFamily = context.ItemFamilies.Where(itemFam => itemFam.Name == "Warhammer").First();
            dwarvenCombatTraining.R_Effects.Add(dwarvenCombatTraining_Battleaxe);
            dwarvenCombatTraining.R_Effects.Add(dwarvenCombatTraining_Handaxe);
            dwarvenCombatTraining.R_Effects.Add(dwarvenCombatTraining_ThrowingHammer);
            dwarvenCombatTraining.R_Effects.Add(dwarvenCombatTraining_Warhammer);

            ChoiceGroup stoneCunning = new();
            SkillEffectBlueprint stonecunningA = new()
            {
                Name = "Stonecunning A"
            };
            stonecunningA.SkillEffectType.SkillEffect = SkillEffect.Expertise;
            stonecunningA.SkillEffectType.SkillEffect_Skill = Skill.History;
            stonecunningA.Conditional = true;
            stonecunningA.HasNoEffectInCombat = true;
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
}
