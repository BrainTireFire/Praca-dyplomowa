using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class walkazabstraktem2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "FK_EffectBlueprints_DiceSet_HealingEffectBlueprint_DiceSetId",
                table: "EffectBlueprints");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectBlueprints_DiceSet_HitpointEffectBlueprint_DiceSetId",
                table: "EffectBlueprints");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectBlueprints_DiceSet_InitiativeEffectBlueprint_DiceSetId",
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
                name: "IX_EffectBlueprints_HealingEffectBlueprint_DiceSetId",
                table: "EffectBlueprints");

            migrationBuilder.DropIndex(
                name: "IX_EffectBlueprints_HitpointEffectBlueprint_DiceSetId",
                table: "EffectBlueprints");

            migrationBuilder.DropIndex(
                name: "IX_EffectBlueprints_InitiativeEffectBlueprint_DiceSetId",
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

            migrationBuilder.DropColumn(
                name: "AbilityEffectInstance_DiceSetId",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "ActionEffectInstance_DiceSetId",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "AttackPerAttackActionEffectInstance_DiceSetId",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "AttackRollEffectInstance_DiceSetId",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "HitpointEffectInstance_DiceSetId",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "MovementEffectInstance_DiceSetId",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "SavingThrowEffectInstance_DiceSetId",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "SkillEffectInstance_DiceSetId",
                table: "EffectInstances");

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
                name: "DamageEffectBlueprint_DiceSetId",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "HealingEffectBlueprint_DiceSetId",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "HitpointEffectBlueprint_DiceSetId",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "InitiativeEffectBlueprint_DiceSetId",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "MagicEffectBlueprint_DiceSetId",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "MovementEffectBlueprint_DiceSetId",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "SavingThrowEffectBlueprint_DiceSetId",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "SkillEffectBlueprint_DiceSetId",
                table: "EffectBlueprints");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AbilityEffectInstance_DiceSetId",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ActionEffectInstance_DiceSetId",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AttackPerAttackActionEffectInstance_DiceSetId",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AttackRollEffectInstance_DiceSetId",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HitpointEffectInstance_DiceSetId",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MovementEffectInstance_DiceSetId",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SavingThrowEffectInstance_DiceSetId",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SkillEffectInstance_DiceSetId",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: true);

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
                name: "DamageEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HealingEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HitpointEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InitiativeEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MagicEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MovementEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SavingThrowEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SkillEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: true);

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
                name: "IX_EffectBlueprints_HealingEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                column: "HealingEffectBlueprint_DiceSetId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectBlueprints_HitpointEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                column: "HitpointEffectBlueprint_DiceSetId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectBlueprints_InitiativeEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                column: "InitiativeEffectBlueprint_DiceSetId");

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
                name: "FK_EffectBlueprints_DiceSet_InitiativeEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                column: "InitiativeEffectBlueprint_DiceSetId",
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
        }
    }
}
