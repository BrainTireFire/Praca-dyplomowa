using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class crazyStuff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EffectBlueprints_ItemFamilies_R_GrantsProficiencyInItemFamilyId",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "MaximumPreparedSpellsFormula",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "HitDie_d10",
                table: "ClassLevels");

            migrationBuilder.DropColumn(
                name: "HitDie_d100",
                table: "ClassLevels");

            migrationBuilder.DropColumn(
                name: "HitDie_d12",
                table: "ClassLevels");

            migrationBuilder.DropColumn(
                name: "HitDie_d20",
                table: "ClassLevels");

            migrationBuilder.DropColumn(
                name: "HitDie_d4",
                table: "ClassLevels");

            migrationBuilder.DropColumn(
                name: "HitDie_d6",
                table: "ClassLevels");

            migrationBuilder.DropColumn(
                name: "UsedHitDice_d10",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "UsedHitDice_d100",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "UsedHitDice_d12",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "UsedHitDice_d20",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "UsedHitDice_d4",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "UsedHitDice_d6",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "UsedHitDice_d8",
                table: "Characters");

            migrationBuilder.RenameColumn(
                name: "ProficiencyEffectType_ProficiencyEffect",
                table: "EffectInstances",
                newName: "SkillEffectInstance_DiceSetId");

            migrationBuilder.RenameColumn(
                name: "DiceSet_flat",
                table: "EffectInstances",
                newName: "SavingThrowEffectInstance_DiceSetId");

            migrationBuilder.RenameColumn(
                name: "DiceSet_d8",
                table: "EffectInstances",
                newName: "MovementEffectInstance_DiceSetId");

            migrationBuilder.RenameColumn(
                name: "DiceSet_d6",
                table: "EffectInstances",
                newName: "HitpointEffectInstance_DiceSetId");

            migrationBuilder.RenameColumn(
                name: "DiceSet_d4",
                table: "EffectInstances",
                newName: "DiceSetId");

            migrationBuilder.RenameColumn(
                name: "DiceSet_d20",
                table: "EffectInstances",
                newName: "AttackRollEffectInstance_DiceSetId");

            migrationBuilder.RenameColumn(
                name: "DiceSet_d12",
                table: "EffectInstances",
                newName: "AttackPerAttackActionEffectInstance_DiceSetId");

            migrationBuilder.RenameColumn(
                name: "DiceSet_d100",
                table: "EffectInstances",
                newName: "ActionEffectInstance_DiceSetId");

            migrationBuilder.RenameColumn(
                name: "DiceSet_d10",
                table: "EffectInstances",
                newName: "AbilityEffectInstance_DiceSetId");

            migrationBuilder.RenameColumn(
                name: "ResourceLevel",
                table: "EffectBlueprints",
                newName: "ResourceAmount");

            migrationBuilder.RenameColumn(
                name: "ProficiencyEffectType_ProficiencyEffect",
                table: "EffectBlueprints",
                newName: "SkillEffectBlueprint_DiceSetId");

            migrationBuilder.RenameColumn(
                name: "DiceSet_flat",
                table: "EffectBlueprints",
                newName: "SavingThrowEffectBlueprint_DiceSetId");

            migrationBuilder.RenameColumn(
                name: "DiceSet_d8",
                table: "EffectBlueprints",
                newName: "MovementEffectBlueprint_DiceSetId");

            migrationBuilder.RenameColumn(
                name: "DiceSet_d6",
                table: "EffectBlueprints",
                newName: "MagicEffectBlueprint_DiceSetId");

            migrationBuilder.RenameColumn(
                name: "DiceSet_d4",
                table: "EffectBlueprints",
                newName: "IniativeEffectBlueprint_DiceSetId");

            migrationBuilder.RenameColumn(
                name: "DiceSet_d20",
                table: "EffectBlueprints",
                newName: "HitpointEffectBlueprint_DiceSetId");

            migrationBuilder.RenameColumn(
                name: "DiceSet_d12",
                table: "EffectBlueprints",
                newName: "HealingEffectBlueprint_DiceSetId");

            migrationBuilder.RenameColumn(
                name: "DiceSet_d100",
                table: "EffectBlueprints",
                newName: "DiceSetId");

            migrationBuilder.RenameColumn(
                name: "DiceSet_d10",
                table: "EffectBlueprints",
                newName: "DamageEffectBlueprint_DiceSetId");

            migrationBuilder.RenameColumn(
                name: "HitDie_flat",
                table: "ClassLevels",
                newName: "HitPoints");

            migrationBuilder.RenameColumn(
                name: "HitDie_d8",
                table: "ClassLevels",
                newName: "HitDieId");

            migrationBuilder.RenameColumn(
                name: "UsedHitDice_flat",
                table: "Characters",
                newName: "UsedHitDiceId");

            migrationBuilder.AddColumn<int>(
                name: "DamageType",
                table: "Weapons",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DamageValueId",
                table: "Weapons",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "SavingThrowRoll",
                table: "Powers",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "SavingThrowBehaviour",
                table: "Powers",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "SavingThrow",
                table: "Powers",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "Range",
                table: "Powers",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "DifficultyClass",
                table: "Powers",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "AuraSize",
                table: "Powers",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "AreaSize",
                table: "Powers",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "AreaShape",
                table: "Powers",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Powers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "R_ClassForUpcastingId",
                table: "Powers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "UpcastByCharacterLevel",
                table: "Powers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UpcastByClassLevel",
                table: "Powers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UpcastByResourceLevel",
                table: "Powers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ActionEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ArmorClassEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AttackPerAttackActionEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AttackRollEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaximumPreparedSpellsFormulaId",
                table: "Classes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ChoiceGroups",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "DiceSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    d20 = table.Column<int>(type: "INTEGER", nullable: false),
                    d12 = table.Column<int>(type: "INTEGER", nullable: false),
                    d10 = table.Column<int>(type: "INTEGER", nullable: false),
                    d8 = table.Column<int>(type: "INTEGER", nullable: false),
                    d6 = table.Column<int>(type: "INTEGER", nullable: false),
                    d4 = table.Column<int>(type: "INTEGER", nullable: false),
                    d100 = table.Column<int>(type: "INTEGER", nullable: false),
                    flat = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiceSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdditionalValue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    R_LevelsInClassId = table.Column<int>(type: "INTEGER", nullable: true),
                    Ability = table.Column<int>(type: "INTEGER", nullable: false),
                    Skill = table.Column<int>(type: "INTEGER", nullable: false),
                    DiceSetId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdditionalValue_Classes_R_LevelsInClassId",
                        column: x => x.R_LevelsInClassId,
                        principalTable: "Classes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AdditionalValue_DiceSet_DiceSetId",
                        column: x => x.DiceSetId,
                        principalTable: "DiceSet",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Weapons_DamageValueId",
                table: "Weapons",
                column: "DamageValueId");

            migrationBuilder.CreateIndex(
                name: "IX_Powers_R_ClassForUpcastingId",
                table: "Powers",
                column: "R_ClassForUpcastingId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectInstances_AbilityEffectInstance_DiceSetId",
                table: "EffectInstances",
                column: "AbilityEffectInstance_DiceSetId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectInstances_ActionEffectInstance_DiceSetId",
                table: "EffectInstances",
                column: "ActionEffectInstance_DiceSetId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectInstances_AttackPerAttackActionEffectInstance_DiceSetId",
                table: "EffectInstances",
                column: "AttackPerAttackActionEffectInstance_DiceSetId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectInstances_AttackRollEffectInstance_DiceSetId",
                table: "EffectInstances",
                column: "AttackRollEffectInstance_DiceSetId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectInstances_DiceSetId",
                table: "EffectInstances",
                column: "DiceSetId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectInstances_HitpointEffectInstance_DiceSetId",
                table: "EffectInstances",
                column: "HitpointEffectInstance_DiceSetId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectInstances_MovementEffectInstance_DiceSetId",
                table: "EffectInstances",
                column: "MovementEffectInstance_DiceSetId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectInstances_SavingThrowEffectInstance_DiceSetId",
                table: "EffectInstances",
                column: "SavingThrowEffectInstance_DiceSetId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectInstances_SkillEffectInstance_DiceSetId",
                table: "EffectInstances",
                column: "SkillEffectInstance_DiceSetId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectBlueprints_ActionEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                column: "ActionEffectBlueprint_DiceSetId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectBlueprints_ArmorClassEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                column: "ArmorClassEffectBlueprint_DiceSetId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectBlueprints_AttackPerAttackActionEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                column: "AttackPerAttackActionEffectBlueprint_DiceSetId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectBlueprints_AttackRollEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                column: "AttackRollEffectBlueprint_DiceSetId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectBlueprints_DamageEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                column: "DamageEffectBlueprint_DiceSetId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectBlueprints_DiceSetId",
                table: "EffectBlueprints",
                column: "DiceSetId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectBlueprints_HealingEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                column: "HealingEffectBlueprint_DiceSetId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectBlueprints_HitpointEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                column: "HitpointEffectBlueprint_DiceSetId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectBlueprints_IniativeEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                column: "IniativeEffectBlueprint_DiceSetId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectBlueprints_MagicEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                column: "MagicEffectBlueprint_DiceSetId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectBlueprints_MovementEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                column: "MovementEffectBlueprint_DiceSetId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectBlueprints_SavingThrowEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                column: "SavingThrowEffectBlueprint_DiceSetId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectBlueprints_SkillEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                column: "SkillEffectBlueprint_DiceSetId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_MaximumPreparedSpellsFormulaId",
                table: "Classes",
                column: "MaximumPreparedSpellsFormulaId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassLevels_HitDieId",
                table: "ClassLevels",
                column: "HitDieId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_UsedHitDiceId",
                table: "Characters",
                column: "UsedHitDiceId");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalValue_DiceSetId",
                table: "AdditionalValue",
                column: "DiceSetId");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalValue_R_LevelsInClassId",
                table: "AdditionalValue",
                column: "R_LevelsInClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_DiceSet_UsedHitDiceId",
                table: "Characters",
                column: "UsedHitDiceId",
                principalTable: "DiceSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassLevels_DiceSet_HitDieId",
                table: "ClassLevels",
                column: "HitDieId",
                principalTable: "DiceSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_DiceSet_MaximumPreparedSpellsFormulaId",
                table: "Classes",
                column: "MaximumPreparedSpellsFormulaId",
                principalTable: "DiceSet",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EffectBlueprints_DiceSet_ActionEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                column: "ActionEffectBlueprint_DiceSetId",
                principalTable: "DiceSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EffectBlueprints_DiceSet_ArmorClassEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                column: "ArmorClassEffectBlueprint_DiceSetId",
                principalTable: "DiceSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EffectBlueprints_DiceSet_AttackPerAttackActionEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                column: "AttackPerAttackActionEffectBlueprint_DiceSetId",
                principalTable: "DiceSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EffectBlueprints_DiceSet_AttackRollEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                column: "AttackRollEffectBlueprint_DiceSetId",
                principalTable: "DiceSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EffectBlueprints_DiceSet_DamageEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                column: "DamageEffectBlueprint_DiceSetId",
                principalTable: "DiceSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EffectBlueprints_DiceSet_DiceSetId",
                table: "EffectBlueprints",
                column: "DiceSetId",
                principalTable: "DiceSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EffectBlueprints_DiceSet_HealingEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                column: "HealingEffectBlueprint_DiceSetId",
                principalTable: "DiceSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EffectBlueprints_DiceSet_HitpointEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                column: "HitpointEffectBlueprint_DiceSetId",
                principalTable: "DiceSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EffectBlueprints_DiceSet_IniativeEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                column: "IniativeEffectBlueprint_DiceSetId",
                principalTable: "DiceSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EffectBlueprints_DiceSet_MagicEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                column: "MagicEffectBlueprint_DiceSetId",
                principalTable: "DiceSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EffectBlueprints_DiceSet_MovementEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                column: "MovementEffectBlueprint_DiceSetId",
                principalTable: "DiceSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EffectBlueprints_DiceSet_SavingThrowEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                column: "SavingThrowEffectBlueprint_DiceSetId",
                principalTable: "DiceSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EffectBlueprints_DiceSet_SkillEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                column: "SkillEffectBlueprint_DiceSetId",
                principalTable: "DiceSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EffectBlueprints_ItemFamilies_R_GrantsProficiencyInItemFamilyId",
                table: "EffectBlueprints",
                column: "R_GrantsProficiencyInItemFamilyId",
                principalTable: "ItemFamilies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EffectInstances_DiceSet_AbilityEffectInstance_DiceSetId",
                table: "EffectInstances",
                column: "AbilityEffectInstance_DiceSetId",
                principalTable: "DiceSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EffectInstances_DiceSet_ActionEffectInstance_DiceSetId",
                table: "EffectInstances",
                column: "ActionEffectInstance_DiceSetId",
                principalTable: "DiceSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EffectInstances_DiceSet_AttackPerAttackActionEffectInstance_DiceSetId",
                table: "EffectInstances",
                column: "AttackPerAttackActionEffectInstance_DiceSetId",
                principalTable: "DiceSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EffectInstances_DiceSet_AttackRollEffectInstance_DiceSetId",
                table: "EffectInstances",
                column: "AttackRollEffectInstance_DiceSetId",
                principalTable: "DiceSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EffectInstances_DiceSet_DiceSetId",
                table: "EffectInstances",
                column: "DiceSetId",
                principalTable: "DiceSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EffectInstances_DiceSet_HitpointEffectInstance_DiceSetId",
                table: "EffectInstances",
                column: "HitpointEffectInstance_DiceSetId",
                principalTable: "DiceSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EffectInstances_DiceSet_MovementEffectInstance_DiceSetId",
                table: "EffectInstances",
                column: "MovementEffectInstance_DiceSetId",
                principalTable: "DiceSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EffectInstances_DiceSet_SavingThrowEffectInstance_DiceSetId",
                table: "EffectInstances",
                column: "SavingThrowEffectInstance_DiceSetId",
                principalTable: "DiceSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EffectInstances_DiceSet_SkillEffectInstance_DiceSetId",
                table: "EffectInstances",
                column: "SkillEffectInstance_DiceSetId",
                principalTable: "DiceSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Powers_Classes_R_ClassForUpcastingId",
                table: "Powers",
                column: "R_ClassForUpcastingId",
                principalTable: "Classes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Weapons_DiceSet_DamageValueId",
                table: "Weapons",
                column: "DamageValueId",
                principalTable: "DiceSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_DiceSet_UsedHitDiceId",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassLevels_DiceSet_HitDieId",
                table: "ClassLevels");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_DiceSet_MaximumPreparedSpellsFormulaId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectBlueprints_DiceSet_ActionEffectBlueprint_DiceSetId",
                table: "EffectBlueprints");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectBlueprints_DiceSet_ArmorClassEffectBlueprint_DiceSetId",
                table: "EffectBlueprints");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectBlueprints_DiceSet_AttackPerAttackActionEffectBlueprint_DiceSetId",
                table: "EffectBlueprints");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectBlueprints_DiceSet_AttackRollEffectBlueprint_DiceSetId",
                table: "EffectBlueprints");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectBlueprints_DiceSet_DamageEffectBlueprint_DiceSetId",
                table: "EffectBlueprints");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectBlueprints_DiceSet_DiceSetId",
                table: "EffectBlueprints");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectBlueprints_DiceSet_HealingEffectBlueprint_DiceSetId",
                table: "EffectBlueprints");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectBlueprints_DiceSet_HitpointEffectBlueprint_DiceSetId",
                table: "EffectBlueprints");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectBlueprints_DiceSet_IniativeEffectBlueprint_DiceSetId",
                table: "EffectBlueprints");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectBlueprints_DiceSet_MagicEffectBlueprint_DiceSetId",
                table: "EffectBlueprints");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectBlueprints_DiceSet_MovementEffectBlueprint_DiceSetId",
                table: "EffectBlueprints");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectBlueprints_DiceSet_SavingThrowEffectBlueprint_DiceSetId",
                table: "EffectBlueprints");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectBlueprints_DiceSet_SkillEffectBlueprint_DiceSetId",
                table: "EffectBlueprints");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectBlueprints_ItemFamilies_R_GrantsProficiencyInItemFamilyId",
                table: "EffectBlueprints");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectInstances_DiceSet_AbilityEffectInstance_DiceSetId",
                table: "EffectInstances");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectInstances_DiceSet_ActionEffectInstance_DiceSetId",
                table: "EffectInstances");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectInstances_DiceSet_AttackPerAttackActionEffectInstance_DiceSetId",
                table: "EffectInstances");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectInstances_DiceSet_AttackRollEffectInstance_DiceSetId",
                table: "EffectInstances");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectInstances_DiceSet_DiceSetId",
                table: "EffectInstances");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectInstances_DiceSet_HitpointEffectInstance_DiceSetId",
                table: "EffectInstances");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectInstances_DiceSet_MovementEffectInstance_DiceSetId",
                table: "EffectInstances");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectInstances_DiceSet_SavingThrowEffectInstance_DiceSetId",
                table: "EffectInstances");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectInstances_DiceSet_SkillEffectInstance_DiceSetId",
                table: "EffectInstances");

            migrationBuilder.DropForeignKey(
                name: "FK_Powers_Classes_R_ClassForUpcastingId",
                table: "Powers");

            migrationBuilder.DropForeignKey(
                name: "FK_Weapons_DiceSet_DamageValueId",
                table: "Weapons");

            migrationBuilder.DropTable(
                name: "AdditionalValue");

            migrationBuilder.DropTable(
                name: "DiceSet");

            migrationBuilder.DropIndex(
                name: "IX_Weapons_DamageValueId",
                table: "Weapons");

            migrationBuilder.DropIndex(
                name: "IX_Powers_R_ClassForUpcastingId",
                table: "Powers");

            migrationBuilder.DropIndex(
                name: "IX_EffectInstances_AbilityEffectInstance_DiceSetId",
                table: "EffectInstances");

            migrationBuilder.DropIndex(
                name: "IX_EffectInstances_ActionEffectInstance_DiceSetId",
                table: "EffectInstances");

            migrationBuilder.DropIndex(
                name: "IX_EffectInstances_AttackPerAttackActionEffectInstance_DiceSetId",
                table: "EffectInstances");

            migrationBuilder.DropIndex(
                name: "IX_EffectInstances_AttackRollEffectInstance_DiceSetId",
                table: "EffectInstances");

            migrationBuilder.DropIndex(
                name: "IX_EffectInstances_DiceSetId",
                table: "EffectInstances");

            migrationBuilder.DropIndex(
                name: "IX_EffectInstances_HitpointEffectInstance_DiceSetId",
                table: "EffectInstances");

            migrationBuilder.DropIndex(
                name: "IX_EffectInstances_MovementEffectInstance_DiceSetId",
                table: "EffectInstances");

            migrationBuilder.DropIndex(
                name: "IX_EffectInstances_SavingThrowEffectInstance_DiceSetId",
                table: "EffectInstances");

            migrationBuilder.DropIndex(
                name: "IX_EffectInstances_SkillEffectInstance_DiceSetId",
                table: "EffectInstances");

            migrationBuilder.DropIndex(
                name: "IX_EffectBlueprints_ActionEffectBlueprint_DiceSetId",
                table: "EffectBlueprints");

            migrationBuilder.DropIndex(
                name: "IX_EffectBlueprints_ArmorClassEffectBlueprint_DiceSetId",
                table: "EffectBlueprints");

            migrationBuilder.DropIndex(
                name: "IX_EffectBlueprints_AttackPerAttackActionEffectBlueprint_DiceSetId",
                table: "EffectBlueprints");

            migrationBuilder.DropIndex(
                name: "IX_EffectBlueprints_AttackRollEffectBlueprint_DiceSetId",
                table: "EffectBlueprints");

            migrationBuilder.DropIndex(
                name: "IX_EffectBlueprints_DamageEffectBlueprint_DiceSetId",
                table: "EffectBlueprints");

            migrationBuilder.DropIndex(
                name: "IX_EffectBlueprints_DiceSetId",
                table: "EffectBlueprints");

            migrationBuilder.DropIndex(
                name: "IX_EffectBlueprints_HealingEffectBlueprint_DiceSetId",
                table: "EffectBlueprints");

            migrationBuilder.DropIndex(
                name: "IX_EffectBlueprints_HitpointEffectBlueprint_DiceSetId",
                table: "EffectBlueprints");

            migrationBuilder.DropIndex(
                name: "IX_EffectBlueprints_IniativeEffectBlueprint_DiceSetId",
                table: "EffectBlueprints");

            migrationBuilder.DropIndex(
                name: "IX_EffectBlueprints_MagicEffectBlueprint_DiceSetId",
                table: "EffectBlueprints");

            migrationBuilder.DropIndex(
                name: "IX_EffectBlueprints_MovementEffectBlueprint_DiceSetId",
                table: "EffectBlueprints");

            migrationBuilder.DropIndex(
                name: "IX_EffectBlueprints_SavingThrowEffectBlueprint_DiceSetId",
                table: "EffectBlueprints");

            migrationBuilder.DropIndex(
                name: "IX_EffectBlueprints_SkillEffectBlueprint_DiceSetId",
                table: "EffectBlueprints");

            migrationBuilder.DropIndex(
                name: "IX_Classes_MaximumPreparedSpellsFormulaId",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_ClassLevels_HitDieId",
                table: "ClassLevels");

            migrationBuilder.DropIndex(
                name: "IX_Characters_UsedHitDiceId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "DamageType",
                table: "Weapons");

            migrationBuilder.DropColumn(
                name: "DamageValueId",
                table: "Weapons");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Powers");

            migrationBuilder.DropColumn(
                name: "R_ClassForUpcastingId",
                table: "Powers");

            migrationBuilder.DropColumn(
                name: "UpcastByCharacterLevel",
                table: "Powers");

            migrationBuilder.DropColumn(
                name: "UpcastByClassLevel",
                table: "Powers");

            migrationBuilder.DropColumn(
                name: "UpcastByResourceLevel",
                table: "Powers");

            migrationBuilder.DropColumn(
                name: "ActionEffectBlueprint_DiceSetId",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "ArmorClassEffectBlueprint_DiceSetId",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "AttackPerAttackActionEffectBlueprint_DiceSetId",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "AttackRollEffectBlueprint_DiceSetId",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "MaximumPreparedSpellsFormulaId",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ChoiceGroups");

            migrationBuilder.RenameColumn(
                name: "SkillEffectInstance_DiceSetId",
                table: "EffectInstances",
                newName: "ProficiencyEffectType_ProficiencyEffect");

            migrationBuilder.RenameColumn(
                name: "SavingThrowEffectInstance_DiceSetId",
                table: "EffectInstances",
                newName: "DiceSet_flat");

            migrationBuilder.RenameColumn(
                name: "MovementEffectInstance_DiceSetId",
                table: "EffectInstances",
                newName: "DiceSet_d8");

            migrationBuilder.RenameColumn(
                name: "HitpointEffectInstance_DiceSetId",
                table: "EffectInstances",
                newName: "DiceSet_d6");

            migrationBuilder.RenameColumn(
                name: "DiceSetId",
                table: "EffectInstances",
                newName: "DiceSet_d4");

            migrationBuilder.RenameColumn(
                name: "AttackRollEffectInstance_DiceSetId",
                table: "EffectInstances",
                newName: "DiceSet_d20");

            migrationBuilder.RenameColumn(
                name: "AttackPerAttackActionEffectInstance_DiceSetId",
                table: "EffectInstances",
                newName: "DiceSet_d12");

            migrationBuilder.RenameColumn(
                name: "ActionEffectInstance_DiceSetId",
                table: "EffectInstances",
                newName: "DiceSet_d100");

            migrationBuilder.RenameColumn(
                name: "AbilityEffectInstance_DiceSetId",
                table: "EffectInstances",
                newName: "DiceSet_d10");

            migrationBuilder.RenameColumn(
                name: "SkillEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                newName: "ProficiencyEffectType_ProficiencyEffect");

            migrationBuilder.RenameColumn(
                name: "SavingThrowEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                newName: "DiceSet_flat");

            migrationBuilder.RenameColumn(
                name: "ResourceAmount",
                table: "EffectBlueprints",
                newName: "ResourceLevel");

            migrationBuilder.RenameColumn(
                name: "MovementEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                newName: "DiceSet_d8");

            migrationBuilder.RenameColumn(
                name: "MagicEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                newName: "DiceSet_d6");

            migrationBuilder.RenameColumn(
                name: "IniativeEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                newName: "DiceSet_d4");

            migrationBuilder.RenameColumn(
                name: "HitpointEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                newName: "DiceSet_d20");

            migrationBuilder.RenameColumn(
                name: "HealingEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                newName: "DiceSet_d12");

            migrationBuilder.RenameColumn(
                name: "DiceSetId",
                table: "EffectBlueprints",
                newName: "DiceSet_d100");

            migrationBuilder.RenameColumn(
                name: "DamageEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                newName: "DiceSet_d10");

            migrationBuilder.RenameColumn(
                name: "HitPoints",
                table: "ClassLevels",
                newName: "HitDie_flat");

            migrationBuilder.RenameColumn(
                name: "HitDieId",
                table: "ClassLevels",
                newName: "HitDie_d8");

            migrationBuilder.RenameColumn(
                name: "UsedHitDiceId",
                table: "Characters",
                newName: "UsedHitDice_flat");

            migrationBuilder.AlterColumn<int>(
                name: "SavingThrowRoll",
                table: "Powers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SavingThrowBehaviour",
                table: "Powers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SavingThrow",
                table: "Powers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Range",
                table: "Powers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DifficultyClass",
                table: "Powers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AuraSize",
                table: "Powers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AreaSize",
                table: "Powers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AreaShape",
                table: "Powers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaximumPreparedSpellsFormula",
                table: "Classes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HitDie_d10",
                table: "ClassLevels",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HitDie_d100",
                table: "ClassLevels",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HitDie_d12",
                table: "ClassLevels",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HitDie_d20",
                table: "ClassLevels",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HitDie_d4",
                table: "ClassLevels",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HitDie_d6",
                table: "ClassLevels",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsedHitDice_d10",
                table: "Characters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsedHitDice_d100",
                table: "Characters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsedHitDice_d12",
                table: "Characters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsedHitDice_d20",
                table: "Characters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsedHitDice_d4",
                table: "Characters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsedHitDice_d6",
                table: "Characters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsedHitDice_d8",
                table: "Characters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_EffectBlueprints_ItemFamilies_R_GrantsProficiencyInItemFamilyId",
                table: "EffectBlueprints",
                column: "R_GrantsProficiencyInItemFamilyId",
                principalTable: "ItemFamilies",
                principalColumn: "Id");
        }
    }
}
