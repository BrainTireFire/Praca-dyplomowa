using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class postgres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RefreshToken = table.Column<string>(type: "text", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Backpacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Backpacks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentSlots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentSlots", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImmaterialResourceBlueprints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    RefreshesOn = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImmaterialResourceBlueprints", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemFamilies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ItemType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemFamilies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Races",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Size = table.Column<int>(type: "integer", nullable: false),
                    Speed = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Races", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
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
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
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
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    R_OwnerId = table.Column<int>(type: "integer", nullable: true)
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
                name: "EquipmentSlotRace",
                columns: table => new
                {
                    R_EquipmentSlotsId = table.Column<int>(type: "integer", nullable: false),
                    R_RacesId = table.Column<int>(type: "integer", nullable: false)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    R_RaceId = table.Column<int>(type: "integer", nullable: false)
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
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    SizeX = table.Column<int>(type: "integer", nullable: false),
                    SizeY = table.Column<int>(type: "integer", nullable: false)
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
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
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
                    Id = table.Column<int>(type: "integer", nullable: false),
                    IsBlueprint = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Weight = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsSpellFocus = table.Column<bool>(type: "boolean", nullable: false),
                    OccupiesAllSlots = table.Column<bool>(type: "boolean", nullable: false),
                    Price_GoldPieces = table.Column<int>(type: "integer", nullable: false),
                    Price_SilverPieces = table.Column<int>(type: "integer", nullable: false),
                    Price_CopperPieces = table.Column<int>(type: "integer", nullable: false),
                    R_ItemInItemsFamilyId = table.Column<int>(type: "integer", nullable: false),
                    R_BackpackHasItemId = table.Column<int>(type: "integer", nullable: true)
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
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Languages_Objects_Id",
                        column: x => x.Id,
                        principalTable: "Objects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActionLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    R_CampaignId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActionLogs_Campaigns_R_CampaignId",
                        column: x => x.R_CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CampaignUser",
                columns: table => new
                {
                    R_UserAttendsAsPlayerToCamgainsId = table.Column<int>(type: "integer", nullable: false),
                    R_UsersAttendsCampaignsId = table.Column<int>(type: "integer", nullable: false)
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
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    R_CampaignId = table.Column<int>(type: "integer", nullable: true),
                    R_BoardId = table.Column<int>(type: "integer", nullable: false)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    R_CampaignId = table.Column<int>(type: "integer", nullable: false)
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
                    Id = table.Column<int>(type: "integer", nullable: false),
                    ArmorClass = table.Column<int>(type: "integer", nullable: false),
                    StealthDisadvantage = table.Column<bool>(type: "boolean", nullable: false),
                    StrengthRequirement = table.Column<int>(type: "integer", nullable: false)
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
                    R_ItemIsEquippableInSlotsId = table.Column<int>(type: "integer", nullable: false),
                    R_ItemsId = table.Column<int>(type: "integer", nullable: false)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
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
                name: "ShopItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Price_GoldPieces = table.Column<int>(type: "integer", nullable: false),
                    Price_SilverPieces = table.Column<int>(type: "integer", nullable: false),
                    Price_CopperPieces = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    R_ShopHasItemId = table.Column<int>(type: "integer", nullable: false),
                    R_ItemInShopId = table.Column<int>(type: "integer", nullable: false)
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
                name: "AdditionalValue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    additionalValueType = table.Column<int>(type: "integer", nullable: false),
                    R_LevelsInClassId = table.Column<int>(type: "integer", nullable: true),
                    Ability = table.Column<int>(type: "integer", nullable: true),
                    Skill = table.Column<int>(type: "integer", nullable: true),
                    DiceSetId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalValue", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Auras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Size = table.Column<int>(type: "integer", nullable: false),
                    R_CenteredAtCharacterId = table.Column<int>(type: "integer", nullable: false),
                    GeneratedBy_Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EffectGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsConstant = table.Column<bool>(type: "boolean", nullable: false),
                    DurationLeft = table.Column<int>(type: "integer", nullable: true),
                    DifficultyClassToBreak = table.Column<int>(type: "integer", nullable: true),
                    SavingThrow = table.Column<int>(type: "integer", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    R_ConcentratedOnByCharacterId = table.Column<int>(type: "integer", nullable: true),
                    R_GeneratesAuraId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EffectGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EffectGroups_Auras_R_GeneratesAuraId",
                        column: x => x.R_GeneratesAuraId,
                        principalTable: "Auras",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CharacterClassLevel",
                columns: table => new
                {
                    R_CharacterHasLevelsInClassId = table.Column<int>(type: "integer", nullable: false),
                    R_CharactersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterClassLevel", x => new { x.R_CharacterHasLevelsInClassId, x.R_CharactersId });
                });

            migrationBuilder.CreateTable(
                name: "CharacterPower",
                columns: table => new
                {
                    R_CharacterKnownsPowersId = table.Column<int>(type: "integer", nullable: false),
                    R_PowersKnownId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterPower", x => new { x.R_CharacterKnownsPowersId, x.R_PowersKnownId });
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Hitpoints = table.Column<int>(type: "integer", nullable: false),
                    IsNpc = table.Column<bool>(type: "boolean", nullable: false),
                    UsedHitDiceId = table.Column<int>(type: "integer", nullable: false),
                    SucceededDeathSavingThrows = table.Column<int>(type: "integer", nullable: false),
                    FailedDeathSavingThrows = table.Column<int>(type: "integer", nullable: false),
                    R_CharacterBelongsToRaceId = table.Column<int>(type: "integer", nullable: false),
                    R_ConcentratesOnId = table.Column<int>(type: "integer", nullable: true),
                    R_CharacterHasBackpackId = table.Column<int>(type: "integer", nullable: false),
                    R_CampaignId = table.Column<int>(type: "integer", nullable: true),
                    R_SpawnedByPowerId = table.Column<int>(type: "integer", nullable: true),
                    TemporaryHitpoints = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: true)
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Characters_EffectGroups_R_ConcentratesOnId",
                        column: x => x.R_ConcentratesOnId,
                        principalTable: "EffectGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Characters_Objects_Id",
                        column: x => x.Id,
                        principalTable: "Objects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Characters_Races_R_CharacterBelongsToRaceId",
                        column: x => x.R_CharacterBelongsToRaceId,
                        principalTable: "Races",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EquipDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    R_CharacterId = table.Column<int>(type: "integer", nullable: false),
                    R_ItemId = table.Column<int>(type: "integer", nullable: false)
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
                name: "ParticipanceDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InitiativeOrder = table.Column<int>(type: "integer", nullable: false),
                    IsSurprised = table.Column<bool>(type: "boolean", nullable: false),
                    NumberOfActionsTaken = table.Column<int>(type: "integer", nullable: false),
                    NumberOfBonusActionsTaken = table.Column<int>(type: "integer", nullable: false),
                    NumberOfAttacksTaken = table.Column<int>(type: "integer", nullable: false),
                    DistanceTraveled = table.Column<int>(type: "integer", nullable: false),
                    R_EncounterId = table.Column<int>(type: "integer", nullable: false),
                    R_CharacterId = table.Column<int>(type: "integer", nullable: false)
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
                name: "EquipDataEquipmentSlot",
                columns: table => new
                {
                    R_SlotsId = table.Column<int>(type: "integer", nullable: false),
                    UsagesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipDataEquipmentSlot", x => new { x.R_SlotsId, x.UsagesId });
                    table.ForeignKey(
                        name: "FK_EquipDataEquipmentSlot_EquipDatas_UsagesId",
                        column: x => x.UsagesId,
                        principalTable: "EquipDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipDataEquipmentSlot_EquipmentSlots_R_SlotsId",
                        column: x => x.R_SlotsId,
                        principalTable: "EquipmentSlots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PositionX = table.Column<int>(type: "integer", nullable: false),
                    PositionY = table.Column<int>(type: "integer", nullable: false),
                    PositionZ = table.Column<int>(type: "integer", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    FieldCoverLevel = table.Column<int>(type: "integer", nullable: false),
                    FieldMovementCost = table.Column<int>(type: "integer", nullable: false),
                    R_BoardId = table.Column<int>(type: "integer", nullable: false),
                    R_OccupiedById = table.Column<int>(type: "integer", nullable: true)
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
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EffectGroupField",
                columns: table => new
                {
                    R_EffectGroupOnFieldId = table.Column<int>(type: "integer", nullable: false),
                    R_EffectOnFieldId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EffectGroupField", x => new { x.R_EffectGroupOnFieldId, x.R_EffectOnFieldId });
                    table.ForeignKey(
                        name: "FK_EffectGroupField_EffectGroups_R_EffectGroupOnFieldId",
                        column: x => x.R_EffectGroupOnFieldId,
                        principalTable: "EffectGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EffectGroupField_Fields_R_EffectOnFieldId",
                        column: x => x.R_EffectOnFieldId,
                        principalTable: "Fields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChoiceGroupEffectBlueprint",
                columns: table => new
                {
                    R_ChoiceGroupsId = table.Column<int>(type: "integer", nullable: false),
                    R_EffectsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChoiceGroupEffectBlueprint", x => new { x.R_ChoiceGroupsId, x.R_EffectsId });
                });

            migrationBuilder.CreateTable(
                name: "ChoiceGroupPower",
                columns: table => new
                {
                    R_AlwaysAvailableThroughChoiceGroupId = table.Column<int>(type: "integer", nullable: false),
                    R_PowersAlwaysAvailableId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChoiceGroupPower", x => new { x.R_AlwaysAvailableThroughChoiceGroupId, x.R_PowersAlwaysAvailableId });
                });

            migrationBuilder.CreateTable(
                name: "ChoiceGroupPower1",
                columns: table => new
                {
                    R_PowersToPrepareId = table.Column<int>(type: "integer", nullable: false),
                    R_ToPrepareThroughChoiceGroupsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChoiceGroupPower1", x => new { x.R_PowersToPrepareId, x.R_ToPrepareThroughChoiceGroupsId });
                });

            migrationBuilder.CreateTable(
                name: "ChoiceGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    NumberToChoose = table.Column<int>(type: "integer", nullable: false),
                    R_GrantedByRaceLevelId = table.Column<int>(type: "integer", nullable: true),
                    R_GrantedByClassLevelId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChoiceGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChoiceGroups_RaceLevels_R_GrantedByRaceLevelId",
                        column: x => x.R_GrantedByRaceLevelId,
                        principalTable: "RaceLevels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ChoiceGroupUsages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    R_CharacterId = table.Column<int>(type: "integer", nullable: false),
                    R_ChoiceGroupId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChoiceGroupUsages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChoiceGroupUsages_Characters_R_CharacterId",
                        column: x => x.R_CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChoiceGroupUsages_ChoiceGroups_R_ChoiceGroupId",
                        column: x => x.R_ChoiceGroupId,
                        principalTable: "ChoiceGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImmaterialResourceAmounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    R_BlueprintId = table.Column<int>(type: "integer", nullable: false),
                    R_ChoiceGroupId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImmaterialResourceAmounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImmaterialResourceAmounts_ChoiceGroups_R_ChoiceGroupId",
                        column: x => x.R_ChoiceGroupId,
                        principalTable: "ChoiceGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImmaterialResourceAmounts_ImmaterialResourceBlueprints_R_Bl~",
                        column: x => x.R_BlueprintId,
                        principalTable: "ImmaterialResourceBlueprints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImmaterialResourceInstances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NeedsRefresh = table.Column<bool>(type: "boolean", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    R_ItemId = table.Column<int>(type: "integer", nullable: true),
                    R_BlueprintId = table.Column<int>(type: "integer", nullable: false),
                    R_CharacterId = table.Column<int>(type: "integer", nullable: true),
                    R_ChoiceGroupUsageId = table.Column<int>(type: "integer", nullable: true)
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
                        name: "FK_ImmaterialResourceInstances_ChoiceGroupUsages_R_ChoiceGroup~",
                        column: x => x.R_ChoiceGroupUsageId,
                        principalTable: "ChoiceGroupUsages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImmaterialResourceInstances_ImmaterialResourceBlueprints_R_~",
                        column: x => x.R_BlueprintId,
                        principalTable: "ImmaterialResourceBlueprints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImmaterialResourceInstances_Items_R_ItemId",
                        column: x => x.R_ItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ChoiceGroupUsagePower",
                columns: table => new
                {
                    R_AlwaysAvailableThroughChoiceGroupUsageId = table.Column<int>(type: "integer", nullable: false),
                    R_PowersAlwaysAvailableGrantedId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChoiceGroupUsagePower", x => new { x.R_AlwaysAvailableThroughChoiceGroupUsageId, x.R_PowersAlwaysAvailableGrantedId });
                    table.ForeignKey(
                        name: "FK_ChoiceGroupUsagePower_ChoiceGroupUsages_R_AlwaysAvailableTh~",
                        column: x => x.R_AlwaysAvailableThroughChoiceGroupUsageId,
                        principalTable: "ChoiceGroupUsages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChoiceGroupUsagePower1",
                columns: table => new
                {
                    R_PowersToPrepareGrantedId = table.Column<int>(type: "integer", nullable: false),
                    R_ToPrepareThroughChoiceGroupUsageId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChoiceGroupUsagePower1", x => new { x.R_PowersToPrepareGrantedId, x.R_ToPrepareThroughChoiceGroupUsageId });
                    table.ForeignKey(
                        name: "FK_ChoiceGroupUsagePower1_ChoiceGroupUsages_R_ToPrepareThrough~",
                        column: x => x.R_ToPrepareThroughChoiceGroupUsageId,
                        principalTable: "ChoiceGroupUsages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    MaximumPreparedSpellsFormulaId = table.Column<int>(type: "integer", nullable: false),
                    SpellcastingAbility = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Powers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    RequiredActionType = table.Column<int>(type: "integer", nullable: false),
                    IsImplemented = table.Column<bool>(type: "boolean", nullable: false),
                    IsMagic = table.Column<bool>(type: "boolean", nullable: false),
                    CastableBy = table.Column<int>(type: "integer", nullable: false),
                    PowerType = table.Column<int>(type: "integer", nullable: false),
                    TargetType = table.Column<int>(type: "integer", nullable: false),
                    IsRanged = table.Column<bool>(type: "boolean", nullable: false),
                    Range = table.Column<int>(type: "integer", nullable: true),
                    MaxTargets = table.Column<int>(type: "integer", nullable: false),
                    MaxTargetsToExclude = table.Column<int>(type: "integer", nullable: false),
                    AreaSize = table.Column<int>(type: "integer", nullable: true),
                    AreaShape = table.Column<int>(type: "integer", nullable: true),
                    AuraSize = table.Column<int>(type: "integer", nullable: true),
                    OverrideCastersDC = table.Column<bool>(type: "boolean", nullable: false),
                    DifficultyClass = table.Column<int>(type: "integer", nullable: true),
                    SavingThrow = table.Column<int>(type: "integer", nullable: true),
                    RequiresConcentration = table.Column<bool>(type: "boolean", nullable: false),
                    SavingThrowBehaviour = table.Column<int>(type: "integer", nullable: true),
                    SavingThrowRoll = table.Column<int>(type: "integer", nullable: true),
                    VerbalComponent = table.Column<bool>(type: "boolean", nullable: false),
                    SomaticComponent = table.Column<bool>(type: "boolean", nullable: false),
                    Duration = table.Column<int>(type: "integer", nullable: false),
                    UpcastBy = table.Column<int>(type: "integer", nullable: false),
                    R_ClassForUpcastingId = table.Column<int>(type: "integer", nullable: true),
                    R_UsesImmaterialResourceId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Powers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Powers_Classes_R_ClassForUpcastingId",
                        column: x => x.R_ClassForUpcastingId,
                        principalTable: "Classes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Powers_ImmaterialResourceBlueprints_R_UsesImmaterialResourc~",
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
                name: "PowerSelections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    R_ClassId = table.Column<int>(type: "integer", nullable: false),
                    R_CharacterId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowerSelections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PowerSelections_Characters_R_CharacterId",
                        column: x => x.R_CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PowerSelections_Classes_R_ClassId",
                        column: x => x.R_ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassPower",
                columns: table => new
                {
                    R_AccessiblePowersId = table.Column<int>(type: "integer", nullable: false),
                    R_ClassesWithAccessId = table.Column<int>(type: "integer", nullable: false)
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
                name: "EffectBlueprints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    ResourceAmount = table.Column<int>(type: "integer", nullable: false),
                    Saved = table.Column<bool>(type: "boolean", nullable: false),
                    Conditional = table.Column<bool>(type: "boolean", nullable: false),
                    IsImplemented = table.Column<bool>(type: "boolean", nullable: false),
                    HasNoEffectInCombat = table.Column<bool>(type: "boolean", nullable: false),
                    R_CreatedByEquippingId = table.Column<int>(type: "integer", nullable: true),
                    R_PowerId = table.Column<int>(type: "integer", nullable: true),
                    R_CastedOnCharactersByAuraId = table.Column<int>(type: "integer", nullable: true),
                    R_CastedOnTilesByAuraId = table.Column<int>(type: "integer", nullable: true),
                    Discriminator = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: false),
                    R_LanguageId = table.Column<int>(type: "integer", nullable: true),
                    MovementCostEffectType_MovementCostEffect = table.Column<int>(type: "integer", nullable: true),
                    R_GrantsProficiencyInItemFamilyId = table.Column<int>(type: "integer", nullable: true),
                    ProficiencyEffectType_ProficiencyEffect = table.Column<int>(type: "integer", nullable: true),
                    ProficiencyEffectType_ItemType = table.Column<int>(type: "integer", nullable: true),
                    ResistanceEffectType_ResistanceEffect = table.Column<int>(type: "integer", nullable: true),
                    ResistanceEffectType_ResistanceEffect_DamageType = table.Column<int>(type: "integer", nullable: true),
                    StatusEffectType_StatusEffect = table.Column<int>(type: "integer", nullable: true),
                    RollMoment = table.Column<int>(type: "integer", nullable: true),
                    DiceSetId = table.Column<int>(type: "integer", nullable: true),
                    AbilityEffectType_AbilityEffect = table.Column<int>(type: "integer", nullable: true),
                    AbilityEffectType_AbilityEffect_Ability = table.Column<int>(type: "integer", nullable: true),
                    ActionEffectType_ActionEffect = table.Column<int>(type: "integer", nullable: true),
                    AttackPerAttackActionEffectType_AttackPerActionEffect = table.Column<int>(type: "integer", nullable: true),
                    AttackRollEffectType_AttackRollEffect_Range = table.Column<int>(type: "integer", nullable: true),
                    AttackRollEffectType_AttackRollEffect_Source = table.Column<int>(type: "integer", nullable: true),
                    AttackRollEffectType_AttackRollEffect_Type = table.Column<int>(type: "integer", nullable: true),
                    DamageEffectType_DamageEffect = table.Column<int>(type: "integer", nullable: true),
                    DamageEffectType_DamageEffect_DamageType = table.Column<int>(type: "integer", nullable: true),
                    HitpointEffectType_HitpointEffect = table.Column<int>(type: "integer", nullable: true),
                    MovementEffectType_MovementEffect = table.Column<int>(type: "integer", nullable: true),
                    SavingThrowEffectType_SavingThrowEffect = table.Column<int>(type: "integer", nullable: true),
                    SavingThrowEffectType_SavingThrowEffect_Ability = table.Column<int>(type: "integer", nullable: true),
                    SavingThrowEffectType_SavingThrowEffect_Condition = table.Column<int>(type: "integer", nullable: true),
                    SavingThrowEffectType_SavingThrowEffect_Nature = table.Column<int>(type: "integer", nullable: true),
                    SizeEffectType_SizeEffect = table.Column<int>(type: "integer", nullable: true),
                    SizeEffectType_SizeEffect_SizeToSet = table.Column<int>(type: "integer", nullable: true),
                    SkillEffectType_SkillEffect = table.Column<int>(type: "integer", nullable: true),
                    SkillEffectType_SkillEffect_Skill = table.Column<int>(type: "integer", nullable: true)
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
                        name: "FK_EffectBlueprints_ItemFamilies_R_GrantsProficiencyInItemFami~",
                        column: x => x.R_GrantsProficiencyInItemFamilyId,
                        principalTable: "ItemFamilies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EffectBlueprints_Items_R_CreatedByEquippingId",
                        column: x => x.R_CreatedByEquippingId,
                        principalTable: "Items",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EffectBlueprints_Languages_R_LanguageId",
                        column: x => x.R_LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EffectBlueprints_Powers_R_PowerId",
                        column: x => x.R_PowerId,
                        principalTable: "Powers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FieldPower",
                columns: table => new
                {
                    R_CasterPowersId = table.Column<int>(type: "integer", nullable: false),
                    R_FieldsCastingId = table.Column<int>(type: "integer", nullable: false)
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
                name: "ItemCostRequirements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Worth_GoldPieces = table.Column<int>(type: "integer", nullable: false),
                    Worth_SilverPieces = table.Column<int>(type: "integer", nullable: false),
                    Worth_CopperPieces = table.Column<int>(type: "integer", nullable: false),
                    PowerId = table.Column<int>(type: "integer", nullable: false),
                    R_ItemFamilyId = table.Column<int>(type: "integer", nullable: false)
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
                    R_EquipItemGrantsAccessToPowerId = table.Column<int>(type: "integer", nullable: false),
                    R_ItemsGrantingPowerId = table.Column<int>(type: "integer", nullable: false)
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
                name: "PowerPowerSelection",
                columns: table => new
                {
                    R_CharacterPreparedPowersId = table.Column<int>(type: "integer", nullable: false),
                    R_PreparedPowersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowerPowerSelection", x => new { x.R_CharacterPreparedPowersId, x.R_PreparedPowersId });
                    table.ForeignKey(
                        name: "FK_PowerPowerSelection_PowerSelections_R_CharacterPreparedPowe~",
                        column: x => x.R_CharacterPreparedPowersId,
                        principalTable: "PowerSelections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PowerPowerSelection_Powers_R_PreparedPowersId",
                        column: x => x.R_PreparedPowersId,
                        principalTable: "Powers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiceSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    R_ValueEffectBlueprintId = table.Column<int>(type: "integer", nullable: true),
                    d20 = table.Column<int>(type: "integer", nullable: false),
                    d12 = table.Column<int>(type: "integer", nullable: false),
                    d10 = table.Column<int>(type: "integer", nullable: false),
                    d8 = table.Column<int>(type: "integer", nullable: false),
                    d6 = table.Column<int>(type: "integer", nullable: false),
                    d4 = table.Column<int>(type: "integer", nullable: false),
                    d100 = table.Column<int>(type: "integer", nullable: false),
                    flat = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiceSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiceSet_EffectBlueprints_R_ValueEffectBlueprintId",
                        column: x => x.R_ValueEffectBlueprintId,
                        principalTable: "EffectBlueprints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    HitDieId = table.Column<int>(type: "integer", nullable: false),
                    HitPoints = table.Column<int>(type: "integer", nullable: false),
                    R_ClassId = table.Column<int>(type: "integer", nullable: false)
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
                        name: "FK_ClassLevels_DiceSet_HitDieId",
                        column: x => x.HitDieId,
                        principalTable: "DiceSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EffectInstances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Conditional = table.Column<bool>(type: "boolean", nullable: false),
                    IsImplemented = table.Column<bool>(type: "boolean", nullable: false),
                    HasNoEffectInCombat = table.Column<bool>(type: "boolean", nullable: false),
                    R_OwnedByGroupId = table.Column<int>(type: "integer", nullable: true),
                    R_GrantedThroughId = table.Column<int>(type: "integer", nullable: true),
                    R_TargetedCharacterId = table.Column<int>(type: "integer", nullable: true),
                    R_TargetedItemId = table.Column<int>(type: "integer", nullable: true),
                    R_GrantedByEquippingItemId = table.Column<int>(type: "integer", nullable: true),
                    Discriminator = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: false),
                    ItemFamilyId = table.Column<int>(type: "integer", nullable: true),
                    R_LanguageId = table.Column<int>(type: "integer", nullable: true),
                    EffectType_MovementCostEffect = table.Column<int>(type: "integer", nullable: true),
                    R_GrantsProficiencyInItemFamilyId = table.Column<int>(type: "integer", nullable: true),
                    ProficiencyEffectType_ProficiencyEffect = table.Column<int>(type: "integer", nullable: true),
                    ProficiencyEffectType_ItemType = table.Column<int>(type: "integer", nullable: true),
                    EffectType_ResistanceEffect = table.Column<int>(type: "integer", nullable: true),
                    EffectType_ResistanceEffect_DamageType = table.Column<int>(type: "integer", nullable: true),
                    EffectType_StatusEffect = table.Column<int>(type: "integer", nullable: true),
                    DiceSetId = table.Column<int>(type: "integer", nullable: true),
                    RollerId = table.Column<int>(type: "integer", nullable: true),
                    EffectType_AbilityEffect = table.Column<int>(type: "integer", nullable: true),
                    EffectType_AbilityEffect_Ability = table.Column<int>(type: "integer", nullable: true),
                    EffectType_ActionEffect = table.Column<int>(type: "integer", nullable: true),
                    EffectType_AttackPerActionEffect = table.Column<int>(type: "integer", nullable: true),
                    EffectType_AttackRollEffect_Range = table.Column<int>(type: "integer", nullable: true),
                    EffectType_AttackRollEffect_Source = table.Column<int>(type: "integer", nullable: true),
                    EffectType_AttackRollEffect_Type = table.Column<int>(type: "integer", nullable: true),
                    EffectType_DamageEffect = table.Column<int>(type: "integer", nullable: true),
                    EffectType_DamageEffect_DamageType = table.Column<int>(type: "integer", nullable: true),
                    CriticalHit = table.Column<bool>(type: "boolean", nullable: true),
                    EffectType_HitpointEffect = table.Column<int>(type: "integer", nullable: true),
                    EffectType_MovementEffect = table.Column<int>(type: "integer", nullable: true),
                    EffectType_SavingThrowEffect = table.Column<int>(type: "integer", nullable: true),
                    EffectType_SavingThrowEffect_Ability = table.Column<int>(type: "integer", nullable: true),
                    EffectType_SavingThrowEffect_Condition = table.Column<int>(type: "integer", nullable: true),
                    EffectType_SavingThrowEffect_Nature = table.Column<int>(type: "integer", nullable: true),
                    EffectType_SizeEffect = table.Column<int>(type: "integer", nullable: true),
                    EffectType_SizeEffect_SizeToSet = table.Column<int>(type: "integer", nullable: true),
                    EffectType_SkillEffect = table.Column<int>(type: "integer", nullable: true),
                    EffectType_SkillEffect_Skill = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EffectInstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EffectInstances_Characters_R_TargetedCharacterId",
                        column: x => x.R_TargetedCharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EffectInstances_Characters_RollerId",
                        column: x => x.RollerId,
                        principalTable: "Characters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EffectInstances_ChoiceGroupUsages_R_GrantedThroughId",
                        column: x => x.R_GrantedThroughId,
                        principalTable: "ChoiceGroupUsages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EffectInstances_DiceSet_DiceSetId",
                        column: x => x.DiceSetId,
                        principalTable: "DiceSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EffectInstances_EffectGroups_R_OwnedByGroupId",
                        column: x => x.R_OwnedByGroupId,
                        principalTable: "EffectGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EffectInstances_ItemFamilies_ItemFamilyId",
                        column: x => x.ItemFamilyId,
                        principalTable: "ItemFamilies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EffectInstances_ItemFamilies_R_GrantsProficiencyInItemFamil~",
                        column: x => x.R_GrantsProficiencyInItemFamilyId,
                        principalTable: "ItemFamilies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EffectInstances_Items_R_GrantedByEquippingItemId",
                        column: x => x.R_GrantedByEquippingItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EffectInstances_Items_R_TargetedItemId",
                        column: x => x.R_TargetedItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EffectInstances_Languages_R_LanguageId",
                        column: x => x.R_LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Weapons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    WeaponWeight = table.Column<int>(type: "integer", nullable: false),
                    DamageType = table.Column<int>(type: "integer", nullable: false),
                    DamageValueId = table.Column<int>(type: "integer", nullable: false),
                    Range = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weapons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Weapons_DiceSet_DamageValueId",
                        column: x => x.DamageValueId,
                        principalTable: "DiceSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Weapons_Items_Id",
                        column: x => x.Id,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MeleeWeapons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Finesse = table.Column<bool>(type: "boolean", nullable: false),
                    Reach = table.Column<bool>(type: "boolean", nullable: false),
                    Thrown = table.Column<bool>(type: "boolean", nullable: false),
                    Versatile = table.Column<bool>(type: "boolean", nullable: false),
                    VersatileDamageValueId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeleeWeapons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeleeWeapons_DiceSet_VersatileDamageValueId",
                        column: x => x.VersatileDamageValueId,
                        principalTable: "DiceSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeleeWeapons_Weapons_Id",
                        column: x => x.Id,
                        principalTable: "Weapons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PowerWeapon",
                columns: table => new
                {
                    R_PowersCastedOnHitId = table.Column<int>(type: "integer", nullable: false),
                    R_WeaponsCastingOnHitId = table.Column<int>(type: "integer", nullable: false)
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
                name: "RangedWeapons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Loaded = table.Column<bool>(type: "boolean", nullable: false),
                    IsReloaded = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RangedWeapons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RangedWeapons_Weapons_Id",
                        column: x => x.Id,
                        principalTable: "Weapons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActionLogs_R_CampaignId",
                table: "ActionLogs",
                column: "R_CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalValue_DiceSetId",
                table: "AdditionalValue",
                column: "DiceSetId");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalValue_R_LevelsInClassId",
                table: "AdditionalValue",
                column: "R_LevelsInClassId");

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
                name: "IX_CharacterPower_R_PowersKnownId",
                table: "CharacterPower",
                column: "R_PowersKnownId");

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
                name: "IX_Characters_UsedHitDiceId",
                table: "Characters",
                column: "UsedHitDiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_UserId",
                table: "Characters",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChoiceGroupEffectBlueprint_R_EffectsId",
                table: "ChoiceGroupEffectBlueprint",
                column: "R_EffectsId");

            migrationBuilder.CreateIndex(
                name: "IX_ChoiceGroupPower_R_PowersAlwaysAvailableId",
                table: "ChoiceGroupPower",
                column: "R_PowersAlwaysAvailableId");

            migrationBuilder.CreateIndex(
                name: "IX_ChoiceGroupPower1_R_ToPrepareThroughChoiceGroupsId",
                table: "ChoiceGroupPower1",
                column: "R_ToPrepareThroughChoiceGroupsId");

            migrationBuilder.CreateIndex(
                name: "IX_ChoiceGroups_R_GrantedByClassLevelId",
                table: "ChoiceGroups",
                column: "R_GrantedByClassLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_ChoiceGroups_R_GrantedByRaceLevelId",
                table: "ChoiceGroups",
                column: "R_GrantedByRaceLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_ChoiceGroupUsagePower_R_PowersAlwaysAvailableGrantedId",
                table: "ChoiceGroupUsagePower",
                column: "R_PowersAlwaysAvailableGrantedId");

            migrationBuilder.CreateIndex(
                name: "IX_ChoiceGroupUsagePower1_R_ToPrepareThroughChoiceGroupUsageId",
                table: "ChoiceGroupUsagePower1",
                column: "R_ToPrepareThroughChoiceGroupUsageId");

            migrationBuilder.CreateIndex(
                name: "IX_ChoiceGroupUsages_R_CharacterId",
                table: "ChoiceGroupUsages",
                column: "R_CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_ChoiceGroupUsages_R_ChoiceGroupId",
                table: "ChoiceGroupUsages",
                column: "R_ChoiceGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_MaximumPreparedSpellsFormulaId",
                table: "Classes",
                column: "MaximumPreparedSpellsFormulaId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassLevels_HitDieId",
                table: "ClassLevels",
                column: "HitDieId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassLevels_R_ClassId",
                table: "ClassLevels",
                column: "R_ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassPower_R_ClassesWithAccessId",
                table: "ClassPower",
                column: "R_ClassesWithAccessId");

            migrationBuilder.CreateIndex(
                name: "IX_DiceSet_R_ValueEffectBlueprintId",
                table: "DiceSet",
                column: "R_ValueEffectBlueprintId",
                unique: true);

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
                name: "IX_EffectBlueprints_R_LanguageId",
                table: "EffectBlueprints",
                column: "R_LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectBlueprints_R_PowerId",
                table: "EffectBlueprints",
                column: "R_PowerId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectGroupField_R_EffectOnFieldId",
                table: "EffectGroupField",
                column: "R_EffectOnFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectGroups_R_GeneratesAuraId",
                table: "EffectGroups",
                column: "R_GeneratesAuraId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EffectInstances_DiceSetId",
                table: "EffectInstances",
                column: "DiceSetId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectInstances_ItemFamilyId",
                table: "EffectInstances",
                column: "ItemFamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectInstances_R_GrantedByEquippingItemId",
                table: "EffectInstances",
                column: "R_GrantedByEquippingItemId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectInstances_R_GrantedThroughId",
                table: "EffectInstances",
                column: "R_GrantedThroughId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectInstances_R_GrantsProficiencyInItemFamilyId",
                table: "EffectInstances",
                column: "R_GrantsProficiencyInItemFamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectInstances_R_LanguageId",
                table: "EffectInstances",
                column: "R_LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectInstances_R_OwnedByGroupId",
                table: "EffectInstances",
                column: "R_OwnedByGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectInstances_R_TargetedCharacterId",
                table: "EffectInstances",
                column: "R_TargetedCharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectInstances_R_TargetedItemId",
                table: "EffectInstances",
                column: "R_TargetedItemId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectInstances_RollerId",
                table: "EffectInstances",
                column: "RollerId");

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
                name: "IX_EquipDataEquipmentSlot_UsagesId",
                table: "EquipDataEquipmentSlot",
                column: "UsagesId");

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
                name: "IX_ImmaterialResourceAmounts_R_BlueprintId",
                table: "ImmaterialResourceAmounts",
                column: "R_BlueprintId");

            migrationBuilder.CreateIndex(
                name: "IX_ImmaterialResourceAmounts_R_ChoiceGroupId",
                table: "ImmaterialResourceAmounts",
                column: "R_ChoiceGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ImmaterialResourceInstances_R_BlueprintId",
                table: "ImmaterialResourceInstances",
                column: "R_BlueprintId");

            migrationBuilder.CreateIndex(
                name: "IX_ImmaterialResourceInstances_R_CharacterId",
                table: "ImmaterialResourceInstances",
                column: "R_CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_ImmaterialResourceInstances_R_ChoiceGroupUsageId",
                table: "ImmaterialResourceInstances",
                column: "R_ChoiceGroupUsageId");

            migrationBuilder.CreateIndex(
                name: "IX_ImmaterialResourceInstances_R_ItemId",
                table: "ImmaterialResourceInstances",
                column: "R_ItemId");

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
                name: "IX_MeleeWeapons_VersatileDamageValueId",
                table: "MeleeWeapons",
                column: "VersatileDamageValueId");

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
                name: "IX_PowerPowerSelection_R_PreparedPowersId",
                table: "PowerPowerSelection",
                column: "R_PreparedPowersId");

            migrationBuilder.CreateIndex(
                name: "IX_Powers_R_ClassForUpcastingId",
                table: "Powers",
                column: "R_ClassForUpcastingId");

            migrationBuilder.CreateIndex(
                name: "IX_Powers_R_UsesImmaterialResourceId",
                table: "Powers",
                column: "R_UsesImmaterialResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_PowerSelections_R_CharacterId",
                table: "PowerSelections",
                column: "R_CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_PowerSelections_R_ClassId",
                table: "PowerSelections",
                column: "R_ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_PowerWeapon_R_WeaponsCastingOnHitId",
                table: "PowerWeapon",
                column: "R_WeaponsCastingOnHitId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Weapons_DamageValueId",
                table: "Weapons",
                column: "DamageValueId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalValue_Classes_R_LevelsInClassId",
                table: "AdditionalValue",
                column: "R_LevelsInClassId",
                principalTable: "Classes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalValue_DiceSet_DiceSetId",
                table: "AdditionalValue",
                column: "DiceSetId",
                principalTable: "DiceSet",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Auras_Characters_R_CenteredAtCharacterId",
                table: "Auras",
                column: "R_CenteredAtCharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterClassLevel_Characters_R_CharactersId",
                table: "CharacterClassLevel",
                column: "R_CharactersId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterClassLevel_ClassLevels_R_CharacterHasLevelsInClass~",
                table: "CharacterClassLevel",
                column: "R_CharacterHasLevelsInClassId",
                principalTable: "ClassLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterPower_Characters_R_CharacterKnownsPowersId",
                table: "CharacterPower",
                column: "R_CharacterKnownsPowersId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterPower_Powers_R_PowersKnownId",
                table: "CharacterPower",
                column: "R_PowersKnownId",
                principalTable: "Powers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_DiceSet_UsedHitDiceId",
                table: "Characters",
                column: "UsedHitDiceId",
                principalTable: "DiceSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Powers_R_SpawnedByPowerId",
                table: "Characters",
                column: "R_SpawnedByPowerId",
                principalTable: "Powers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChoiceGroupEffectBlueprint_ChoiceGroups_R_ChoiceGroupsId",
                table: "ChoiceGroupEffectBlueprint",
                column: "R_ChoiceGroupsId",
                principalTable: "ChoiceGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChoiceGroupEffectBlueprint_EffectBlueprints_R_EffectsId",
                table: "ChoiceGroupEffectBlueprint",
                column: "R_EffectsId",
                principalTable: "EffectBlueprints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChoiceGroupPower_ChoiceGroups_R_AlwaysAvailableThroughChoic~",
                table: "ChoiceGroupPower",
                column: "R_AlwaysAvailableThroughChoiceGroupId",
                principalTable: "ChoiceGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChoiceGroupPower_Powers_R_PowersAlwaysAvailableId",
                table: "ChoiceGroupPower",
                column: "R_PowersAlwaysAvailableId",
                principalTable: "Powers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChoiceGroupPower1_ChoiceGroups_R_ToPrepareThroughChoiceGrou~",
                table: "ChoiceGroupPower1",
                column: "R_ToPrepareThroughChoiceGroupsId",
                principalTable: "ChoiceGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChoiceGroupPower1_Powers_R_PowersToPrepareId",
                table: "ChoiceGroupPower1",
                column: "R_PowersToPrepareId",
                principalTable: "Powers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChoiceGroups_ClassLevels_R_GrantedByClassLevelId",
                table: "ChoiceGroups",
                column: "R_GrantedByClassLevelId",
                principalTable: "ClassLevels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChoiceGroupUsagePower_Powers_R_PowersAlwaysAvailableGranted~",
                table: "ChoiceGroupUsagePower",
                column: "R_PowersAlwaysAvailableGrantedId",
                principalTable: "Powers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChoiceGroupUsagePower1_Powers_R_PowersToPrepareGrantedId",
                table: "ChoiceGroupUsagePower1",
                column: "R_PowersToPrepareGrantedId",
                principalTable: "Powers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_DiceSet_MaximumPreparedSpellsFormulaId",
                table: "Classes",
                column: "MaximumPreparedSpellsFormulaId",
                principalTable: "DiceSet",
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
                name: "FK_Powers_Classes_R_ClassForUpcastingId",
                table: "Powers");

            migrationBuilder.DropForeignKey(
                name: "FK_Characters_DiceSet_UsedHitDiceId",
                table: "Characters");

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
                name: "AdditionalValue");

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
                name: "CharacterPower");

            migrationBuilder.DropTable(
                name: "ChoiceGroupEffectBlueprint");

            migrationBuilder.DropTable(
                name: "ChoiceGroupPower");

            migrationBuilder.DropTable(
                name: "ChoiceGroupPower1");

            migrationBuilder.DropTable(
                name: "ChoiceGroupUsagePower");

            migrationBuilder.DropTable(
                name: "ChoiceGroupUsagePower1");

            migrationBuilder.DropTable(
                name: "ClassPower");

            migrationBuilder.DropTable(
                name: "EffectGroupField");

            migrationBuilder.DropTable(
                name: "EffectInstances");

            migrationBuilder.DropTable(
                name: "EquipDataEquipmentSlot");

            migrationBuilder.DropTable(
                name: "EquipmentSlotItem");

            migrationBuilder.DropTable(
                name: "EquipmentSlotRace");

            migrationBuilder.DropTable(
                name: "FieldPower");

            migrationBuilder.DropTable(
                name: "ImmaterialResourceAmounts");

            migrationBuilder.DropTable(
                name: "ImmaterialResourceInstances");

            migrationBuilder.DropTable(
                name: "ItemCostRequirements");

            migrationBuilder.DropTable(
                name: "ItemPower");

            migrationBuilder.DropTable(
                name: "MeleeWeapons");

            migrationBuilder.DropTable(
                name: "PowerPowerSelection");

            migrationBuilder.DropTable(
                name: "PowerWeapon");

            migrationBuilder.DropTable(
                name: "RangedWeapons");

            migrationBuilder.DropTable(
                name: "ShopItems");

            migrationBuilder.DropTable(
                name: "Tools");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "EquipDatas");

            migrationBuilder.DropTable(
                name: "EquipmentSlots");

            migrationBuilder.DropTable(
                name: "Fields");

            migrationBuilder.DropTable(
                name: "ChoiceGroupUsages");

            migrationBuilder.DropTable(
                name: "PowerSelections");

            migrationBuilder.DropTable(
                name: "Weapons");

            migrationBuilder.DropTable(
                name: "Shops");

            migrationBuilder.DropTable(
                name: "ParticipanceDatas");

            migrationBuilder.DropTable(
                name: "ChoiceGroups");

            migrationBuilder.DropTable(
                name: "Encounters");

            migrationBuilder.DropTable(
                name: "ClassLevels");

            migrationBuilder.DropTable(
                name: "RaceLevels");

            migrationBuilder.DropTable(
                name: "Boards");

            migrationBuilder.DropTable(
                name: "Campaigns");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "DiceSet");

            migrationBuilder.DropTable(
                name: "EffectBlueprints");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Languages");

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
