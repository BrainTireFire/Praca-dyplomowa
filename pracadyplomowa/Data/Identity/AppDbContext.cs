﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using pracadyplomowa.Models;
using pracadyplomowa.Models.Entities;
using pracadyplomowa.Models.Entities.Campaign;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Items;
using pracadyplomowa.Models.Entities.Powers;

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

        // Powers
        public DbSet<Aura> Auras { get; set; }
        public DbSet<EffectBlueprint> EffectBlueprints { get; set; }
        public DbSet<EffectGroup> EffectGroups { get; set; }
        public DbSet<EffectInstance> EffectInstances { get; set; }
        public DbSet<ImmaterialResourceAmount> ImmaterialResourceAmounts { get; set; }
        public DbSet<ImmaterialResourceBlueprint> ImmaterialResourceBlueprints { get; set; }
        public DbSet<ImmaterialResourceInstance> ImmaterialResourceInstances { get; set; }
        public DbSet<Power> Powers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
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
                        .IsRequired();

                builder.Entity<Character>()
                        .HasOne(c => c.R_ConcentratesOn)
                        .WithOne(c => c.R_ConcentratedOnByCharacter)
                        .HasForeignKey<Character>(c => c.R_ConcentratesOnId)
                        .IsRequired(false);

                builder.Entity<Character>()
                        .HasMany(c => c.R_AffectedBy)
                        .WithMany(c => c.R_TargetedCharacters);

                builder.Entity<Character>()
                        .HasMany(c => c.R_PowersKnown)
                        .WithMany(c => c.R_CharacterKnownsPowers);

                builder.Entity<Character>()
                        .HasMany(c => c.R_PowersPrepared)
                        .WithMany(c => c.R_CharacterPreparedPowers);

                builder.Entity<Character>()
                        .HasOne(c => c.R_SpawnedByPower)
                        .WithMany(c => c.R_SpawnedCharacters)
                        .HasForeignKey(c => c.R_SpawnedByPowerId)
                        .IsRequired(false);

                builder.Entity<Weapon>()
                        .HasMany(c => c.R_PowersCastedOnHit)
                        .WithMany(c => c.R_WeaponsCastingOnHit);

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
                        .HasOne(c => c.R_OriginatesFromAura)
                        .WithMany(c => c.R_OwnedEffectGroups)
                        .HasForeignKey(c => c.R_OriginatesFromAuraId)
                        .IsRequired(false);

                builder.Entity<EffectGroup>()
                        .HasOne(c => c.R_ItemAffectedBy)
                        .WithMany(c => c.R_EffectGroupAffectedBy)
                        .HasForeignKey(c => c.R_ItemAffectedById)
                        .IsRequired(false);

                builder.Entity<EffectGroup>()
                        .HasOne(c => c.R_ItemGiveEffect)
                        .WithMany(c => c.R_EffectGroupFromItem)
                        .HasForeignKey(c => c.R_ItemGiveEffectId)
                        .IsRequired(false);


                builder.Entity<Item>().UseTptMappingStrategy();
        }
}