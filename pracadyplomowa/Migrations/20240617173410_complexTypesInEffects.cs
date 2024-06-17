using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Migrations
{
    /// <inheritdoc />
    public partial class complexTypesInEffects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Backpacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Backpacks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    MaximumPreparedSpellsFormula = table.Column<string>(type: "TEXT", nullable: true),
                    SpellcastingAbility = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentSlots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentSlots", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImmaterialResourceBlueprints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    RefreshesOn = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImmaterialResourceBlueprints", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemFamilies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemFamilies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Races",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Races", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Objects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    R_OwnerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Objects_AspNetUsers_R_OwnerId",
                        column: x => x.R_OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImmaterialResourceAmounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Count = table.Column<int>(type: "INTEGER", nullable: false),
                    Level = table.Column<int>(type: "INTEGER", nullable: false),
                    R_BlueprintId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImmaterialResourceAmounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImmaterialResourceAmounts_ImmaterialResourceBlueprints_R_BlueprintId",
                        column: x => x.R_BlueprintId,
                        principalTable: "ImmaterialResourceBlueprints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentSlotRace",
                columns: table => new
                {
                    R_EquipmentSlotsId = table.Column<int>(type: "INTEGER", nullable: false),
                    R_RacesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentSlotRace", x => new { x.R_EquipmentSlotsId, x.R_RacesId });
                    table.ForeignKey(
                        name: "FK_EquipmentSlotRace_EquipmentSlots_R_EquipmentSlotsId",
                        column: x => x.R_EquipmentSlotsId,
                        principalTable: "EquipmentSlots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentSlotRace_Races_R_RacesId",
                        column: x => x.R_RacesId,
                        principalTable: "Races",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RaceLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Level = table.Column<int>(type: "INTEGER", nullable: false),
                    R_RaceId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceLevels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RaceLevels_Races_R_RaceId",
                        column: x => x.R_RaceId,
                        principalTable: "Races",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Boards_Objects_Id",
                        column: x => x.Id,
                        principalTable: "Objects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Campaigns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaigns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Campaigns_Objects_Id",
                        column: x => x.Id,
                        principalTable: "Objects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Weight = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    IsSpellFocus = table.Column<bool>(type: "INTEGER", nullable: false),
                    R_ItemInItemsFamilyId = table.Column<int>(type: "INTEGER", nullable: false),
                    R_BackpackHasItemId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Backpacks_R_BackpackHasItemId",
                        column: x => x.R_BackpackHasItemId,
                        principalTable: "Backpacks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_ItemFamilies_R_ItemInItemsFamilyId",
                        column: x => x.R_ItemInItemsFamilyId,
                        principalTable: "ItemFamilies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Items_Objects_Id",
                        column: x => x.Id,
                        principalTable: "Objects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Powers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    RequiredActionType = table.Column<int>(type: "INTEGER", nullable: false),
                    IsImplemented = table.Column<bool>(type: "INTEGER", nullable: false),
                    CastableBy = table.Column<int>(type: "INTEGER", nullable: false),
                    PowerType = table.Column<int>(type: "INTEGER", nullable: false),
                    TargetType = table.Column<int>(type: "INTEGER", nullable: false),
                    Range = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxTargets = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxTargetsToExclude = table.Column<int>(type: "INTEGER", nullable: false),
                    AreaSize = table.Column<int>(type: "INTEGER", nullable: false),
                    AreaShape = table.Column<int>(type: "INTEGER", nullable: false),
                    AuraSize = table.Column<int>(type: "INTEGER", nullable: false),
                    DifficultyClass = table.Column<int>(type: "INTEGER", nullable: false),
                    SavingThrow = table.Column<int>(type: "INTEGER", nullable: false),
                    RequiresConcentration = table.Column<bool>(type: "INTEGER", nullable: false),
                    SavingThrowBehaviour = table.Column<int>(type: "INTEGER", nullable: false),
                    SavingThrowRoll = table.Column<int>(type: "INTEGER", nullable: false),
                    VerbalComponent = table.Column<bool>(type: "INTEGER", nullable: false),
                    SomaticComponent = table.Column<bool>(type: "INTEGER", nullable: false),
                    R_UsesImmaterialResourceId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Powers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Powers_ImmaterialResourceBlueprints_R_UsesImmaterialResourceId",
                        column: x => x.R_UsesImmaterialResourceId,
                        principalTable: "ImmaterialResourceBlueprints",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Powers_Objects_Id",
                        column: x => x.Id,
                        principalTable: "Objects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImmaterialResourceAmountRaceLevel",
                columns: table => new
                {
                    R_ImmaterialResourceAmountsId = table.Column<int>(type: "INTEGER", nullable: false),
                    R_RaceLevelsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImmaterialResourceAmountRaceLevel", x => new { x.R_ImmaterialResourceAmountsId, x.R_RaceLevelsId });
                    table.ForeignKey(
                        name: "FK_ImmaterialResourceAmountRaceLevel_ImmaterialResourceAmounts_R_ImmaterialResourceAmountsId",
                        column: x => x.R_ImmaterialResourceAmountsId,
                        principalTable: "ImmaterialResourceAmounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImmaterialResourceAmountRaceLevel_RaceLevels_R_RaceLevelsId",
                        column: x => x.R_RaceLevelsId,
                        principalTable: "RaceLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActionLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    R_EncounterId = table.Column<int>(type: "INTEGER", nullable: false),
                    CampaignId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActionLogs_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CampaignUser",
                columns: table => new
                {
                    R_UserAttendsAsPlayerToCamgainsId = table.Column<int>(type: "INTEGER", nullable: false),
                    R_UsersAttendsCampaignsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignUser", x => new { x.R_UserAttendsAsPlayerToCamgainsId, x.R_UsersAttendsCampaignsId });
                    table.ForeignKey(
                        name: "FK_CampaignUser_AspNetUsers_R_UsersAttendsCampaignsId",
                        column: x => x.R_UsersAttendsCampaignsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CampaignUser_Campaigns_R_UserAttendsAsPlayerToCamgainsId",
                        column: x => x.R_UserAttendsAsPlayerToCamgainsId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Encounters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    R_EncounterInTheCampaignId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    R_CampaignId = table.Column<int>(type: "INTEGER", nullable: true),
                    R_BoardId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Encounters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Encounters_Boards_R_BoardId",
                        column: x => x.R_BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Encounters_Campaigns_R_CampaignId",
                        column: x => x.R_CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Encounters_Objects_Id",
                        column: x => x.Id,
                        principalTable: "Objects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Location = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    R_CampaignId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shops_Campaigns_R_CampaignId",
                        column: x => x.R_CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Apparels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ArmorClass = table.Column<int>(type: "INTEGER", nullable: false),
                    StealthDisadvantage = table.Column<bool>(type: "INTEGER", nullable: false),
                    StrengthRequirement = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apparels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Apparels_Items_Id",
                        column: x => x.Id,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentSlotItem",
                columns: table => new
                {
                    R_ItemIsEquippableInSlotsId = table.Column<int>(type: "INTEGER", nullable: false),
                    R_ItemsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentSlotItem", x => new { x.R_ItemIsEquippableInSlotsId, x.R_ItemsId });
                    table.ForeignKey(
                        name: "FK_EquipmentSlotItem_EquipmentSlots_R_ItemIsEquippableInSlotsId",
                        column: x => x.R_ItemIsEquippableInSlotsId,
                        principalTable: "EquipmentSlots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentSlotItem_Items_R_ItemsId",
                        column: x => x.R_ItemsId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tools",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tools", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tools_Items_Id",
                        column: x => x.Id,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Weapons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Finesse = table.Column<bool>(type: "INTEGER", nullable: false),
                    Heavy = table.Column<bool>(type: "INTEGER", nullable: false),
                    Light = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weapons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Weapons_Items_Id",
                        column: x => x.Id,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassPower",
                columns: table => new
                {
                    R_AccessiblePowersId = table.Column<int>(type: "INTEGER", nullable: false),
                    R_ClassesWithAccessId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassPower", x => new { x.R_AccessiblePowersId, x.R_ClassesWithAccessId });
                    table.ForeignKey(
                        name: "FK_ClassPower_Classes_R_ClassesWithAccessId",
                        column: x => x.R_ClassesWithAccessId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassPower_Powers_R_AccessiblePowersId",
                        column: x => x.R_AccessiblePowersId,
                        principalTable: "Powers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemCostRequirements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Value = table.Column<int>(type: "INTEGER", nullable: false),
                    PowerId = table.Column<int>(type: "INTEGER", nullable: false),
                    R_ItemFamilyId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCostRequirements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemCostRequirements_ItemFamilies_R_ItemFamilyId",
                        column: x => x.R_ItemFamilyId,
                        principalTable: "ItemFamilies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemCostRequirements_Powers_PowerId",
                        column: x => x.PowerId,
                        principalTable: "Powers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemPower",
                columns: table => new
                {
                    R_EquipItemGrantsAccessToPowerId = table.Column<int>(type: "INTEGER", nullable: false),
                    R_ItemsGrantingPowerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPower", x => new { x.R_EquipItemGrantsAccessToPowerId, x.R_ItemsGrantingPowerId });
                    table.ForeignKey(
                        name: "FK_ItemPower_Items_R_ItemsGrantingPowerId",
                        column: x => x.R_ItemsGrantingPowerId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemPower_Powers_R_EquipItemGrantsAccessToPowerId",
                        column: x => x.R_EquipItemGrantsAccessToPowerId,
                        principalTable: "Powers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShopItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    R_ShopHasItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    R_ItemInShopId = table.Column<int>(type: "INTEGER", nullable: false),
                    Price_CopperPieces = table.Column<int>(type: "INTEGER", nullable: false),
                    Price_GoldPieces = table.Column<int>(type: "INTEGER", nullable: false),
                    Price_SilverPieces = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShopItems_Items_R_ShopHasItemId",
                        column: x => x.R_ShopHasItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShopItems_Shops_R_ItemInShopId",
                        column: x => x.R_ItemInShopId,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PowerWeapon",
                columns: table => new
                {
                    R_PowersCastedOnHitId = table.Column<int>(type: "INTEGER", nullable: false),
                    R_WeaponsCastingOnHitId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowerWeapon", x => new { x.R_PowersCastedOnHitId, x.R_WeaponsCastingOnHitId });
                    table.ForeignKey(
                        name: "FK_PowerWeapon_Powers_R_PowersCastedOnHitId",
                        column: x => x.R_PowersCastedOnHitId,
                        principalTable: "Powers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PowerWeapon_Weapons_R_WeaponsCastingOnHitId",
                        column: x => x.R_WeaponsCastingOnHitId,
                        principalTable: "Weapons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Auras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Size = table.Column<int>(type: "INTEGER", nullable: false),
                    R_CenteredAtCharacterId = table.Column<int>(type: "INTEGER", nullable: false),
                    GeneratedBy_Id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EffectBlueprints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    ResourceLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    Saved = table.Column<bool>(type: "INTEGER", nullable: false),
                    EffectType = table.Column<int>(type: "INTEGER", nullable: false),
                    R_CreatedByEquippingId = table.Column<int>(type: "INTEGER", nullable: false),
                    R_PowerId = table.Column<int>(type: "INTEGER", nullable: true),
                    R_CastedOnCharactersByAuraId = table.Column<int>(type: "INTEGER", nullable: true),
                    R_CastedOnTilesByAuraId = table.Column<int>(type: "INTEGER", nullable: true),
                    R_GrantsProficiencyInItemFamilyId = table.Column<int>(type: "INTEGER", nullable: true),
                    GrantsProficiencyInItemFamilyId = table.Column<int>(type: "INTEGER", nullable: true),
                    AbilityEffectType_AbilityEffect = table.Column<int>(type: "INTEGER", nullable: false),
                    AbilityEffectType_AbilityEffect_Ability = table.Column<int>(type: "INTEGER", nullable: false),
                    AbilityEffectType_AbilityEffect_Value_d10 = table.Column<int>(type: "INTEGER", nullable: false),
                    AbilityEffectType_AbilityEffect_Value_d100 = table.Column<int>(type: "INTEGER", nullable: false),
                    AbilityEffectType_AbilityEffect_Value_d12 = table.Column<int>(type: "INTEGER", nullable: false),
                    AbilityEffectType_AbilityEffect_Value_d20 = table.Column<int>(type: "INTEGER", nullable: false),
                    AbilityEffectType_AbilityEffect_Value_d4 = table.Column<int>(type: "INTEGER", nullable: false),
                    AbilityEffectType_AbilityEffect_Value_d6 = table.Column<int>(type: "INTEGER", nullable: false),
                    AbilityEffectType_AbilityEffect_Value_d8 = table.Column<int>(type: "INTEGER", nullable: false),
                    AbilityEffectType_AbilityEffect_Value_flat = table.Column<int>(type: "INTEGER", nullable: false),
                    ActionEffectType_ActionEffect = table.Column<int>(type: "INTEGER", nullable: false),
                    ActionEffectType_ActionEffect_Value_d10 = table.Column<int>(type: "INTEGER", nullable: false),
                    ActionEffectType_ActionEffect_Value_d100 = table.Column<int>(type: "INTEGER", nullable: false),
                    ActionEffectType_ActionEffect_Value_d12 = table.Column<int>(type: "INTEGER", nullable: false),
                    ActionEffectType_ActionEffect_Value_d20 = table.Column<int>(type: "INTEGER", nullable: false),
                    ActionEffectType_ActionEffect_Value_d4 = table.Column<int>(type: "INTEGER", nullable: false),
                    ActionEffectType_ActionEffect_Value_d6 = table.Column<int>(type: "INTEGER", nullable: false),
                    ActionEffectType_ActionEffect_Value_d8 = table.Column<int>(type: "INTEGER", nullable: false),
                    ActionEffectType_ActionEffect_Value_flat = table.Column<int>(type: "INTEGER", nullable: false),
                    ArmorClassEffectType_ArmorClassEffect_Value_d10 = table.Column<int>(type: "INTEGER", nullable: false),
                    ArmorClassEffectType_ArmorClassEffect_Value_d100 = table.Column<int>(type: "INTEGER", nullable: false),
                    ArmorClassEffectType_ArmorClassEffect_Value_d12 = table.Column<int>(type: "INTEGER", nullable: false),
                    ArmorClassEffectType_ArmorClassEffect_Value_d20 = table.Column<int>(type: "INTEGER", nullable: false),
                    ArmorClassEffectType_ArmorClassEffect_Value_d4 = table.Column<int>(type: "INTEGER", nullable: false),
                    ArmorClassEffectType_ArmorClassEffect_Value_d6 = table.Column<int>(type: "INTEGER", nullable: false),
                    ArmorClassEffectType_ArmorClassEffect_Value_d8 = table.Column<int>(type: "INTEGER", nullable: false),
                    ArmorClassEffectType_ArmorClassEffect_Value_flat = table.Column<int>(type: "INTEGER", nullable: false),
                    AttackPerAttackActionEffectType_AttackPerActionEffect = table.Column<int>(type: "INTEGER", nullable: false),
                    AttackPerAttackActionEffectType_AttackPerActionEffect_Value_d10 = table.Column<int>(type: "INTEGER", nullable: false),
                    AttackPerAttackActionEffectType_AttackPerActionEffect_Value_d100 = table.Column<int>(type: "INTEGER", nullable: false),
                    AttackPerAttackActionEffectType_AttackPerActionEffect_Value_d12 = table.Column<int>(type: "INTEGER", nullable: false),
                    AttackPerAttackActionEffectType_AttackPerActionEffect_Value_d20 = table.Column<int>(type: "INTEGER", nullable: false),
                    AttackPerAttackActionEffectType_AttackPerActionEffect_Value_d4 = table.Column<int>(type: "INTEGER", nullable: false),
                    AttackPerAttackActionEffectType_AttackPerActionEffect_Value_d6 = table.Column<int>(type: "INTEGER", nullable: false),
                    AttackPerAttackActionEffectType_AttackPerActionEffect_Value_d8 = table.Column<int>(type: "INTEGER", nullable: false),
                    AttackPerAttackActionEffectType_AttackPerActionEffect_Value_flat = table.Column<int>(type: "INTEGER", nullable: false),
                    AttackRollEffectType_AttackRollEffect_Range = table.Column<int>(type: "INTEGER", nullable: false),
                    AttackRollEffectType_AttackRollEffect_Source = table.Column<int>(type: "INTEGER", nullable: false),
                    AttackRollEffectType_AttackRollEffect_Type = table.Column<int>(type: "INTEGER", nullable: false),
                    AttackRollEffectType_AttackRollEffect_Value_d10 = table.Column<int>(type: "INTEGER", nullable: false),
                    AttackRollEffectType_AttackRollEffect_Value_d100 = table.Column<int>(type: "INTEGER", nullable: false),
                    AttackRollEffectType_AttackRollEffect_Value_d12 = table.Column<int>(type: "INTEGER", nullable: false),
                    AttackRollEffectType_AttackRollEffect_Value_d20 = table.Column<int>(type: "INTEGER", nullable: false),
                    AttackRollEffectType_AttackRollEffect_Value_d4 = table.Column<int>(type: "INTEGER", nullable: false),
                    AttackRollEffectType_AttackRollEffect_Value_d6 = table.Column<int>(type: "INTEGER", nullable: false),
                    AttackRollEffectType_AttackRollEffect_Value_d8 = table.Column<int>(type: "INTEGER", nullable: false),
                    AttackRollEffectType_AttackRollEffect_Value_flat = table.Column<int>(type: "INTEGER", nullable: false),
                    DamageEffectType_DamageEffect = table.Column<int>(type: "INTEGER", nullable: false),
                    DamageEffectType_DamageEffect_DamageType = table.Column<int>(type: "INTEGER", nullable: false),
                    DamageEffectType_DamageEffect_Value_d10 = table.Column<int>(type: "INTEGER", nullable: false),
                    DamageEffectType_DamageEffect_Value_d100 = table.Column<int>(type: "INTEGER", nullable: false),
                    DamageEffectType_DamageEffect_Value_d12 = table.Column<int>(type: "INTEGER", nullable: false),
                    DamageEffectType_DamageEffect_Value_d20 = table.Column<int>(type: "INTEGER", nullable: false),
                    DamageEffectType_DamageEffect_Value_d4 = table.Column<int>(type: "INTEGER", nullable: false),
                    DamageEffectType_DamageEffect_Value_d6 = table.Column<int>(type: "INTEGER", nullable: false),
                    DamageEffectType_DamageEffect_Value_d8 = table.Column<int>(type: "INTEGER", nullable: false),
                    DamageEffectType_DamageEffect_Value_flat = table.Column<int>(type: "INTEGER", nullable: false),
                    HealingEffectType_HealingEffect_Value_d10 = table.Column<int>(type: "INTEGER", nullable: false),
                    HealingEffectType_HealingEffect_Value_d100 = table.Column<int>(type: "INTEGER", nullable: false),
                    HealingEffectType_HealingEffect_Value_d12 = table.Column<int>(type: "INTEGER", nullable: false),
                    HealingEffectType_HealingEffect_Value_d20 = table.Column<int>(type: "INTEGER", nullable: false),
                    HealingEffectType_HealingEffect_Value_d4 = table.Column<int>(type: "INTEGER", nullable: false),
                    HealingEffectType_HealingEffect_Value_d6 = table.Column<int>(type: "INTEGER", nullable: false),
                    HealingEffectType_HealingEffect_Value_d8 = table.Column<int>(type: "INTEGER", nullable: false),
                    HealingEffectType_HealingEffect_Value_flat = table.Column<int>(type: "INTEGER", nullable: false),
                    HitpointEffectType_HitpointEffect = table.Column<int>(type: "INTEGER", nullable: false),
                    HitpointEffectType_HitpointEffect_Value_d10 = table.Column<int>(type: "INTEGER", nullable: false),
                    HitpointEffectType_HitpointEffect_Value_d100 = table.Column<int>(type: "INTEGER", nullable: false),
                    HitpointEffectType_HitpointEffect_Value_d12 = table.Column<int>(type: "INTEGER", nullable: false),
                    HitpointEffectType_HitpointEffect_Value_d20 = table.Column<int>(type: "INTEGER", nullable: false),
                    HitpointEffectType_HitpointEffect_Value_d4 = table.Column<int>(type: "INTEGER", nullable: false),
                    HitpointEffectType_HitpointEffect_Value_d6 = table.Column<int>(type: "INTEGER", nullable: false),
                    HitpointEffectType_HitpointEffect_Value_d8 = table.Column<int>(type: "INTEGER", nullable: false),
                    HitpointEffectType_HitpointEffect_Value_flat = table.Column<int>(type: "INTEGER", nullable: false),
                    InitiativeEffectType_InitiativeEffect_Value_d10 = table.Column<int>(type: "INTEGER", nullable: false),
                    InitiativeEffectType_InitiativeEffect_Value_d100 = table.Column<int>(type: "INTEGER", nullable: false),
                    InitiativeEffectType_InitiativeEffect_Value_d12 = table.Column<int>(type: "INTEGER", nullable: false),
                    InitiativeEffectType_InitiativeEffect_Value_d20 = table.Column<int>(type: "INTEGER", nullable: false),
                    InitiativeEffectType_InitiativeEffect_Value_d4 = table.Column<int>(type: "INTEGER", nullable: false),
                    InitiativeEffectType_InitiativeEffect_Value_d6 = table.Column<int>(type: "INTEGER", nullable: false),
                    InitiativeEffectType_InitiativeEffect_Value_d8 = table.Column<int>(type: "INTEGER", nullable: false),
                    InitiativeEffectType_InitiativeEffect_Value_flat = table.Column<int>(type: "INTEGER", nullable: false),
                    MagicItemEffectType_MagicItemEffect_Value_d10 = table.Column<int>(type: "INTEGER", nullable: false),
                    MagicItemEffectType_MagicItemEffect_Value_d100 = table.Column<int>(type: "INTEGER", nullable: false),
                    MagicItemEffectType_MagicItemEffect_Value_d12 = table.Column<int>(type: "INTEGER", nullable: false),
                    MagicItemEffectType_MagicItemEffect_Value_d20 = table.Column<int>(type: "INTEGER", nullable: false),
                    MagicItemEffectType_MagicItemEffect_Value_d4 = table.Column<int>(type: "INTEGER", nullable: false),
                    MagicItemEffectType_MagicItemEffect_Value_d6 = table.Column<int>(type: "INTEGER", nullable: false),
                    MagicItemEffectType_MagicItemEffect_Value_d8 = table.Column<int>(type: "INTEGER", nullable: false),
                    MagicItemEffectType_MagicItemEffect_Value_flat = table.Column<int>(type: "INTEGER", nullable: false),
                    MovementCostEffectType_MovementCost_Multiplier = table.Column<int>(type: "INTEGER", nullable: false),
                    MovementEffectType_MovementEffect = table.Column<int>(type: "INTEGER", nullable: false),
                    MovementEffectType_MovementEffect_Value_d10 = table.Column<int>(type: "INTEGER", nullable: false),
                    MovementEffectType_MovementEffect_Value_d100 = table.Column<int>(type: "INTEGER", nullable: false),
                    MovementEffectType_MovementEffect_Value_d12 = table.Column<int>(type: "INTEGER", nullable: false),
                    MovementEffectType_MovementEffect_Value_d20 = table.Column<int>(type: "INTEGER", nullable: false),
                    MovementEffectType_MovementEffect_Value_d4 = table.Column<int>(type: "INTEGER", nullable: false),
                    MovementEffectType_MovementEffect_Value_d6 = table.Column<int>(type: "INTEGER", nullable: false),
                    MovementEffectType_MovementEffect_Value_d8 = table.Column<int>(type: "INTEGER", nullable: false),
                    MovementEffectType_MovementEffect_Value_flat = table.Column<int>(type: "INTEGER", nullable: false),
                    ProficiencyEffectType_ProficiencyEffect = table.Column<int>(type: "INTEGER", nullable: false),
                    ResistanceEffectType_ResistanceEffect = table.Column<int>(type: "INTEGER", nullable: false),
                    ResistanceEffectType_ResistanceEffect_DamageType = table.Column<int>(type: "INTEGER", nullable: false),
                    SavingThrowEffectType_SavingThrowEffect = table.Column<int>(type: "INTEGER", nullable: false),
                    SavingThrowEffectType_SavingThrowEffect_Ability = table.Column<int>(type: "INTEGER", nullable: false),
                    SavingThrowEffectType_SavingThrowEffect_Value_d10 = table.Column<int>(type: "INTEGER", nullable: false),
                    SavingThrowEffectType_SavingThrowEffect_Value_d100 = table.Column<int>(type: "INTEGER", nullable: false),
                    SavingThrowEffectType_SavingThrowEffect_Value_d12 = table.Column<int>(type: "INTEGER", nullable: false),
                    SavingThrowEffectType_SavingThrowEffect_Value_d20 = table.Column<int>(type: "INTEGER", nullable: false),
                    SavingThrowEffectType_SavingThrowEffect_Value_d4 = table.Column<int>(type: "INTEGER", nullable: false),
                    SavingThrowEffectType_SavingThrowEffect_Value_d6 = table.Column<int>(type: "INTEGER", nullable: false),
                    SavingThrowEffectType_SavingThrowEffect_Value_d8 = table.Column<int>(type: "INTEGER", nullable: false),
                    SavingThrowEffectType_SavingThrowEffect_Value_flat = table.Column<int>(type: "INTEGER", nullable: false),
                    SizeEffectType_SizeEffect = table.Column<int>(type: "INTEGER", nullable: false),
                    SizeEffectType_SizeEffect_SizeToSet = table.Column<int>(type: "INTEGER", nullable: false),
                    SizeEffectType_SizeEffect_Value_d10 = table.Column<int>(type: "INTEGER", nullable: false),
                    SizeEffectType_SizeEffect_Value_d100 = table.Column<int>(type: "INTEGER", nullable: false),
                    SizeEffectType_SizeEffect_Value_d12 = table.Column<int>(type: "INTEGER", nullable: false),
                    SizeEffectType_SizeEffect_Value_d20 = table.Column<int>(type: "INTEGER", nullable: false),
                    SizeEffectType_SizeEffect_Value_d4 = table.Column<int>(type: "INTEGER", nullable: false),
                    SizeEffectType_SizeEffect_Value_d6 = table.Column<int>(type: "INTEGER", nullable: false),
                    SizeEffectType_SizeEffect_Value_d8 = table.Column<int>(type: "INTEGER", nullable: false),
                    SizeEffectType_SizeEffect_Value_flat = table.Column<int>(type: "INTEGER", nullable: false),
                    SkillEffectType_SkillEffect = table.Column<int>(type: "INTEGER", nullable: false),
                    SkillEffectType_SkillEffect_Skill = table.Column<int>(type: "INTEGER", nullable: false),
                    SkillEffectType_SkillEffect_Value_d10 = table.Column<int>(type: "INTEGER", nullable: false),
                    SkillEffectType_SkillEffect_Value_d100 = table.Column<int>(type: "INTEGER", nullable: false),
                    SkillEffectType_SkillEffect_Value_d12 = table.Column<int>(type: "INTEGER", nullable: false),
                    SkillEffectType_SkillEffect_Value_d20 = table.Column<int>(type: "INTEGER", nullable: false),
                    SkillEffectType_SkillEffect_Value_d4 = table.Column<int>(type: "INTEGER", nullable: false),
                    SkillEffectType_SkillEffect_Value_d6 = table.Column<int>(type: "INTEGER", nullable: false),
                    SkillEffectType_SkillEffect_Value_d8 = table.Column<int>(type: "INTEGER", nullable: false),
                    SkillEffectType_SkillEffect_Value_flat = table.Column<int>(type: "INTEGER", nullable: false),
                    StatusEffectType_StatusEffect = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EffectBlueprints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EffectBlueprints_Auras_R_CastedOnCharactersByAuraId",
                        column: x => x.R_CastedOnCharactersByAuraId,
                        principalTable: "Auras",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EffectBlueprints_Auras_R_CastedOnTilesByAuraId",
                        column: x => x.R_CastedOnTilesByAuraId,
                        principalTable: "Auras",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EffectBlueprints_ItemFamilies_R_GrantsProficiencyInItemFamilyId",
                        column: x => x.R_GrantsProficiencyInItemFamilyId,
                        principalTable: "ItemFamilies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EffectBlueprints_Items_R_CreatedByEquippingId",
                        column: x => x.R_CreatedByEquippingId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EffectBlueprints_Powers_R_PowerId",
                        column: x => x.R_PowerId,
                        principalTable: "Powers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EffectGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsConstant = table.Column<bool>(type: "INTEGER", nullable: false),
                    DurationLeft = table.Column<int>(type: "INTEGER", nullable: false),
                    DifficultyClassToBreak = table.Column<int>(type: "INTEGER", nullable: false),
                    SavingThrow = table.Column<int>(type: "INTEGER", nullable: false),
                    SavingThrowRetakenEveryTurn = table.Column<bool>(type: "INTEGER", nullable: false),
                    R_ConcentratedOnByCharacterId = table.Column<int>(type: "INTEGER", nullable: true),
                    R_ItemAffectedById = table.Column<int>(type: "INTEGER", nullable: true),
                    R_ItemGiveEffectId = table.Column<int>(type: "INTEGER", nullable: true),
                    R_OriginatesFromAuraId = table.Column<int>(type: "INTEGER", nullable: true),
                    R_GeneratesAuraId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EffectGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EffectGroups_Auras_R_GeneratesAuraId",
                        column: x => x.R_GeneratesAuraId,
                        principalTable: "Auras",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EffectGroups_Auras_R_OriginatesFromAuraId",
                        column: x => x.R_OriginatesFromAuraId,
                        principalTable: "Auras",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EffectGroups_Items_R_ItemAffectedById",
                        column: x => x.R_ItemAffectedById,
                        principalTable: "Items",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EffectGroups_Items_R_ItemGiveEffectId",
                        column: x => x.R_ItemGiveEffectId,
                        principalTable: "Items",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    R_CharacterBelongsToRaceId = table.Column<int>(type: "INTEGER", nullable: false),
                    R_ConcentratesOnId = table.Column<int>(type: "INTEGER", nullable: true),
                    R_CharacterHasBackpackId = table.Column<int>(type: "INTEGER", nullable: false),
                    R_CampaignId = table.Column<int>(type: "INTEGER", nullable: false),
                    R_SpawnedByPowerId = table.Column<int>(type: "INTEGER", nullable: true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: true),
                    UsedHitDice_d10 = table.Column<int>(type: "INTEGER", nullable: false),
                    UsedHitDice_d100 = table.Column<int>(type: "INTEGER", nullable: false),
                    UsedHitDice_d12 = table.Column<int>(type: "INTEGER", nullable: false),
                    UsedHitDice_d20 = table.Column<int>(type: "INTEGER", nullable: false),
                    UsedHitDice_d4 = table.Column<int>(type: "INTEGER", nullable: false),
                    UsedHitDice_d6 = table.Column<int>(type: "INTEGER", nullable: false),
                    UsedHitDice_d8 = table.Column<int>(type: "INTEGER", nullable: false),
                    UsedHitDice_flat = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Characters_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Characters_Backpacks_R_CharacterHasBackpackId",
                        column: x => x.R_CharacterHasBackpackId,
                        principalTable: "Backpacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Characters_Campaigns_R_CampaignId",
                        column: x => x.R_CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Characters_EffectGroups_R_ConcentratesOnId",
                        column: x => x.R_ConcentratesOnId,
                        principalTable: "EffectGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Characters_Objects_Id",
                        column: x => x.Id,
                        principalTable: "Objects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Characters_Powers_R_SpawnedByPowerId",
                        column: x => x.R_SpawnedByPowerId,
                        principalTable: "Powers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Characters_Races_R_CharacterBelongsToRaceId",
                        column: x => x.R_CharacterBelongsToRaceId,
                        principalTable: "Races",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EffectInstances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    SourceName = table.Column<string>(type: "TEXT", nullable: false),
                    EffectType = table.Column<int>(type: "INTEGER", nullable: false),
                    R_OwnedByGroupId = table.Column<int>(type: "INTEGER", nullable: false),
                    OwnedByGroupId = table.Column<int>(type: "INTEGER", nullable: false),
                    R_GrantsProficiencyInItemFamilyId = table.Column<int>(type: "INTEGER", nullable: true),
                    GrantsProficiencyInItemFamilyId = table.Column<int>(type: "INTEGER", nullable: true),
                    AbilityEffectType_AbilityEffect = table.Column<int>(type: "INTEGER", nullable: false),
                    AbilityEffectType_AbilityEffect_Ability = table.Column<int>(type: "INTEGER", nullable: false),
                    AbilityEffectType_AbilityEffect_Value_d10 = table.Column<int>(type: "INTEGER", nullable: false),
                    AbilityEffectType_AbilityEffect_Value_d100 = table.Column<int>(type: "INTEGER", nullable: false),
                    AbilityEffectType_AbilityEffect_Value_d12 = table.Column<int>(type: "INTEGER", nullable: false),
                    AbilityEffectType_AbilityEffect_Value_d20 = table.Column<int>(type: "INTEGER", nullable: false),
                    AbilityEffectType_AbilityEffect_Value_d4 = table.Column<int>(type: "INTEGER", nullable: false),
                    AbilityEffectType_AbilityEffect_Value_d6 = table.Column<int>(type: "INTEGER", nullable: false),
                    AbilityEffectType_AbilityEffect_Value_d8 = table.Column<int>(type: "INTEGER", nullable: false),
                    AbilityEffectType_AbilityEffect_Value_flat = table.Column<int>(type: "INTEGER", nullable: false),
                    ActionEffectType_ActionEffect = table.Column<int>(type: "INTEGER", nullable: false),
                    ActionEffectType_ActionEffect_Value_d10 = table.Column<int>(type: "INTEGER", nullable: false),
                    ActionEffectType_ActionEffect_Value_d100 = table.Column<int>(type: "INTEGER", nullable: false),
                    ActionEffectType_ActionEffect_Value_d12 = table.Column<int>(type: "INTEGER", nullable: false),
                    ActionEffectType_ActionEffect_Value_d20 = table.Column<int>(type: "INTEGER", nullable: false),
                    ActionEffectType_ActionEffect_Value_d4 = table.Column<int>(type: "INTEGER", nullable: false),
                    ActionEffectType_ActionEffect_Value_d6 = table.Column<int>(type: "INTEGER", nullable: false),
                    ActionEffectType_ActionEffect_Value_d8 = table.Column<int>(type: "INTEGER", nullable: false),
                    ActionEffectType_ActionEffect_Value_flat = table.Column<int>(type: "INTEGER", nullable: false),
                    ArmorClassEffectType_ArmorClassEffect_Value_d10 = table.Column<int>(type: "INTEGER", nullable: false),
                    ArmorClassEffectType_ArmorClassEffect_Value_d100 = table.Column<int>(type: "INTEGER", nullable: false),
                    ArmorClassEffectType_ArmorClassEffect_Value_d12 = table.Column<int>(type: "INTEGER", nullable: false),
                    ArmorClassEffectType_ArmorClassEffect_Value_d20 = table.Column<int>(type: "INTEGER", nullable: false),
                    ArmorClassEffectType_ArmorClassEffect_Value_d4 = table.Column<int>(type: "INTEGER", nullable: false),
                    ArmorClassEffectType_ArmorClassEffect_Value_d6 = table.Column<int>(type: "INTEGER", nullable: false),
                    ArmorClassEffectType_ArmorClassEffect_Value_d8 = table.Column<int>(type: "INTEGER", nullable: false),
                    ArmorClassEffectType_ArmorClassEffect_Value_flat = table.Column<int>(type: "INTEGER", nullable: false),
                    AttackPerAttackActionEffectType_AttackPerActionEffect = table.Column<int>(type: "INTEGER", nullable: false),
                    AttackPerAttackActionEffectType_AttackPerActionEffect_Value_d10 = table.Column<int>(type: "INTEGER", nullable: false),
                    AttackPerAttackActionEffectType_AttackPerActionEffect_Value_d100 = table.Column<int>(type: "INTEGER", nullable: false),
                    AttackPerAttackActionEffectType_AttackPerActionEffect_Value_d12 = table.Column<int>(type: "INTEGER", nullable: false),
                    AttackPerAttackActionEffectType_AttackPerActionEffect_Value_d20 = table.Column<int>(type: "INTEGER", nullable: false),
                    AttackPerAttackActionEffectType_AttackPerActionEffect_Value_d4 = table.Column<int>(type: "INTEGER", nullable: false),
                    AttackPerAttackActionEffectType_AttackPerActionEffect_Value_d6 = table.Column<int>(type: "INTEGER", nullable: false),
                    AttackPerAttackActionEffectType_AttackPerActionEffect_Value_d8 = table.Column<int>(type: "INTEGER", nullable: false),
                    AttackPerAttackActionEffectType_AttackPerActionEffect_Value_flat = table.Column<int>(type: "INTEGER", nullable: false),
                    AttackRollEffectType_AttackRollEffect_Range = table.Column<int>(type: "INTEGER", nullable: false),
                    AttackRollEffectType_AttackRollEffect_Source = table.Column<int>(type: "INTEGER", nullable: false),
                    AttackRollEffectType_AttackRollEffect_Type = table.Column<int>(type: "INTEGER", nullable: false),
                    AttackRollEffectType_AttackRollEffect_Value_d10 = table.Column<int>(type: "INTEGER", nullable: false),
                    AttackRollEffectType_AttackRollEffect_Value_d100 = table.Column<int>(type: "INTEGER", nullable: false),
                    AttackRollEffectType_AttackRollEffect_Value_d12 = table.Column<int>(type: "INTEGER", nullable: false),
                    AttackRollEffectType_AttackRollEffect_Value_d20 = table.Column<int>(type: "INTEGER", nullable: false),
                    AttackRollEffectType_AttackRollEffect_Value_d4 = table.Column<int>(type: "INTEGER", nullable: false),
                    AttackRollEffectType_AttackRollEffect_Value_d6 = table.Column<int>(type: "INTEGER", nullable: false),
                    AttackRollEffectType_AttackRollEffect_Value_d8 = table.Column<int>(type: "INTEGER", nullable: false),
                    AttackRollEffectType_AttackRollEffect_Value_flat = table.Column<int>(type: "INTEGER", nullable: false),
                    DamageEffectType_DamageEffect = table.Column<int>(type: "INTEGER", nullable: false),
                    DamageEffectType_DamageEffect_DamageType = table.Column<int>(type: "INTEGER", nullable: false),
                    DamageEffectType_DamageEffect_Value_d10 = table.Column<int>(type: "INTEGER", nullable: false),
                    DamageEffectType_DamageEffect_Value_d100 = table.Column<int>(type: "INTEGER", nullable: false),
                    DamageEffectType_DamageEffect_Value_d12 = table.Column<int>(type: "INTEGER", nullable: false),
                    DamageEffectType_DamageEffect_Value_d20 = table.Column<int>(type: "INTEGER", nullable: false),
                    DamageEffectType_DamageEffect_Value_d4 = table.Column<int>(type: "INTEGER", nullable: false),
                    DamageEffectType_DamageEffect_Value_d6 = table.Column<int>(type: "INTEGER", nullable: false),
                    DamageEffectType_DamageEffect_Value_d8 = table.Column<int>(type: "INTEGER", nullable: false),
                    DamageEffectType_DamageEffect_Value_flat = table.Column<int>(type: "INTEGER", nullable: false),
                    HealingEffectType_HealingEffect_Value_d10 = table.Column<int>(type: "INTEGER", nullable: false),
                    HealingEffectType_HealingEffect_Value_d100 = table.Column<int>(type: "INTEGER", nullable: false),
                    HealingEffectType_HealingEffect_Value_d12 = table.Column<int>(type: "INTEGER", nullable: false),
                    HealingEffectType_HealingEffect_Value_d20 = table.Column<int>(type: "INTEGER", nullable: false),
                    HealingEffectType_HealingEffect_Value_d4 = table.Column<int>(type: "INTEGER", nullable: false),
                    HealingEffectType_HealingEffect_Value_d6 = table.Column<int>(type: "INTEGER", nullable: false),
                    HealingEffectType_HealingEffect_Value_d8 = table.Column<int>(type: "INTEGER", nullable: false),
                    HealingEffectType_HealingEffect_Value_flat = table.Column<int>(type: "INTEGER", nullable: false),
                    HitpointEffectType_HitpointEffect = table.Column<int>(type: "INTEGER", nullable: false),
                    HitpointEffectType_HitpointEffect_Value_d10 = table.Column<int>(type: "INTEGER", nullable: false),
                    HitpointEffectType_HitpointEffect_Value_d100 = table.Column<int>(type: "INTEGER", nullable: false),
                    HitpointEffectType_HitpointEffect_Value_d12 = table.Column<int>(type: "INTEGER", nullable: false),
                    HitpointEffectType_HitpointEffect_Value_d20 = table.Column<int>(type: "INTEGER", nullable: false),
                    HitpointEffectType_HitpointEffect_Value_d4 = table.Column<int>(type: "INTEGER", nullable: false),
                    HitpointEffectType_HitpointEffect_Value_d6 = table.Column<int>(type: "INTEGER", nullable: false),
                    HitpointEffectType_HitpointEffect_Value_d8 = table.Column<int>(type: "INTEGER", nullable: false),
                    HitpointEffectType_HitpointEffect_Value_flat = table.Column<int>(type: "INTEGER", nullable: false),
                    InitiativeEffectType_InitiativeEffect_Value_d10 = table.Column<int>(type: "INTEGER", nullable: false),
                    InitiativeEffectType_InitiativeEffect_Value_d100 = table.Column<int>(type: "INTEGER", nullable: false),
                    InitiativeEffectType_InitiativeEffect_Value_d12 = table.Column<int>(type: "INTEGER", nullable: false),
                    InitiativeEffectType_InitiativeEffect_Value_d20 = table.Column<int>(type: "INTEGER", nullable: false),
                    InitiativeEffectType_InitiativeEffect_Value_d4 = table.Column<int>(type: "INTEGER", nullable: false),
                    InitiativeEffectType_InitiativeEffect_Value_d6 = table.Column<int>(type: "INTEGER", nullable: false),
                    InitiativeEffectType_InitiativeEffect_Value_d8 = table.Column<int>(type: "INTEGER", nullable: false),
                    InitiativeEffectType_InitiativeEffect_Value_flat = table.Column<int>(type: "INTEGER", nullable: false),
                    MagicItemEffectType_MagicItemEffect_Value_d10 = table.Column<int>(type: "INTEGER", nullable: false),
                    MagicItemEffectType_MagicItemEffect_Value_d100 = table.Column<int>(type: "INTEGER", nullable: false),
                    MagicItemEffectType_MagicItemEffect_Value_d12 = table.Column<int>(type: "INTEGER", nullable: false),
                    MagicItemEffectType_MagicItemEffect_Value_d20 = table.Column<int>(type: "INTEGER", nullable: false),
                    MagicItemEffectType_MagicItemEffect_Value_d4 = table.Column<int>(type: "INTEGER", nullable: false),
                    MagicItemEffectType_MagicItemEffect_Value_d6 = table.Column<int>(type: "INTEGER", nullable: false),
                    MagicItemEffectType_MagicItemEffect_Value_d8 = table.Column<int>(type: "INTEGER", nullable: false),
                    MagicItemEffectType_MagicItemEffect_Value_flat = table.Column<int>(type: "INTEGER", nullable: false),
                    MovementCostEffectType_MovementCost_Multiplier = table.Column<int>(type: "INTEGER", nullable: false),
                    MovementEffectType_MovementEffect = table.Column<int>(type: "INTEGER", nullable: false),
                    MovementEffectType_MovementEffect_Value_d10 = table.Column<int>(type: "INTEGER", nullable: false),
                    MovementEffectType_MovementEffect_Value_d100 = table.Column<int>(type: "INTEGER", nullable: false),
                    MovementEffectType_MovementEffect_Value_d12 = table.Column<int>(type: "INTEGER", nullable: false),
                    MovementEffectType_MovementEffect_Value_d20 = table.Column<int>(type: "INTEGER", nullable: false),
                    MovementEffectType_MovementEffect_Value_d4 = table.Column<int>(type: "INTEGER", nullable: false),
                    MovementEffectType_MovementEffect_Value_d6 = table.Column<int>(type: "INTEGER", nullable: false),
                    MovementEffectType_MovementEffect_Value_d8 = table.Column<int>(type: "INTEGER", nullable: false),
                    MovementEffectType_MovementEffect_Value_flat = table.Column<int>(type: "INTEGER", nullable: false),
                    ProficiencyEffectType_ProficiencyEffect = table.Column<int>(type: "INTEGER", nullable: false),
                    ResistanceEffectType_ResistanceEffect = table.Column<int>(type: "INTEGER", nullable: false),
                    ResistanceEffectType_ResistanceEffect_DamageType = table.Column<int>(type: "INTEGER", nullable: false),
                    SavingThrowEffectType_SavingThrowEffect = table.Column<int>(type: "INTEGER", nullable: false),
                    SavingThrowEffectType_SavingThrowEffect_Ability = table.Column<int>(type: "INTEGER", nullable: false),
                    SavingThrowEffectType_SavingThrowEffect_Value_d10 = table.Column<int>(type: "INTEGER", nullable: false),
                    SavingThrowEffectType_SavingThrowEffect_Value_d100 = table.Column<int>(type: "INTEGER", nullable: false),
                    SavingThrowEffectType_SavingThrowEffect_Value_d12 = table.Column<int>(type: "INTEGER", nullable: false),
                    SavingThrowEffectType_SavingThrowEffect_Value_d20 = table.Column<int>(type: "INTEGER", nullable: false),
                    SavingThrowEffectType_SavingThrowEffect_Value_d4 = table.Column<int>(type: "INTEGER", nullable: false),
                    SavingThrowEffectType_SavingThrowEffect_Value_d6 = table.Column<int>(type: "INTEGER", nullable: false),
                    SavingThrowEffectType_SavingThrowEffect_Value_d8 = table.Column<int>(type: "INTEGER", nullable: false),
                    SavingThrowEffectType_SavingThrowEffect_Value_flat = table.Column<int>(type: "INTEGER", nullable: false),
                    SizeEffectType_SizeEffect = table.Column<int>(type: "INTEGER", nullable: false),
                    SizeEffectType_SizeEffect_SizeToSet = table.Column<int>(type: "INTEGER", nullable: false),
                    SizeEffectType_SizeEffect_Value_d10 = table.Column<int>(type: "INTEGER", nullable: false),
                    SizeEffectType_SizeEffect_Value_d100 = table.Column<int>(type: "INTEGER", nullable: false),
                    SizeEffectType_SizeEffect_Value_d12 = table.Column<int>(type: "INTEGER", nullable: false),
                    SizeEffectType_SizeEffect_Value_d20 = table.Column<int>(type: "INTEGER", nullable: false),
                    SizeEffectType_SizeEffect_Value_d4 = table.Column<int>(type: "INTEGER", nullable: false),
                    SizeEffectType_SizeEffect_Value_d6 = table.Column<int>(type: "INTEGER", nullable: false),
                    SizeEffectType_SizeEffect_Value_d8 = table.Column<int>(type: "INTEGER", nullable: false),
                    SizeEffectType_SizeEffect_Value_flat = table.Column<int>(type: "INTEGER", nullable: false),
                    SkillEffectType_SkillEffect = table.Column<int>(type: "INTEGER", nullable: false),
                    SkillEffectType_SkillEffect_Skill = table.Column<int>(type: "INTEGER", nullable: false),
                    SkillEffectType_SkillEffect_Value_d10 = table.Column<int>(type: "INTEGER", nullable: false),
                    SkillEffectType_SkillEffect_Value_d100 = table.Column<int>(type: "INTEGER", nullable: false),
                    SkillEffectType_SkillEffect_Value_d12 = table.Column<int>(type: "INTEGER", nullable: false),
                    SkillEffectType_SkillEffect_Value_d20 = table.Column<int>(type: "INTEGER", nullable: false),
                    SkillEffectType_SkillEffect_Value_d4 = table.Column<int>(type: "INTEGER", nullable: false),
                    SkillEffectType_SkillEffect_Value_d6 = table.Column<int>(type: "INTEGER", nullable: false),
                    SkillEffectType_SkillEffect_Value_d8 = table.Column<int>(type: "INTEGER", nullable: false),
                    SkillEffectType_SkillEffect_Value_flat = table.Column<int>(type: "INTEGER", nullable: false),
                    StatusEffectType_StatusEffect = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EffectInstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EffectInstances_EffectGroups_R_OwnedByGroupId",
                        column: x => x.R_OwnedByGroupId,
                        principalTable: "EffectGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EffectInstances_ItemFamilies_R_GrantsProficiencyInItemFamilyId",
                        column: x => x.R_GrantsProficiencyInItemFamilyId,
                        principalTable: "ItemFamilies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CharacterEffectGroup",
                columns: table => new
                {
                    R_AffectedById = table.Column<int>(type: "INTEGER", nullable: false),
                    R_TargetedCharactersId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterEffectGroup", x => new { x.R_AffectedById, x.R_TargetedCharactersId });
                    table.ForeignKey(
                        name: "FK_CharacterEffectGroup_Characters_R_TargetedCharactersId",
                        column: x => x.R_TargetedCharactersId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterEffectGroup_EffectGroups_R_AffectedById",
                        column: x => x.R_AffectedById,
                        principalTable: "EffectGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterPower",
                columns: table => new
                {
                    R_CharacterKnownsPowersId = table.Column<int>(type: "INTEGER", nullable: false),
                    R_PowersKnownId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterPower", x => new { x.R_CharacterKnownsPowersId, x.R_PowersKnownId });
                    table.ForeignKey(
                        name: "FK_CharacterPower_Characters_R_CharacterKnownsPowersId",
                        column: x => x.R_CharacterKnownsPowersId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterPower_Powers_R_PowersKnownId",
                        column: x => x.R_PowersKnownId,
                        principalTable: "Powers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterPower1",
                columns: table => new
                {
                    R_CharacterPreparedPowersId = table.Column<int>(type: "INTEGER", nullable: false),
                    R_PowersPreparedId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterPower1", x => new { x.R_CharacterPreparedPowersId, x.R_PowersPreparedId });
                    table.ForeignKey(
                        name: "FK_CharacterPower1_Characters_R_CharacterPreparedPowersId",
                        column: x => x.R_CharacterPreparedPowersId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterPower1_Powers_R_PowersPreparedId",
                        column: x => x.R_PowersPreparedId,
                        principalTable: "Powers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EquipDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsEquipped = table.Column<bool>(type: "INTEGER", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    R_CharacterId = table.Column<int>(type: "INTEGER", nullable: false),
                    R_ItemId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipDatas_Characters_R_CharacterId",
                        column: x => x.R_CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipDatas_Items_R_ItemId",
                        column: x => x.R_ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImmaterialResourceInstances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NeedsRefresh = table.Column<bool>(type: "INTEGER", nullable: false),
                    Level = table.Column<int>(type: "INTEGER", nullable: false),
                    R_ResourceGrantedToItemId = table.Column<int>(type: "INTEGER", nullable: true),
                    R_BlueprintId = table.Column<int>(type: "INTEGER", nullable: false),
                    BlueprintId = table.Column<int>(type: "INTEGER", nullable: false),
                    R_CharacterId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImmaterialResourceInstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImmaterialResourceInstances_Characters_R_CharacterId",
                        column: x => x.R_CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ImmaterialResourceInstances_ImmaterialResourceBlueprints_R_BlueprintId",
                        column: x => x.R_BlueprintId,
                        principalTable: "ImmaterialResourceBlueprints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImmaterialResourceInstances_Items_R_ResourceGrantedToItemId",
                        column: x => x.R_ResourceGrantedToItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ParticipanceDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    R_EncounterId = table.Column<int>(type: "INTEGER", nullable: false),
                    R_CharacterOccupiesFieldId = table.Column<int>(type: "INTEGER", nullable: false),
                    InitiativeOrder = table.Column<int>(type: "INTEGER", nullable: false),
                    IsSurprised = table.Column<bool>(type: "INTEGER", nullable: false),
                    NumberOfActionsTaken = table.Column<int>(type: "INTEGER", nullable: false),
                    NumberOfBonusActionsTaken = table.Column<int>(type: "INTEGER", nullable: false),
                    NumberOfAttacksTaken = table.Column<int>(type: "INTEGER", nullable: false),
                    DistanceTraveled = table.Column<int>(type: "INTEGER", nullable: false),
                    R_CharacterId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipanceDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParticipanceDatas_Characters_R_CharacterId",
                        column: x => x.R_CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParticipanceDatas_Encounters_R_EncounterId",
                        column: x => x.R_EncounterId,
                        principalTable: "Encounters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Level = table.Column<int>(type: "INTEGER", nullable: false),
                    R_ClassId = table.Column<int>(type: "INTEGER", nullable: false),
                    ImmaterialResourceInstanceId = table.Column<int>(type: "INTEGER", nullable: true),
                    HitDie_d10 = table.Column<int>(type: "INTEGER", nullable: false),
                    HitDie_d100 = table.Column<int>(type: "INTEGER", nullable: false),
                    HitDie_d12 = table.Column<int>(type: "INTEGER", nullable: false),
                    HitDie_d20 = table.Column<int>(type: "INTEGER", nullable: false),
                    HitDie_d4 = table.Column<int>(type: "INTEGER", nullable: false),
                    HitDie_d6 = table.Column<int>(type: "INTEGER", nullable: false),
                    HitDie_d8 = table.Column<int>(type: "INTEGER", nullable: false),
                    HitDie_flat = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassLevels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassLevels_Classes_R_ClassId",
                        column: x => x.R_ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassLevels_ImmaterialResourceInstances_ImmaterialResourceInstanceId",
                        column: x => x.ImmaterialResourceInstanceId,
                        principalTable: "ImmaterialResourceInstances",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Fields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    R_BoardId = table.Column<int>(type: "INTEGER", nullable: false),
                    PositionX = table.Column<int>(type: "INTEGER", nullable: false),
                    PositionY = table.Column<int>(type: "INTEGER", nullable: false),
                    PositionZ = table.Column<int>(type: "INTEGER", nullable: false),
                    Color = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    R_OccupiedById = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fields_Boards_R_BoardId",
                        column: x => x.R_BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fields_ParticipanceDatas_R_OccupiedById",
                        column: x => x.R_OccupiedById,
                        principalTable: "ParticipanceDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterClassLevel",
                columns: table => new
                {
                    R_CharacterHasLevelsInClassId = table.Column<int>(type: "INTEGER", nullable: false),
                    R_CharactersId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterClassLevel", x => new { x.R_CharacterHasLevelsInClassId, x.R_CharactersId });
                    table.ForeignKey(
                        name: "FK_CharacterClassLevel_Characters_R_CharactersId",
                        column: x => x.R_CharactersId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterClassLevel_ClassLevels_R_CharacterHasLevelsInClassId",
                        column: x => x.R_CharacterHasLevelsInClassId,
                        principalTable: "ClassLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChoiceGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NumberToChoose = table.Column<int>(type: "INTEGER", nullable: false),
                    R_GrantedByRaceLevelId = table.Column<int>(type: "INTEGER", nullable: true),
                    GrantedByRaceLevelId = table.Column<int>(type: "INTEGER", nullable: true),
                    R_GrantedByClassLevelId = table.Column<int>(type: "INTEGER", nullable: true),
                    GrantedByClassLevelId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChoiceGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChoiceGroups_ClassLevels_R_GrantedByClassLevelId",
                        column: x => x.R_GrantedByClassLevelId,
                        principalTable: "ClassLevels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChoiceGroups_RaceLevels_R_GrantedByRaceLevelId",
                        column: x => x.R_GrantedByRaceLevelId,
                        principalTable: "RaceLevels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClassLevelImmaterialResourceAmount",
                columns: table => new
                {
                    R_ClassLevelsId = table.Column<int>(type: "INTEGER", nullable: false),
                    R_ImmaterialResourceAmountsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassLevelImmaterialResourceAmount", x => new { x.R_ClassLevelsId, x.R_ImmaterialResourceAmountsId });
                    table.ForeignKey(
                        name: "FK_ClassLevelImmaterialResourceAmount_ClassLevels_R_ClassLevelsId",
                        column: x => x.R_ClassLevelsId,
                        principalTable: "ClassLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassLevelImmaterialResourceAmount_ImmaterialResourceAmounts_R_ImmaterialResourceAmountsId",
                        column: x => x.R_ImmaterialResourceAmountsId,
                        principalTable: "ImmaterialResourceAmounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EffectGroupField",
                columns: table => new
                {
                    R_EffectOnFieldId = table.Column<int>(type: "INTEGER", nullable: false),
                    R_EffectOnFieldId1 = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EffectGroupField", x => new { x.R_EffectOnFieldId, x.R_EffectOnFieldId1 });
                    table.ForeignKey(
                        name: "FK_EffectGroupField_EffectGroups_R_EffectOnFieldId",
                        column: x => x.R_EffectOnFieldId,
                        principalTable: "EffectGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EffectGroupField_Fields_R_EffectOnFieldId1",
                        column: x => x.R_EffectOnFieldId1,
                        principalTable: "Fields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FieldPower",
                columns: table => new
                {
                    R_CasterPowersId = table.Column<int>(type: "INTEGER", nullable: false),
                    R_FieldsCastingId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldPower", x => new { x.R_CasterPowersId, x.R_FieldsCastingId });
                    table.ForeignKey(
                        name: "FK_FieldPower_Fields_R_FieldsCastingId",
                        column: x => x.R_FieldsCastingId,
                        principalTable: "Fields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FieldPower_Powers_R_CasterPowersId",
                        column: x => x.R_CasterPowersId,
                        principalTable: "Powers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChoiceGroupEffectBlueprint",
                columns: table => new
                {
                    R_ChoiceGroupsId = table.Column<int>(type: "INTEGER", nullable: false),
                    R_EffectsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChoiceGroupEffectBlueprint", x => new { x.R_ChoiceGroupsId, x.R_EffectsId });
                    table.ForeignKey(
                        name: "FK_ChoiceGroupEffectBlueprint_ChoiceGroups_R_ChoiceGroupsId",
                        column: x => x.R_ChoiceGroupsId,
                        principalTable: "ChoiceGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChoiceGroupEffectBlueprint_EffectBlueprints_R_EffectsId",
                        column: x => x.R_EffectsId,
                        principalTable: "EffectBlueprints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChoiceGroupPower",
                columns: table => new
                {
                    R_ChoiceGroupsId = table.Column<int>(type: "INTEGER", nullable: false),
                    R_PowersId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChoiceGroupPower", x => new { x.R_ChoiceGroupsId, x.R_PowersId });
                    table.ForeignKey(
                        name: "FK_ChoiceGroupPower_ChoiceGroups_R_ChoiceGroupsId",
                        column: x => x.R_ChoiceGroupsId,
                        principalTable: "ChoiceGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChoiceGroupPower_Powers_R_PowersId",
                        column: x => x.R_PowersId,
                        principalTable: "Powers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActionLogs_CampaignId",
                table: "ActionLogs",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Auras_R_CenteredAtCharacterId",
                table: "Auras",
                column: "R_CenteredAtCharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_CampaignUser_R_UsersAttendsCampaignsId",
                table: "CampaignUser",
                column: "R_UsersAttendsCampaignsId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterClassLevel_R_CharactersId",
                table: "CharacterClassLevel",
                column: "R_CharactersId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterEffectGroup_R_TargetedCharactersId",
                table: "CharacterEffectGroup",
                column: "R_TargetedCharactersId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterPower_R_PowersKnownId",
                table: "CharacterPower",
                column: "R_PowersKnownId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterPower1_R_PowersPreparedId",
                table: "CharacterPower1",
                column: "R_PowersPreparedId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_R_CampaignId",
                table: "Characters",
                column: "R_CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_R_CharacterBelongsToRaceId",
                table: "Characters",
                column: "R_CharacterBelongsToRaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_R_CharacterHasBackpackId",
                table: "Characters",
                column: "R_CharacterHasBackpackId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Characters_R_ConcentratesOnId",
                table: "Characters",
                column: "R_ConcentratesOnId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Characters_R_SpawnedByPowerId",
                table: "Characters",
                column: "R_SpawnedByPowerId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_UserId",
                table: "Characters",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChoiceGroupEffectBlueprint_R_EffectsId",
                table: "ChoiceGroupEffectBlueprint",
                column: "R_EffectsId");

            migrationBuilder.CreateIndex(
                name: "IX_ChoiceGroupPower_R_PowersId",
                table: "ChoiceGroupPower",
                column: "R_PowersId");

            migrationBuilder.CreateIndex(
                name: "IX_ChoiceGroups_R_GrantedByClassLevelId",
                table: "ChoiceGroups",
                column: "R_GrantedByClassLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_ChoiceGroups_R_GrantedByRaceLevelId",
                table: "ChoiceGroups",
                column: "R_GrantedByRaceLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassLevelImmaterialResourceAmount_R_ImmaterialResourceAmountsId",
                table: "ClassLevelImmaterialResourceAmount",
                column: "R_ImmaterialResourceAmountsId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassLevels_ImmaterialResourceInstanceId",
                table: "ClassLevels",
                column: "ImmaterialResourceInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassLevels_R_ClassId",
                table: "ClassLevels",
                column: "R_ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassPower_R_ClassesWithAccessId",
                table: "ClassPower",
                column: "R_ClassesWithAccessId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectBlueprints_R_CastedOnCharactersByAuraId",
                table: "EffectBlueprints",
                column: "R_CastedOnCharactersByAuraId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectBlueprints_R_CastedOnTilesByAuraId",
                table: "EffectBlueprints",
                column: "R_CastedOnTilesByAuraId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectBlueprints_R_CreatedByEquippingId",
                table: "EffectBlueprints",
                column: "R_CreatedByEquippingId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectBlueprints_R_GrantsProficiencyInItemFamilyId",
                table: "EffectBlueprints",
                column: "R_GrantsProficiencyInItemFamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectBlueprints_R_PowerId",
                table: "EffectBlueprints",
                column: "R_PowerId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectGroupField_R_EffectOnFieldId1",
                table: "EffectGroupField",
                column: "R_EffectOnFieldId1");

            migrationBuilder.CreateIndex(
                name: "IX_EffectGroups_R_GeneratesAuraId",
                table: "EffectGroups",
                column: "R_GeneratesAuraId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EffectGroups_R_ItemAffectedById",
                table: "EffectGroups",
                column: "R_ItemAffectedById");

            migrationBuilder.CreateIndex(
                name: "IX_EffectGroups_R_ItemGiveEffectId",
                table: "EffectGroups",
                column: "R_ItemGiveEffectId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectGroups_R_OriginatesFromAuraId",
                table: "EffectGroups",
                column: "R_OriginatesFromAuraId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectInstances_R_GrantsProficiencyInItemFamilyId",
                table: "EffectInstances",
                column: "R_GrantsProficiencyInItemFamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectInstances_R_OwnedByGroupId",
                table: "EffectInstances",
                column: "R_OwnedByGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Encounters_R_BoardId",
                table: "Encounters",
                column: "R_BoardId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Encounters_R_CampaignId",
                table: "Encounters",
                column: "R_CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipDatas_R_CharacterId",
                table: "EquipDatas",
                column: "R_CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipDatas_R_ItemId",
                table: "EquipDatas",
                column: "R_ItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentSlotItem_R_ItemsId",
                table: "EquipmentSlotItem",
                column: "R_ItemsId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentSlotRace_R_RacesId",
                table: "EquipmentSlotRace",
                column: "R_RacesId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldPower_R_FieldsCastingId",
                table: "FieldPower",
                column: "R_FieldsCastingId");

            migrationBuilder.CreateIndex(
                name: "IX_Fields_R_BoardId",
                table: "Fields",
                column: "R_BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Fields_R_OccupiedById",
                table: "Fields",
                column: "R_OccupiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ImmaterialResourceAmountRaceLevel_R_RaceLevelsId",
                table: "ImmaterialResourceAmountRaceLevel",
                column: "R_RaceLevelsId");

            migrationBuilder.CreateIndex(
                name: "IX_ImmaterialResourceAmounts_R_BlueprintId",
                table: "ImmaterialResourceAmounts",
                column: "R_BlueprintId");

            migrationBuilder.CreateIndex(
                name: "IX_ImmaterialResourceInstances_R_BlueprintId",
                table: "ImmaterialResourceInstances",
                column: "R_BlueprintId");

            migrationBuilder.CreateIndex(
                name: "IX_ImmaterialResourceInstances_R_CharacterId",
                table: "ImmaterialResourceInstances",
                column: "R_CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_ImmaterialResourceInstances_R_ResourceGrantedToItemId",
                table: "ImmaterialResourceInstances",
                column: "R_ResourceGrantedToItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemCostRequirements_PowerId",
                table: "ItemCostRequirements",
                column: "PowerId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemCostRequirements_R_ItemFamilyId",
                table: "ItemCostRequirements",
                column: "R_ItemFamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPower_R_ItemsGrantingPowerId",
                table: "ItemPower",
                column: "R_ItemsGrantingPowerId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_R_BackpackHasItemId",
                table: "Items",
                column: "R_BackpackHasItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_R_ItemInItemsFamilyId",
                table: "Items",
                column: "R_ItemInItemsFamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_Objects_R_OwnerId",
                table: "Objects",
                column: "R_OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipanceDatas_R_CharacterId",
                table: "ParticipanceDatas",
                column: "R_CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipanceDatas_R_EncounterId",
                table: "ParticipanceDatas",
                column: "R_EncounterId");

            migrationBuilder.CreateIndex(
                name: "IX_PowerWeapon_R_WeaponsCastingOnHitId",
                table: "PowerWeapon",
                column: "R_WeaponsCastingOnHitId");

            migrationBuilder.CreateIndex(
                name: "IX_Powers_R_UsesImmaterialResourceId",
                table: "Powers",
                column: "R_UsesImmaterialResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceLevels_R_RaceId",
                table: "RaceLevels",
                column: "R_RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopItems_R_ItemInShopId",
                table: "ShopItems",
                column: "R_ItemInShopId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopItems_R_ShopHasItemId",
                table: "ShopItems",
                column: "R_ShopHasItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Shops_R_CampaignId",
                table: "Shops",
                column: "R_CampaignId");

            migrationBuilder.AddForeignKey(
                name: "FK_Auras_Characters_R_CenteredAtCharacterId",
                table: "Auras",
                column: "R_CenteredAtCharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Campaigns_R_CampaignId",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectGroups_Items_R_ItemAffectedById",
                table: "EffectGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectGroups_Items_R_ItemGiveEffectId",
                table: "EffectGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_Characters_AspNetUsers_UserId",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_Objects_AspNetUsers_R_OwnerId",
                table: "Objects");

            migrationBuilder.DropForeignKey(
                name: "FK_Auras_Characters_R_CenteredAtCharacterId",
                table: "Auras");

            migrationBuilder.DropTable(
                name: "ActionLogs");

            migrationBuilder.DropTable(
                name: "Apparels");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CampaignUser");

            migrationBuilder.DropTable(
                name: "CharacterClassLevel");

            migrationBuilder.DropTable(
                name: "CharacterEffectGroup");

            migrationBuilder.DropTable(
                name: "CharacterPower");

            migrationBuilder.DropTable(
                name: "CharacterPower1");

            migrationBuilder.DropTable(
                name: "ChoiceGroupEffectBlueprint");

            migrationBuilder.DropTable(
                name: "ChoiceGroupPower");

            migrationBuilder.DropTable(
                name: "ClassLevelImmaterialResourceAmount");

            migrationBuilder.DropTable(
                name: "ClassPower");

            migrationBuilder.DropTable(
                name: "EffectGroupField");

            migrationBuilder.DropTable(
                name: "EffectInstances");

            migrationBuilder.DropTable(
                name: "EquipDatas");

            migrationBuilder.DropTable(
                name: "EquipmentSlotItem");

            migrationBuilder.DropTable(
                name: "EquipmentSlotRace");

            migrationBuilder.DropTable(
                name: "FieldPower");

            migrationBuilder.DropTable(
                name: "ImmaterialResourceAmountRaceLevel");

            migrationBuilder.DropTable(
                name: "ItemCostRequirements");

            migrationBuilder.DropTable(
                name: "ItemPower");

            migrationBuilder.DropTable(
                name: "PowerWeapon");

            migrationBuilder.DropTable(
                name: "ShopItems");

            migrationBuilder.DropTable(
                name: "Tools");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "EffectBlueprints");

            migrationBuilder.DropTable(
                name: "ChoiceGroups");

            migrationBuilder.DropTable(
                name: "EquipmentSlots");

            migrationBuilder.DropTable(
                name: "Fields");

            migrationBuilder.DropTable(
                name: "ImmaterialResourceAmounts");

            migrationBuilder.DropTable(
                name: "Weapons");

            migrationBuilder.DropTable(
                name: "Shops");

            migrationBuilder.DropTable(
                name: "ClassLevels");

            migrationBuilder.DropTable(
                name: "RaceLevels");

            migrationBuilder.DropTable(
                name: "ParticipanceDatas");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "ImmaterialResourceInstances");

            migrationBuilder.DropTable(
                name: "Encounters");

            migrationBuilder.DropTable(
                name: "Boards");

            migrationBuilder.DropTable(
                name: "Campaigns");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "ItemFamilies");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Backpacks");

            migrationBuilder.DropTable(
                name: "EffectGroups");

            migrationBuilder.DropTable(
                name: "Powers");

            migrationBuilder.DropTable(
                name: "Races");

            migrationBuilder.DropTable(
                name: "Auras");

            migrationBuilder.DropTable(
                name: "ImmaterialResourceBlueprints");

            migrationBuilder.DropTable(
                name: "Objects");
        }
    }
}
