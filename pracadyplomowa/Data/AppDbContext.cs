using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using pracadyplomowa.Models;
using pracadyplomowa.Models.Entities;
using pracadyplomowa.Models.Entities.Campaign;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Items;
using pracadyplomowa.Models.Entities.Powers;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;
using pracadyplomowa.Models.Entities.Powers.EffectInstances;

namespace pracadyplomowa;

public class AppDbContext : IdentityDbContext<User, Role, int,
    IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
    IdentityRoleClaim<int>, IdentityUserToken<int>>
{
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        // Identity
        public DbSet<ObjectWithOwner> Objects { get; set; }

        // Campaigns
        public DbSet<ActionLog> ActionLogs { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Encounter> Encounters { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<ParticipanceData> ParticipanceDatas { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<ShopItem> ShopItems { get; set; }

        // Characters
        public DbSet<Character> Characters { get; set; }
        public DbSet<ChoiceGroup> ChoiceGroups { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<ClassLevel> ClassLevels { get; set; }
        //public DbSet<DiceSet> DiceSet { get; set; }
        public DbSet<EquipData> EquipDatas { get; set; }
        public DbSet<EquipmentSlot> EquipmentSlots { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<RaceLevel> RaceLevels { get; set; }

        // Items
        public DbSet<Item> Items { get; set; }
        public DbSet<Apparel> Apparels { get; set; }
        public DbSet<Backpack> Backpacks { get; set; }
        public DbSet<ItemCostRequirement> ItemCostRequirements { get; set; }
        public DbSet<ItemFamily> ItemFamilies { get; set; }
        // public DbSet<Purse> Purses { get; set; }
        public DbSet<Tool> Tools { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<MeleeWeapon> MeleeWeapons { get; set; }
        public DbSet<RangedWeapon> RangedWeapons { get; set; }

        // Powers
        public DbSet<Aura> Auras { get; set; }
        public DbSet<EffectBlueprint> EffectBlueprints { get; set; }
        public DbSet<EffectGroup> EffectGroups { get; set; }
        public DbSet<EffectInstance> EffectInstances { get; set; }
        public DbSet<ImmaterialResourceAmount> ImmaterialResourceAmounts { get; set; }
        public DbSet<ImmaterialResourceBlueprint> ImmaterialResourceBlueprints { get; set; }
        public DbSet<ImmaterialResourceInstance> ImmaterialResourceInstances { get; set; }
        public DbSet<Power> Powers { get; set; }

        
        public DbSet<AbilityEffectBlueprint> AbilityEffectBlueprints { get; set; }
        public DbSet<ActionEffectBlueprint> ActionEffectBlueprints { get; set; }
        public DbSet<ArmorClassEffectBlueprint> ArmorClassEffectBlueprints { get; set; }
        public DbSet<AttackPerAttackActionEffectBlueprint> AttackPerAttackActionEffectBlueprints { get; set; }
        public DbSet<AttackRollEffectBlueprint> AttackRollEffectBlueprints { get; set; }
        public DbSet<DamageEffectBlueprint> DamageEffectBlueprints { get; set; }
        public DbSet<HealingEffectBlueprint> HealingEffectBlueprints { get; set; }
        public DbSet<HitpointEffectBlueprint> HitpointEffectBlueprints { get; set; }
        public DbSet<InitiativeEffectBlueprint> InitiativeEffectBlueprints { get; set; }
        public DbSet<MagicEffectBlueprint> MagicEffectBlueprints { get; set; }
        public DbSet<MovementCostEffectBlueprint> MovementCostEffectBlueprints { get; set; }
        public DbSet<MovementEffectBlueprint> MovementEffectBlueprints { get; set; }
        public DbSet<ProficiencyEffectBlueprint> ProficiencyEffectBlueprints { get; set; }
        public DbSet<ResistanceEffectBlueprint> ResistanceEffectBlueprints { get; set; }
        public DbSet<SavingThrowEffectBlueprint> SavingThrowEffectBlueprints { get; set; }
        public DbSet<SizeEffectBlueprint> SizeEffectBlueprints { get; set; }
        public DbSet<SkillEffectBlueprint> SkillEffectBlueprints { get; set; }
        public DbSet<StatusEffectBlueprint> StatusEffectBlueprints { get; set; }
        public DbSet<ValueEffectBlueprint> ValueEffectBlueprints { get; set; }
        public DbSet<DummyEffectBlueprint> DummyEffectBlueprints { get; set; }
        public DbSet<OffHandAttackEffectBlueprint> OffHandEffectBlueprints { get; set; }
        public DbSet<LanguageEffectBlueprint> LanguageEffectBlueprints { get; set; }
        
        public DbSet<AbilityEffectInstance> AbilityEffectInstances { get; set; }
        public DbSet<ActionEffectInstance> ActionEffectInstances { get; set; }
        public DbSet<ArmorClassEffectInstance> ArmorClassEffectInstances { get; set; }
        public DbSet<AttackPerAttackActionEffectInstance> AttackPerAttackActionEffectInstances { get; set; }
        public DbSet<AttackRollEffectInstance> AttackRollEffectInstances { get; set; }
        public DbSet<DamageEffectInstance> DamageEffectInstances { get; set; }
        public DbSet<HealingEffectInstance> HealingEffectInstances { get; set; }
        public DbSet<HitpointEffectInstance> HitpointEffectInstances { get; set; }
        public DbSet<MovementCostEffectInstance> MovementCostEffectInstances { get; set; }
        public DbSet<MovementEffectInstance> MovementEffectInstances { get; set; }
        public DbSet<ProficiencyEffectInstance> ProficiencyEffectInstances { get; set; }
        public DbSet<ResistanceEffectInstance> ResistanceEffectInstances { get; set; }
        public DbSet<SavingThrowEffectInstance> SavingThrowEffectInstances { get; set; }
        public DbSet<SizeEffectInstance> SizeEffectInstances { get; set; }
        public DbSet<SkillEffectInstance> SkillEffectInstances { get; set; }
        public DbSet<StatusEffectInstance> StatusEffectInstances { get; set; }
        public DbSet<ValueEffectInstance> ValueEffectInstances { get; set; }
        public DbSet<DummyEffectInstance> DummyEffectInstances { get; set; }
        public DbSet<OffHandAttackEffectInstance> OffHandEffectInstances { get; set; }
        public DbSet<LanguageEffectInstance> LanguageEffectInstances { get; set; }
        public DbSet<ChoiceGroupUsage> ChoiceGroupUsages {get; set;}
        public DbSet<Language> Languages {get; set;}
        public DbSet<PowerSelection> PowerSelections { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
                foreach (var entityType in builder.Model.GetEntityTypes())
                {
                        foreach (var property in entityType.GetProperties())
                        {
                        if (property.ClrType == typeof(DateTime))
                        {
                                property.SetValueConverter(new ValueConverter<DateTime, DateTime>(
                                v => v.ToUniversalTime(),
                                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
                                ));
                        }
                        }
                }
                base.OnModelCreating(builder);

                builder.Entity<User>()
                        .HasMany(ur => ur.UserRoles)
                        .WithOne(u => u.User)
                        .HasForeignKey(ur => ur.UserId)
                        .IsRequired();

                builder.Entity<Role>()
                        .HasMany(ur => ur.UserRoles)
                        .WithOne(u => u.Role)
                        .HasForeignKey(ur => ur.RoleId)
                        .IsRequired();


                builder.Entity<ObjectWithOwner>().UseTptMappingStrategy();
                builder.Entity<ObjectWithOwner>()
                        .HasKey(i => i.Id);
                builder.Entity<ObjectWithOwner>()
                        .HasOne(i => i.R_Owner)
                        .WithMany(o => o.R_Objects)
                        .HasForeignKey(i => i.R_OwnerId)
                        .IsRequired(false)
                        .OnDelete(DeleteBehavior.Cascade);

                builder.Entity<Character>()
                        .HasOne(c => c.R_ConcentratesOn)
                        .WithOne(c => c.R_ConcentratedOnByCharacter)
                        .HasForeignKey<EffectGroup>(c => c.R_ConcentratedOnByCharacterId)
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired(false);

                builder.Entity<Character>()
                        .HasMany(c => c.R_AffectedBy)
                        .WithOne(c => c.R_TargetedCharacter)
                        .OnDelete(DeleteBehavior.Cascade);

                builder.Entity<Character>()
                        .HasMany(c => c.R_PowersKnown)
                        .WithMany(c => c.R_CharacterKnownsPowers);

                builder.Entity<Character>()
                        .HasMany(c => c.R_PowersPrepared)
                        .WithOne(c => c.R_Character)
                        .OnDelete(DeleteBehavior.Cascade);

                builder.Entity<Character>()
                        .HasOne(c => c.R_SpawnedByPower)
                        .WithMany(c => c.R_SpawnedCharacters)
                        .HasForeignKey(c => c.R_SpawnedByPowerId)
                        .IsRequired(false);

                // builder.Entity<Weapon>()
                //         .HasMany(c => c.R_PowersCastedOnHit)
                //         .WithMany(c => c.R_WeaponsCastingOnHit);

                builder.Entity<EffectBlueprint>()
                        .HasOne(c => c.R_CastedOnCharactersByAura)
                        .WithMany(c => c.R_EffectsOnCharactersInRange)
                        .HasForeignKey(c => c.R_CastedOnCharactersByAuraId)
                        .IsRequired(false);

                builder.Entity<EffectBlueprint>()
                        .HasOne(c => c.R_CastedOnTilesByAura)
                        .WithMany(c => c.R_EffectsOnTilesInRange)
                        .HasForeignKey(c => c.R_CastedOnTilesByAuraId)
                        .IsRequired(false);

                builder.Entity<EffectGroup>()
                        .HasOne(c => c.R_GeneratesAura)
                        .WithOne(c => c.R_GeneratedBy)
                        .HasForeignKey<EffectGroup>(c => c.R_GeneratesAuraId)
                        .IsRequired(false);
                builder.Entity<EffectGroup>()
                        .HasMany(c => c.R_OwnedEffects)
                        .WithOne(e => e.R_OwnedByGroup)
                        .HasForeignKey(c => c.R_OwnedByGroupId)
                        .IsRequired(false)
                        .OnDelete(DeleteBehavior.Cascade);
                
                builder.Entity<Field>()
                        .HasOne(c => c.R_OccupiedBy)
                        .WithOne(c => c.R_OccupiedField)
                        .HasForeignKey<Field>(c => c.R_OccupiedById)
                        .IsRequired(false);

                // builder.Entity<EffectGroup>()
                //         .HasOne(c => c.R_OriginatesFromAura)
                //         .WithMany(c => c.R_OwnedEffectGroups)
                //         .HasForeignKey(c => c.R_OriginatesFromAuraId)
                //         .IsRequired(false);

                // builder.Entity<EffectGroup>()
                //         .HasOne(c => c.R_ItemAffectedBy)
                //         .WithMany(c => c.R_EffectGroupAffectedBy)
                //         .HasForeignKey(c => c.R_ItemAffectedById)
                //         .IsRequired(false);

                // builder.Entity<EffectGroup>()
                //         .HasOne(c => c.R_ItemGiveEffect)
                //         .WithMany(c => c.R_EffectGroupFromItem)
                //         .HasForeignKey(c => c.R_ItemGiveEffectId)
                //         .IsRequired(false);
                builder.Entity<Item>()
                        .HasMany(c => c.R_AffectedBy)
                        .WithOne(ei => ei.R_TargetedItem)
                        .HasForeignKey(ei => ei.R_TargetedItemId)
                        .IsRequired(false);
                builder.Entity<Item>().Navigation(i => i.R_ItemInItemsFamily).AutoInclude();
                builder.Entity<EquipData>().Navigation(ed => ed.R_Slots).AutoInclude();
                // builder.Entity<Item>()
                //         .HasMany(c => c.R_ItemCreateEffectsOnEquip)
                //         .WithOne(ei => ei.R_CreatedByEquipping)
                //         .HasForeignKey(ei => ei.R_CreatedByEquippingId)
                //         .IsRequired(false);

                
                builder.Entity<Class>()
                        .HasMany(c => c.R_AccessiblePowers)
                        .WithMany(p => p.R_ClassesWithAccess);
                
                builder.Entity<Class>()
                        .HasMany(c => c.R_UsedForUpcastingOfPowers)
                        .WithOne(p => p.R_ClassForUpcasting)
                        .HasForeignKey(p => p.R_ClassForUpcastingId)
                        .IsRequired(false);


                builder.Entity<Item>().UseTptMappingStrategy();
                builder.Entity<EffectBlueprint>().UseTphMappingStrategy();
                builder.Entity<EffectInstance>().UseTphMappingStrategy();
                builder.Entity<ValueEffectBlueprint>()
                        .HasOne(veb => veb.DiceSet)
                        .WithOne(ds => ds.R_ValueEffectBlueprint)
                        .HasForeignKey<DiceSet>(ds => ds.R_ValueEffectBlueprintId)
                        .OnDelete(DeleteBehavior.Cascade);
                        
                builder.Entity<ValueEffectInstance>().Navigation(e=>e.DiceSet).AutoInclude();
                builder.Entity<ValueEffectBlueprint>().Navigation(e=>e.DiceSet).AutoInclude();
                builder.Entity<DiceSet>().Navigation(e=>e.additionalValues).AutoInclude();
                builder.Entity<DiceSet.AdditionalValue>().Navigation(e=>e.R_LevelsInClass).AutoInclude();
                builder.Entity<LanguageEffectBlueprint>().Navigation(e=>e.R_Language).AutoInclude();
                builder.Entity<LanguageEffectBlueprint>()
                        .HasOne(lei => lei.R_Language)
                        .WithMany(l => l.R_EffectBlueprints)
                        .HasForeignKey(lei => lei.R_LanguageId);
                builder.Entity<LanguageEffectInstance>().Navigation(e=>e.R_Language).AutoInclude();
                builder.Entity<LanguageEffectInstance>()
                        .HasOne(lei => lei.R_Language)
                        .WithMany(l => l.R_EffectInstances)
                        .HasForeignKey(lei => lei.R_LanguageId);
                builder.Entity<ProficiencyEffectBlueprint>().Navigation(e=>e.R_GrantsProficiencyInItemFamily).AutoInclude();
                builder.Entity<ProficiencyEffectInstance>().Navigation(e=>e.R_GrantsProficiencyInItemFamily).AutoInclude();

                builder.Entity<ChoiceGroup>().HasMany(cg => cg.R_PowersAlwaysAvailable).WithMany(p => p.R_AlwaysAvailableThroughChoiceGroup);
                builder.Entity<ChoiceGroup>().HasMany(cg => cg.R_PowersToPrepare).WithMany(p => p.R_ToPrepareThroughChoiceGroups);
                builder.Entity<ChoiceGroupUsage>().HasMany(cg => cg.R_PowersAlwaysAvailableGranted).WithMany(p => p.R_AlwaysAvailableThroughChoiceGroupUsage);
                builder.Entity<ChoiceGroupUsage>().HasMany(cg => cg.R_PowersToPrepareGranted).WithMany(p => p.R_ToPrepareThroughChoiceGroupUsage);
                builder.Entity<ChoiceGroupUsage>().HasMany(cg => cg.R_ResourcesGranted).WithOne(r => r.R_ChoiceGroupUsage).OnDelete(DeleteBehavior.Cascade);
                builder.Entity<ImmaterialResourceInstance>().Navigation(i => i.R_Blueprint).AutoInclude();
                builder.Entity<Weapon>().Navigation(i => i.DamageValue).AutoInclude();
                builder.Entity<MeleeWeapon>().Navigation(i => i.VersatileDamageValue).AutoInclude();
                builder.Entity<EquipData>().Navigation(i => i.R_Slots).AutoInclude();
        }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new Data.DeletionInterceptor());
        base.OnConfiguring(optionsBuilder);
    }
}