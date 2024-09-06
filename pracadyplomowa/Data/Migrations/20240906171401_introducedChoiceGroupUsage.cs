using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class introducedChoiceGroupUsage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "StatusEffectType_StatusEffect",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "SkillEffectType_SkillEffect_Skill",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "SkillEffectType_SkillEffect",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "SizeEffectType_SizeEffect_SizeToSet",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "SizeEffectType_SizeEffect",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "SizeEffectType_SizeBonus",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "SavingThrowEffectType_SavingThrowEffect_Ability",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "SavingThrowEffectType_SavingThrowEffect",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "ResistanceEffectType_ResistanceEffect_DamageType",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "ResistanceEffectType_ResistanceEffect",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "ProficiencyEffectType_ProficiencyEffect",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "MovementEffectType_MovementEffect",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "MovementCostEffectType_MovementCost_Multiplier",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "HitpointEffectType_HitpointEffect",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "DamageEffectType_DamageEffect_DamageType",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "DamageEffectType_DamageEffect",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "AttackRollEffectType_AttackRollEffect_Type",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "AttackRollEffectType_AttackRollEffect_Source",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "AttackRollEffectType_AttackRollEffect_Range",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "AttackPerAttackActionEffectType_AttackPerActionEffect",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "ActionEffectType_ActionEffect",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "AbilityEffectType_AbilityEffect_Ability",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "AbilityEffectType_AbilityEffect",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "StatusEffectType_StatusEffect",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "SkillEffectType_SkillEffect_Skill",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "SkillEffectType_SkillEffect",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "SizeEffectType_SizeEffect_SizeToSet",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "SizeEffectType_SizeEffect",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "SizeEffectType_SizeBonus",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "SavingThrowEffectType_SavingThrowEffect_Ability",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "SavingThrowEffectType_SavingThrowEffect",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "ResistanceEffectType_ResistanceEffect_DamageType",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "ResistanceEffectType_ResistanceEffect",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "ProficiencyEffectType_ProficiencyEffect",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "MovementEffectType_MovementEffect",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "MovementCostEffectType_MovementCost_Multiplier",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "HitpointEffectType_HitpointEffect",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "DamageEffectType_DamageEffect_DamageType",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "DamageEffectType_DamageEffect",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "AttackRollEffectType_AttackRollEffect_Type",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "AttackRollEffectType_AttackRollEffect_Source",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "AttackRollEffectType_AttackRollEffect_Range",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "AttackPerAttackActionEffectType_AttackPerActionEffect",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "ActionEffectType_ActionEffect",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "AbilityEffectType_AbilityEffect_Ability",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "AbilityEffectType_AbilityEffect",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateTable(
                name: "ChoiceGroupUsages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    R_CharacterId = table.Column<int>(type: "INTEGER", nullable: false),
                    R_ChoiceGroupId = table.Column<int>(type: "INTEGER", nullable: false)
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
                name: "ChoiceGroupUsageEffectGroup",
                columns: table => new
                {
                    R_EffectGroupsGrantedId = table.Column<int>(type: "INTEGER", nullable: false),
                    R_GrantedThroughId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChoiceGroupUsageEffectGroup", x => new { x.R_EffectGroupsGrantedId, x.R_GrantedThroughId });
                    table.ForeignKey(
                        name: "FK_ChoiceGroupUsageEffectGroup_ChoiceGroupUsages_R_GrantedThroughId",
                        column: x => x.R_GrantedThroughId,
                        principalTable: "ChoiceGroupUsages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChoiceGroupUsageEffectGroup_EffectGroups_R_EffectGroupsGrantedId",
                        column: x => x.R_EffectGroupsGrantedId,
                        principalTable: "EffectGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChoiceGroupUsagePower",
                columns: table => new
                {
                    R_GrantedThroughId = table.Column<int>(type: "INTEGER", nullable: false),
                    R_PowersGrantedId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChoiceGroupUsagePower", x => new { x.R_GrantedThroughId, x.R_PowersGrantedId });
                    table.ForeignKey(
                        name: "FK_ChoiceGroupUsagePower_ChoiceGroupUsages_R_GrantedThroughId",
                        column: x => x.R_GrantedThroughId,
                        principalTable: "ChoiceGroupUsages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChoiceGroupUsagePower_Powers_R_PowersGrantedId",
                        column: x => x.R_PowersGrantedId,
                        principalTable: "Powers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChoiceGroupUsageEffectGroup_R_GrantedThroughId",
                table: "ChoiceGroupUsageEffectGroup",
                column: "R_GrantedThroughId");

            migrationBuilder.CreateIndex(
                name: "IX_ChoiceGroupUsagePower_R_PowersGrantedId",
                table: "ChoiceGroupUsagePower",
                column: "R_PowersGrantedId");

            migrationBuilder.CreateIndex(
                name: "IX_ChoiceGroupUsages_R_CharacterId",
                table: "ChoiceGroupUsages",
                column: "R_CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_ChoiceGroupUsages_R_ChoiceGroupId",
                table: "ChoiceGroupUsages",
                column: "R_ChoiceGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChoiceGroupUsageEffectGroup");

            migrationBuilder.DropTable(
                name: "ChoiceGroupUsagePower");

            migrationBuilder.DropTable(
                name: "ChoiceGroupUsages");

            migrationBuilder.AlterColumn<int>(
                name: "StatusEffectType_StatusEffect",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SkillEffectType_SkillEffect_Skill",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SkillEffectType_SkillEffect",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SizeEffectType_SizeEffect_SizeToSet",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SizeEffectType_SizeEffect",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SizeEffectType_SizeBonus",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SavingThrowEffectType_SavingThrowEffect_Ability",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SavingThrowEffectType_SavingThrowEffect",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ResistanceEffectType_ResistanceEffect_DamageType",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ResistanceEffectType_ResistanceEffect",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProficiencyEffectType_ProficiencyEffect",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MovementEffectType_MovementEffect",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MovementCostEffectType_MovementCost_Multiplier",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HitpointEffectType_HitpointEffect",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DamageEffectType_DamageEffect_DamageType",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DamageEffectType_DamageEffect",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AttackRollEffectType_AttackRollEffect_Type",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AttackRollEffectType_AttackRollEffect_Source",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AttackRollEffectType_AttackRollEffect_Range",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AttackPerAttackActionEffectType_AttackPerActionEffect",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ActionEffectType_ActionEffect",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AbilityEffectType_AbilityEffect_Ability",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AbilityEffectType_AbilityEffect",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StatusEffectType_StatusEffect",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SkillEffectType_SkillEffect_Skill",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SkillEffectType_SkillEffect",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SizeEffectType_SizeEffect_SizeToSet",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SizeEffectType_SizeEffect",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SizeEffectType_SizeBonus",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SavingThrowEffectType_SavingThrowEffect_Ability",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SavingThrowEffectType_SavingThrowEffect",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ResistanceEffectType_ResistanceEffect_DamageType",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ResistanceEffectType_ResistanceEffect",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProficiencyEffectType_ProficiencyEffect",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MovementEffectType_MovementEffect",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MovementCostEffectType_MovementCost_Multiplier",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HitpointEffectType_HitpointEffect",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DamageEffectType_DamageEffect_DamageType",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DamageEffectType_DamageEffect",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AttackRollEffectType_AttackRollEffect_Type",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AttackRollEffectType_AttackRollEffect_Source",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AttackRollEffectType_AttackRollEffect_Range",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AttackPerAttackActionEffectType_AttackPerActionEffect",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ActionEffectType_ActionEffect",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AbilityEffectType_AbilityEffect_Ability",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AbilityEffectType_AbilityEffect",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);
        }
    }
}
