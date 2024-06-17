using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Identity.Migrations
{
    /// <inheritdoc />
    public partial class diceSetInEffectTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AbilityEffectType_AbilityEffect_Value",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "ArmorClassEffectType_ArmorClassEffect_Value",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "AttackRollEffectType_AttackRollEffect_Value",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "DamageEffectType_DamageEffect_Value",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "HealingEffectType_HealingEffect_Value",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "HitpointEffectType_HitpointEffect_Value",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "InitiativeEffectType_InitiativeEffect_Value",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "MovementEffectType_MovementEffect_Value",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "SavingThrowEffectType_SavingThrowEffect_Value",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "SkillEffectType_SkillEffect_Value",
                table: "EffectBlueprints");

            migrationBuilder.RenameColumn(
                name: "SizeEffectType_SizeEffect_Value",
                table: "EffectBlueprints",
                newName: "SkillEffectType_SkillEffect_Value_flat");

            migrationBuilder.RenameColumn(
                name: "MagicItemEffectType_MagicItemEffect_Value",
                table: "EffectBlueprints",
                newName: "SkillEffectType_SkillEffect_Value_d8");

            migrationBuilder.RenameColumn(
                name: "AttackPerAttackActionEffectType_AttackPerActionEffect_Value",
                table: "EffectBlueprints",
                newName: "SkillEffectType_SkillEffect_Value_d6");

            migrationBuilder.RenameColumn(
                name: "ActionEffectType_ActionEffect_Value",
                table: "EffectBlueprints",
                newName: "SkillEffectType_SkillEffect_Value_d4");

            migrationBuilder.AddColumn<int>(
                name: "AbilityEffectType_AbilityEffect_Value_d10",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AbilityEffectType_AbilityEffect_Value_d100",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AbilityEffectType_AbilityEffect_Value_d12",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AbilityEffectType_AbilityEffect_Value_d20",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AbilityEffectType_AbilityEffect_Value_d4",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AbilityEffectType_AbilityEffect_Value_d6",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AbilityEffectType_AbilityEffect_Value_d8",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AbilityEffectType_AbilityEffect_Value_flat",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ActionEffectType_ActionEffect_Value_d10",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ActionEffectType_ActionEffect_Value_d100",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ActionEffectType_ActionEffect_Value_d12",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ActionEffectType_ActionEffect_Value_d20",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ActionEffectType_ActionEffect_Value_d4",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ActionEffectType_ActionEffect_Value_d6",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ActionEffectType_ActionEffect_Value_d8",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ActionEffectType_ActionEffect_Value_flat",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ArmorClassEffectType_ArmorClassEffect_Value_d10",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ArmorClassEffectType_ArmorClassEffect_Value_d100",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ArmorClassEffectType_ArmorClassEffect_Value_d12",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ArmorClassEffectType_ArmorClassEffect_Value_d20",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ArmorClassEffectType_ArmorClassEffect_Value_d4",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ArmorClassEffectType_ArmorClassEffect_Value_d6",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ArmorClassEffectType_ArmorClassEffect_Value_d8",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ArmorClassEffectType_ArmorClassEffect_Value_flat",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AttackPerAttackActionEffectType_AttackPerActionEffect_Value_d10",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AttackPerAttackActionEffectType_AttackPerActionEffect_Value_d100",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AttackPerAttackActionEffectType_AttackPerActionEffect_Value_d12",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AttackPerAttackActionEffectType_AttackPerActionEffect_Value_d20",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AttackPerAttackActionEffectType_AttackPerActionEffect_Value_d4",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AttackPerAttackActionEffectType_AttackPerActionEffect_Value_d6",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AttackPerAttackActionEffectType_AttackPerActionEffect_Value_d8",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AttackPerAttackActionEffectType_AttackPerActionEffect_Value_flat",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AttackRollEffectType_AttackRollEffect_Value_d10",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AttackRollEffectType_AttackRollEffect_Value_d100",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AttackRollEffectType_AttackRollEffect_Value_d12",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AttackRollEffectType_AttackRollEffect_Value_d20",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AttackRollEffectType_AttackRollEffect_Value_d4",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AttackRollEffectType_AttackRollEffect_Value_d6",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AttackRollEffectType_AttackRollEffect_Value_d8",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AttackRollEffectType_AttackRollEffect_Value_flat",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DamageEffectType_DamageEffect_Value_d10",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DamageEffectType_DamageEffect_Value_d100",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DamageEffectType_DamageEffect_Value_d12",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DamageEffectType_DamageEffect_Value_d20",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DamageEffectType_DamageEffect_Value_d4",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DamageEffectType_DamageEffect_Value_d6",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DamageEffectType_DamageEffect_Value_d8",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DamageEffectType_DamageEffect_Value_flat",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HealingEffectType_HealingEffect_Value_d10",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HealingEffectType_HealingEffect_Value_d100",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HealingEffectType_HealingEffect_Value_d12",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HealingEffectType_HealingEffect_Value_d20",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HealingEffectType_HealingEffect_Value_d4",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HealingEffectType_HealingEffect_Value_d6",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HealingEffectType_HealingEffect_Value_d8",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HealingEffectType_HealingEffect_Value_flat",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HitpointEffectType_HitpointEffect_Value_d10",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HitpointEffectType_HitpointEffect_Value_d100",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HitpointEffectType_HitpointEffect_Value_d12",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HitpointEffectType_HitpointEffect_Value_d20",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HitpointEffectType_HitpointEffect_Value_d4",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HitpointEffectType_HitpointEffect_Value_d6",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HitpointEffectType_HitpointEffect_Value_d8",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HitpointEffectType_HitpointEffect_Value_flat",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InitiativeEffectType_InitiativeEffect_Value_d10",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InitiativeEffectType_InitiativeEffect_Value_d100",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InitiativeEffectType_InitiativeEffect_Value_d12",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InitiativeEffectType_InitiativeEffect_Value_d20",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InitiativeEffectType_InitiativeEffect_Value_d4",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InitiativeEffectType_InitiativeEffect_Value_d6",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InitiativeEffectType_InitiativeEffect_Value_d8",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InitiativeEffectType_InitiativeEffect_Value_flat",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MagicItemEffectType_MagicItemEffect_Value_d10",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MagicItemEffectType_MagicItemEffect_Value_d100",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MagicItemEffectType_MagicItemEffect_Value_d12",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MagicItemEffectType_MagicItemEffect_Value_d20",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MagicItemEffectType_MagicItemEffect_Value_d4",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MagicItemEffectType_MagicItemEffect_Value_d6",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MagicItemEffectType_MagicItemEffect_Value_d8",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MagicItemEffectType_MagicItemEffect_Value_flat",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MovementEffectType_MovementEffect_Value_d10",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MovementEffectType_MovementEffect_Value_d100",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MovementEffectType_MovementEffect_Value_d12",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MovementEffectType_MovementEffect_Value_d20",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MovementEffectType_MovementEffect_Value_d4",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MovementEffectType_MovementEffect_Value_d6",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MovementEffectType_MovementEffect_Value_d8",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MovementEffectType_MovementEffect_Value_flat",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SavingThrowEffectType_SavingThrowEffect_Value_d10",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SavingThrowEffectType_SavingThrowEffect_Value_d100",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SavingThrowEffectType_SavingThrowEffect_Value_d12",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SavingThrowEffectType_SavingThrowEffect_Value_d20",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SavingThrowEffectType_SavingThrowEffect_Value_d4",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SavingThrowEffectType_SavingThrowEffect_Value_d6",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SavingThrowEffectType_SavingThrowEffect_Value_d8",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SavingThrowEffectType_SavingThrowEffect_Value_flat",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SizeEffectType_SizeEffect_Value_d10",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SizeEffectType_SizeEffect_Value_d100",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SizeEffectType_SizeEffect_Value_d12",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SizeEffectType_SizeEffect_Value_d20",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SizeEffectType_SizeEffect_Value_d4",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SizeEffectType_SizeEffect_Value_d6",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SizeEffectType_SizeEffect_Value_d8",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SizeEffectType_SizeEffect_Value_flat",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SkillEffectType_SkillEffect_Value_d10",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SkillEffectType_SkillEffect_Value_d100",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SkillEffectType_SkillEffect_Value_d12",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SkillEffectType_SkillEffect_Value_d20",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AbilityEffectType_AbilityEffect_Value_d10",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "AbilityEffectType_AbilityEffect_Value_d100",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "AbilityEffectType_AbilityEffect_Value_d12",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "AbilityEffectType_AbilityEffect_Value_d20",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "AbilityEffectType_AbilityEffect_Value_d4",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "AbilityEffectType_AbilityEffect_Value_d6",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "AbilityEffectType_AbilityEffect_Value_d8",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "AbilityEffectType_AbilityEffect_Value_flat",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "ActionEffectType_ActionEffect_Value_d10",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "ActionEffectType_ActionEffect_Value_d100",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "ActionEffectType_ActionEffect_Value_d12",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "ActionEffectType_ActionEffect_Value_d20",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "ActionEffectType_ActionEffect_Value_d4",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "ActionEffectType_ActionEffect_Value_d6",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "ActionEffectType_ActionEffect_Value_d8",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "ActionEffectType_ActionEffect_Value_flat",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "ArmorClassEffectType_ArmorClassEffect_Value_d10",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "ArmorClassEffectType_ArmorClassEffect_Value_d100",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "ArmorClassEffectType_ArmorClassEffect_Value_d12",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "ArmorClassEffectType_ArmorClassEffect_Value_d20",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "ArmorClassEffectType_ArmorClassEffect_Value_d4",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "ArmorClassEffectType_ArmorClassEffect_Value_d6",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "ArmorClassEffectType_ArmorClassEffect_Value_d8",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "ArmorClassEffectType_ArmorClassEffect_Value_flat",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "AttackPerAttackActionEffectType_AttackPerActionEffect_Value_d10",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "AttackPerAttackActionEffectType_AttackPerActionEffect_Value_d100",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "AttackPerAttackActionEffectType_AttackPerActionEffect_Value_d12",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "AttackPerAttackActionEffectType_AttackPerActionEffect_Value_d20",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "AttackPerAttackActionEffectType_AttackPerActionEffect_Value_d4",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "AttackPerAttackActionEffectType_AttackPerActionEffect_Value_d6",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "AttackPerAttackActionEffectType_AttackPerActionEffect_Value_d8",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "AttackPerAttackActionEffectType_AttackPerActionEffect_Value_flat",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "AttackRollEffectType_AttackRollEffect_Value_d10",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "AttackRollEffectType_AttackRollEffect_Value_d100",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "AttackRollEffectType_AttackRollEffect_Value_d12",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "AttackRollEffectType_AttackRollEffect_Value_d20",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "AttackRollEffectType_AttackRollEffect_Value_d4",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "AttackRollEffectType_AttackRollEffect_Value_d6",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "AttackRollEffectType_AttackRollEffect_Value_d8",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "AttackRollEffectType_AttackRollEffect_Value_flat",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "DamageEffectType_DamageEffect_Value_d10",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "DamageEffectType_DamageEffect_Value_d100",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "DamageEffectType_DamageEffect_Value_d12",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "DamageEffectType_DamageEffect_Value_d20",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "DamageEffectType_DamageEffect_Value_d4",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "DamageEffectType_DamageEffect_Value_d6",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "DamageEffectType_DamageEffect_Value_d8",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "DamageEffectType_DamageEffect_Value_flat",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "HealingEffectType_HealingEffect_Value_d10",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "HealingEffectType_HealingEffect_Value_d100",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "HealingEffectType_HealingEffect_Value_d12",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "HealingEffectType_HealingEffect_Value_d20",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "HealingEffectType_HealingEffect_Value_d4",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "HealingEffectType_HealingEffect_Value_d6",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "HealingEffectType_HealingEffect_Value_d8",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "HealingEffectType_HealingEffect_Value_flat",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "HitpointEffectType_HitpointEffect_Value_d10",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "HitpointEffectType_HitpointEffect_Value_d100",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "HitpointEffectType_HitpointEffect_Value_d12",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "HitpointEffectType_HitpointEffect_Value_d20",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "HitpointEffectType_HitpointEffect_Value_d4",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "HitpointEffectType_HitpointEffect_Value_d6",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "HitpointEffectType_HitpointEffect_Value_d8",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "HitpointEffectType_HitpointEffect_Value_flat",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "InitiativeEffectType_InitiativeEffect_Value_d10",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "InitiativeEffectType_InitiativeEffect_Value_d100",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "InitiativeEffectType_InitiativeEffect_Value_d12",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "InitiativeEffectType_InitiativeEffect_Value_d20",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "InitiativeEffectType_InitiativeEffect_Value_d4",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "InitiativeEffectType_InitiativeEffect_Value_d6",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "InitiativeEffectType_InitiativeEffect_Value_d8",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "InitiativeEffectType_InitiativeEffect_Value_flat",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "MagicItemEffectType_MagicItemEffect_Value_d10",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "MagicItemEffectType_MagicItemEffect_Value_d100",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "MagicItemEffectType_MagicItemEffect_Value_d12",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "MagicItemEffectType_MagicItemEffect_Value_d20",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "MagicItemEffectType_MagicItemEffect_Value_d4",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "MagicItemEffectType_MagicItemEffect_Value_d6",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "MagicItemEffectType_MagicItemEffect_Value_d8",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "MagicItemEffectType_MagicItemEffect_Value_flat",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "MovementEffectType_MovementEffect_Value_d10",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "MovementEffectType_MovementEffect_Value_d100",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "MovementEffectType_MovementEffect_Value_d12",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "MovementEffectType_MovementEffect_Value_d20",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "MovementEffectType_MovementEffect_Value_d4",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "MovementEffectType_MovementEffect_Value_d6",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "MovementEffectType_MovementEffect_Value_d8",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "MovementEffectType_MovementEffect_Value_flat",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "SavingThrowEffectType_SavingThrowEffect_Value_d10",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "SavingThrowEffectType_SavingThrowEffect_Value_d100",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "SavingThrowEffectType_SavingThrowEffect_Value_d12",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "SavingThrowEffectType_SavingThrowEffect_Value_d20",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "SavingThrowEffectType_SavingThrowEffect_Value_d4",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "SavingThrowEffectType_SavingThrowEffect_Value_d6",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "SavingThrowEffectType_SavingThrowEffect_Value_d8",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "SavingThrowEffectType_SavingThrowEffect_Value_flat",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "SizeEffectType_SizeEffect_Value_d10",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "SizeEffectType_SizeEffect_Value_d100",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "SizeEffectType_SizeEffect_Value_d12",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "SizeEffectType_SizeEffect_Value_d20",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "SizeEffectType_SizeEffect_Value_d4",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "SizeEffectType_SizeEffect_Value_d6",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "SizeEffectType_SizeEffect_Value_d8",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "SizeEffectType_SizeEffect_Value_flat",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "SkillEffectType_SkillEffect_Value_d10",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "SkillEffectType_SkillEffect_Value_d100",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "SkillEffectType_SkillEffect_Value_d12",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "SkillEffectType_SkillEffect_Value_d20",
                table: "EffectBlueprints");

            migrationBuilder.RenameColumn(
                name: "SkillEffectType_SkillEffect_Value_flat",
                table: "EffectBlueprints",
                newName: "SizeEffectType_SizeEffect_Value");

            migrationBuilder.RenameColumn(
                name: "SkillEffectType_SkillEffect_Value_d8",
                table: "EffectBlueprints",
                newName: "MagicItemEffectType_MagicItemEffect_Value");

            migrationBuilder.RenameColumn(
                name: "SkillEffectType_SkillEffect_Value_d6",
                table: "EffectBlueprints",
                newName: "AttackPerAttackActionEffectType_AttackPerActionEffect_Value");

            migrationBuilder.RenameColumn(
                name: "SkillEffectType_SkillEffect_Value_d4",
                table: "EffectBlueprints",
                newName: "ActionEffectType_ActionEffect_Value");

            migrationBuilder.AddColumn<string>(
                name: "AbilityEffectType_AbilityEffect_Value",
                table: "EffectBlueprints",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArmorClassEffectType_ArmorClassEffect_Value",
                table: "EffectBlueprints",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AttackRollEffectType_AttackRollEffect_Value",
                table: "EffectBlueprints",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DamageEffectType_DamageEffect_Value",
                table: "EffectBlueprints",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HealingEffectType_HealingEffect_Value",
                table: "EffectBlueprints",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HitpointEffectType_HitpointEffect_Value",
                table: "EffectBlueprints",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InitiativeEffectType_InitiativeEffect_Value",
                table: "EffectBlueprints",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MovementEffectType_MovementEffect_Value",
                table: "EffectBlueprints",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SavingThrowEffectType_SavingThrowEffect_Value",
                table: "EffectBlueprints",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SkillEffectType_SkillEffect_Value",
                table: "EffectBlueprints",
                type: "TEXT",
                nullable: true);
        }
    }
}
