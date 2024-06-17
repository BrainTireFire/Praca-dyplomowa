using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Identity.Migrations
{
    /// <inheritdoc />
    public partial class complexTypesInEffectInstance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttackRollEffect_Value",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "DamageEffect_Value",
                table: "EffectInstances");

            migrationBuilder.RenameColumn(
                name: "StatusEffect",
                table: "EffectInstances",
                newName: "StatusEffectType_StatusEffect");

            migrationBuilder.RenameColumn(
                name: "SkillEffect_Skill",
                table: "EffectInstances",
                newName: "SkillEffectType_SkillEffect_Skill");

            migrationBuilder.RenameColumn(
                name: "SkillEffect",
                table: "EffectInstances",
                newName: "SkillEffectType_SkillEffect");

            migrationBuilder.RenameColumn(
                name: "SizeEffect_SizeToSet",
                table: "EffectInstances",
                newName: "SizeEffectType_SizeEffect_SizeToSet");

            migrationBuilder.RenameColumn(
                name: "SizeEffect",
                table: "EffectInstances",
                newName: "SizeEffectType_SizeEffect");

            migrationBuilder.RenameColumn(
                name: "SavingThrowEffect_Ability",
                table: "EffectInstances",
                newName: "SavingThrowEffectType_SavingThrowEffect_Ability");

            migrationBuilder.RenameColumn(
                name: "SavingThrowEffect",
                table: "EffectInstances",
                newName: "SavingThrowEffectType_SavingThrowEffect");

            migrationBuilder.RenameColumn(
                name: "ResistanceEffect_DamageType",
                table: "EffectInstances",
                newName: "ResistanceEffectType_ResistanceEffect_DamageType");

            migrationBuilder.RenameColumn(
                name: "ResistanceEffect",
                table: "EffectInstances",
                newName: "ResistanceEffectType_ResistanceEffect");

            migrationBuilder.RenameColumn(
                name: "ProficiencyEffect",
                table: "EffectInstances",
                newName: "ProficiencyEffectType_ProficiencyEffect");

            migrationBuilder.RenameColumn(
                name: "MovementEffect",
                table: "EffectInstances",
                newName: "MovementEffectType_MovementEffect");

            migrationBuilder.RenameColumn(
                name: "MovementCost_Multiplier",
                table: "EffectInstances",
                newName: "MovementCostEffectType_MovementCost_Multiplier");

            migrationBuilder.RenameColumn(
                name: "HitpointEffect",
                table: "EffectInstances",
                newName: "HitpointEffectType_HitpointEffect");

            migrationBuilder.RenameColumn(
                name: "DamageEffect_DamageType",
                table: "EffectInstances",
                newName: "DamageEffectType_DamageEffect_DamageType");

            migrationBuilder.RenameColumn(
                name: "DamageEffect",
                table: "EffectInstances",
                newName: "DamageEffectType_DamageEffect");

            migrationBuilder.RenameColumn(
                name: "AttackRollEffect_Type",
                table: "EffectInstances",
                newName: "AttackRollEffectType_AttackRollEffect_Type");

            migrationBuilder.RenameColumn(
                name: "AttackRollEffect_Source",
                table: "EffectInstances",
                newName: "AttackRollEffectType_AttackRollEffect_Source");

            migrationBuilder.RenameColumn(
                name: "AttackRollEffect_Range",
                table: "EffectInstances",
                newName: "AttackRollEffectType_AttackRollEffect_Range");

            migrationBuilder.RenameColumn(
                name: "AttackPerActionEffect",
                table: "EffectInstances",
                newName: "AttackPerAttackActionEffectType_AttackPerActionEffect");

            migrationBuilder.RenameColumn(
                name: "ActionEffect",
                table: "EffectInstances",
                newName: "ActionEffectType_ActionEffect");

            migrationBuilder.RenameColumn(
                name: "AbilityEffect_Ability",
                table: "EffectInstances",
                newName: "AbilityEffectType_AbilityEffect_Ability");

            migrationBuilder.RenameColumn(
                name: "AbilityEffect",
                table: "EffectInstances",
                newName: "AbilityEffectType_AbilityEffect");

            migrationBuilder.RenameColumn(
                name: "SkillEffect_Value",
                table: "EffectInstances",
                newName: "SkillEffectType_SkillEffect_Value_flat");

            migrationBuilder.RenameColumn(
                name: "SizeEffect_Value",
                table: "EffectInstances",
                newName: "SkillEffectType_SkillEffect_Value_d8");

            migrationBuilder.RenameColumn(
                name: "SavingThrowEffect_Value",
                table: "EffectInstances",
                newName: "SkillEffectType_SkillEffect_Value_d6");

            migrationBuilder.RenameColumn(
                name: "MovementEffect_Value",
                table: "EffectInstances",
                newName: "SkillEffectType_SkillEffect_Value_d4");

            migrationBuilder.RenameColumn(
                name: "MagicItemEffect_Value",
                table: "EffectInstances",
                newName: "SkillEffectType_SkillEffect_Value_d20");

            migrationBuilder.RenameColumn(
                name: "InitiativeEffect_Value",
                table: "EffectInstances",
                newName: "SkillEffectType_SkillEffect_Value_d12");

            migrationBuilder.RenameColumn(
                name: "HitpointEffect_Value",
                table: "EffectInstances",
                newName: "SkillEffectType_SkillEffect_Value_d100");

            migrationBuilder.RenameColumn(
                name: "HealingEffec_Value",
                table: "EffectInstances",
                newName: "SkillEffectType_SkillEffect_Value_d10");

            migrationBuilder.RenameColumn(
                name: "AttackPerActionEffect_Value",
                table: "EffectInstances",
                newName: "SizeEffectType_SizeEffect_Value_flat");

            migrationBuilder.RenameColumn(
                name: "ArmorClassEffect_Value",
                table: "EffectInstances",
                newName: "SizeEffectType_SizeEffect_Value_d8");

            migrationBuilder.RenameColumn(
                name: "ActionEffect_Value",
                table: "EffectInstances",
                newName: "SizeEffectType_SizeEffect_Value_d6");

            migrationBuilder.RenameColumn(
                name: "AbilityEffect_Value",
                table: "EffectInstances",
                newName: "SizeEffectType_SizeEffect_Value_d4");

            migrationBuilder.AddColumn<int>(
                name: "AbilityEffectType_AbilityEffect_Value_d10",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AbilityEffectType_AbilityEffect_Value_d100",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AbilityEffectType_AbilityEffect_Value_d12",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AbilityEffectType_AbilityEffect_Value_d20",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AbilityEffectType_AbilityEffect_Value_d4",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AbilityEffectType_AbilityEffect_Value_d6",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AbilityEffectType_AbilityEffect_Value_d8",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AbilityEffectType_AbilityEffect_Value_flat",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ActionEffectType_ActionEffect_Value_d10",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ActionEffectType_ActionEffect_Value_d100",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ActionEffectType_ActionEffect_Value_d12",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ActionEffectType_ActionEffect_Value_d20",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ActionEffectType_ActionEffect_Value_d4",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ActionEffectType_ActionEffect_Value_d6",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ActionEffectType_ActionEffect_Value_d8",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ActionEffectType_ActionEffect_Value_flat",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ArmorClassEffectType_ArmorClassEffect_Value_d10",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ArmorClassEffectType_ArmorClassEffect_Value_d100",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ArmorClassEffectType_ArmorClassEffect_Value_d12",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ArmorClassEffectType_ArmorClassEffect_Value_d20",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ArmorClassEffectType_ArmorClassEffect_Value_d4",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ArmorClassEffectType_ArmorClassEffect_Value_d6",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ArmorClassEffectType_ArmorClassEffect_Value_d8",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ArmorClassEffectType_ArmorClassEffect_Value_flat",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AttackPerAttackActionEffectType_AttackPerActionEffect_Value_d10",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AttackPerAttackActionEffectType_AttackPerActionEffect_Value_d100",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AttackPerAttackActionEffectType_AttackPerActionEffect_Value_d12",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AttackPerAttackActionEffectType_AttackPerActionEffect_Value_d20",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AttackPerAttackActionEffectType_AttackPerActionEffect_Value_d4",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AttackPerAttackActionEffectType_AttackPerActionEffect_Value_d6",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AttackPerAttackActionEffectType_AttackPerActionEffect_Value_d8",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AttackPerAttackActionEffectType_AttackPerActionEffect_Value_flat",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AttackRollEffectType_AttackRollEffect_Value_d10",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AttackRollEffectType_AttackRollEffect_Value_d100",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AttackRollEffectType_AttackRollEffect_Value_d12",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AttackRollEffectType_AttackRollEffect_Value_d20",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AttackRollEffectType_AttackRollEffect_Value_d4",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AttackRollEffectType_AttackRollEffect_Value_d6",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AttackRollEffectType_AttackRollEffect_Value_d8",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AttackRollEffectType_AttackRollEffect_Value_flat",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DamageEffectType_DamageEffect_Value_d10",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DamageEffectType_DamageEffect_Value_d100",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DamageEffectType_DamageEffect_Value_d12",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DamageEffectType_DamageEffect_Value_d20",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DamageEffectType_DamageEffect_Value_d4",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DamageEffectType_DamageEffect_Value_d6",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DamageEffectType_DamageEffect_Value_d8",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DamageEffectType_DamageEffect_Value_flat",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HealingEffectType_HealingEffect_Value_d10",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HealingEffectType_HealingEffect_Value_d100",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HealingEffectType_HealingEffect_Value_d12",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HealingEffectType_HealingEffect_Value_d20",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HealingEffectType_HealingEffect_Value_d4",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HealingEffectType_HealingEffect_Value_d6",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HealingEffectType_HealingEffect_Value_d8",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HealingEffectType_HealingEffect_Value_flat",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HitpointEffectType_HitpointEffect_Value_d10",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HitpointEffectType_HitpointEffect_Value_d100",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HitpointEffectType_HitpointEffect_Value_d12",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HitpointEffectType_HitpointEffect_Value_d20",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HitpointEffectType_HitpointEffect_Value_d4",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HitpointEffectType_HitpointEffect_Value_d6",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HitpointEffectType_HitpointEffect_Value_d8",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HitpointEffectType_HitpointEffect_Value_flat",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InitiativeEffectType_InitiativeEffect_Value_d10",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InitiativeEffectType_InitiativeEffect_Value_d100",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InitiativeEffectType_InitiativeEffect_Value_d12",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InitiativeEffectType_InitiativeEffect_Value_d20",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InitiativeEffectType_InitiativeEffect_Value_d4",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InitiativeEffectType_InitiativeEffect_Value_d6",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InitiativeEffectType_InitiativeEffect_Value_d8",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InitiativeEffectType_InitiativeEffect_Value_flat",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MagicItemEffectType_MagicItemEffect_Value_d10",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MagicItemEffectType_MagicItemEffect_Value_d100",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MagicItemEffectType_MagicItemEffect_Value_d12",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MagicItemEffectType_MagicItemEffect_Value_d20",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MagicItemEffectType_MagicItemEffect_Value_d4",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MagicItemEffectType_MagicItemEffect_Value_d6",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MagicItemEffectType_MagicItemEffect_Value_d8",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MagicItemEffectType_MagicItemEffect_Value_flat",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MovementEffectType_MovementEffect_Value_d10",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MovementEffectType_MovementEffect_Value_d100",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MovementEffectType_MovementEffect_Value_d12",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MovementEffectType_MovementEffect_Value_d20",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MovementEffectType_MovementEffect_Value_d4",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MovementEffectType_MovementEffect_Value_d6",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MovementEffectType_MovementEffect_Value_d8",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MovementEffectType_MovementEffect_Value_flat",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SavingThrowEffectType_SavingThrowEffect_Value_d10",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SavingThrowEffectType_SavingThrowEffect_Value_d100",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SavingThrowEffectType_SavingThrowEffect_Value_d12",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SavingThrowEffectType_SavingThrowEffect_Value_d20",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SavingThrowEffectType_SavingThrowEffect_Value_d4",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SavingThrowEffectType_SavingThrowEffect_Value_d6",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SavingThrowEffectType_SavingThrowEffect_Value_d8",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SavingThrowEffectType_SavingThrowEffect_Value_flat",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SizeEffectType_SizeEffect_Value_d10",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SizeEffectType_SizeEffect_Value_d100",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SizeEffectType_SizeEffect_Value_d12",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SizeEffectType_SizeEffect_Value_d20",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AbilityEffectType_AbilityEffect_Value_d10",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "AbilityEffectType_AbilityEffect_Value_d100",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "AbilityEffectType_AbilityEffect_Value_d12",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "AbilityEffectType_AbilityEffect_Value_d20",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "AbilityEffectType_AbilityEffect_Value_d4",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "AbilityEffectType_AbilityEffect_Value_d6",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "AbilityEffectType_AbilityEffect_Value_d8",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "AbilityEffectType_AbilityEffect_Value_flat",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "ActionEffectType_ActionEffect_Value_d10",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "ActionEffectType_ActionEffect_Value_d100",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "ActionEffectType_ActionEffect_Value_d12",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "ActionEffectType_ActionEffect_Value_d20",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "ActionEffectType_ActionEffect_Value_d4",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "ActionEffectType_ActionEffect_Value_d6",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "ActionEffectType_ActionEffect_Value_d8",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "ActionEffectType_ActionEffect_Value_flat",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "ArmorClassEffectType_ArmorClassEffect_Value_d10",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "ArmorClassEffectType_ArmorClassEffect_Value_d100",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "ArmorClassEffectType_ArmorClassEffect_Value_d12",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "ArmorClassEffectType_ArmorClassEffect_Value_d20",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "ArmorClassEffectType_ArmorClassEffect_Value_d4",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "ArmorClassEffectType_ArmorClassEffect_Value_d6",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "ArmorClassEffectType_ArmorClassEffect_Value_d8",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "ArmorClassEffectType_ArmorClassEffect_Value_flat",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "AttackPerAttackActionEffectType_AttackPerActionEffect_Value_d10",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "AttackPerAttackActionEffectType_AttackPerActionEffect_Value_d100",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "AttackPerAttackActionEffectType_AttackPerActionEffect_Value_d12",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "AttackPerAttackActionEffectType_AttackPerActionEffect_Value_d20",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "AttackPerAttackActionEffectType_AttackPerActionEffect_Value_d4",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "AttackPerAttackActionEffectType_AttackPerActionEffect_Value_d6",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "AttackPerAttackActionEffectType_AttackPerActionEffect_Value_d8",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "AttackPerAttackActionEffectType_AttackPerActionEffect_Value_flat",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "AttackRollEffectType_AttackRollEffect_Value_d10",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "AttackRollEffectType_AttackRollEffect_Value_d100",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "AttackRollEffectType_AttackRollEffect_Value_d12",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "AttackRollEffectType_AttackRollEffect_Value_d20",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "AttackRollEffectType_AttackRollEffect_Value_d4",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "AttackRollEffectType_AttackRollEffect_Value_d6",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "AttackRollEffectType_AttackRollEffect_Value_d8",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "AttackRollEffectType_AttackRollEffect_Value_flat",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "DamageEffectType_DamageEffect_Value_d10",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "DamageEffectType_DamageEffect_Value_d100",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "DamageEffectType_DamageEffect_Value_d12",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "DamageEffectType_DamageEffect_Value_d20",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "DamageEffectType_DamageEffect_Value_d4",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "DamageEffectType_DamageEffect_Value_d6",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "DamageEffectType_DamageEffect_Value_d8",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "DamageEffectType_DamageEffect_Value_flat",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "HealingEffectType_HealingEffect_Value_d10",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "HealingEffectType_HealingEffect_Value_d100",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "HealingEffectType_HealingEffect_Value_d12",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "HealingEffectType_HealingEffect_Value_d20",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "HealingEffectType_HealingEffect_Value_d4",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "HealingEffectType_HealingEffect_Value_d6",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "HealingEffectType_HealingEffect_Value_d8",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "HealingEffectType_HealingEffect_Value_flat",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "HitpointEffectType_HitpointEffect_Value_d10",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "HitpointEffectType_HitpointEffect_Value_d100",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "HitpointEffectType_HitpointEffect_Value_d12",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "HitpointEffectType_HitpointEffect_Value_d20",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "HitpointEffectType_HitpointEffect_Value_d4",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "HitpointEffectType_HitpointEffect_Value_d6",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "HitpointEffectType_HitpointEffect_Value_d8",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "HitpointEffectType_HitpointEffect_Value_flat",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "InitiativeEffectType_InitiativeEffect_Value_d10",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "InitiativeEffectType_InitiativeEffect_Value_d100",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "InitiativeEffectType_InitiativeEffect_Value_d12",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "InitiativeEffectType_InitiativeEffect_Value_d20",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "InitiativeEffectType_InitiativeEffect_Value_d4",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "InitiativeEffectType_InitiativeEffect_Value_d6",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "InitiativeEffectType_InitiativeEffect_Value_d8",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "InitiativeEffectType_InitiativeEffect_Value_flat",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "MagicItemEffectType_MagicItemEffect_Value_d10",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "MagicItemEffectType_MagicItemEffect_Value_d100",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "MagicItemEffectType_MagicItemEffect_Value_d12",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "MagicItemEffectType_MagicItemEffect_Value_d20",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "MagicItemEffectType_MagicItemEffect_Value_d4",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "MagicItemEffectType_MagicItemEffect_Value_d6",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "MagicItemEffectType_MagicItemEffect_Value_d8",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "MagicItemEffectType_MagicItemEffect_Value_flat",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "MovementEffectType_MovementEffect_Value_d10",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "MovementEffectType_MovementEffect_Value_d100",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "MovementEffectType_MovementEffect_Value_d12",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "MovementEffectType_MovementEffect_Value_d20",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "MovementEffectType_MovementEffect_Value_d4",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "MovementEffectType_MovementEffect_Value_d6",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "MovementEffectType_MovementEffect_Value_d8",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "MovementEffectType_MovementEffect_Value_flat",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "SavingThrowEffectType_SavingThrowEffect_Value_d10",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "SavingThrowEffectType_SavingThrowEffect_Value_d100",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "SavingThrowEffectType_SavingThrowEffect_Value_d12",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "SavingThrowEffectType_SavingThrowEffect_Value_d20",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "SavingThrowEffectType_SavingThrowEffect_Value_d4",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "SavingThrowEffectType_SavingThrowEffect_Value_d6",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "SavingThrowEffectType_SavingThrowEffect_Value_d8",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "SavingThrowEffectType_SavingThrowEffect_Value_flat",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "SizeEffectType_SizeEffect_Value_d10",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "SizeEffectType_SizeEffect_Value_d100",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "SizeEffectType_SizeEffect_Value_d12",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "SizeEffectType_SizeEffect_Value_d20",
                table: "EffectInstances");

            migrationBuilder.RenameColumn(
                name: "StatusEffectType_StatusEffect",
                table: "EffectInstances",
                newName: "StatusEffect");

            migrationBuilder.RenameColumn(
                name: "SkillEffectType_SkillEffect_Skill",
                table: "EffectInstances",
                newName: "SkillEffect_Skill");

            migrationBuilder.RenameColumn(
                name: "SkillEffectType_SkillEffect",
                table: "EffectInstances",
                newName: "SkillEffect");

            migrationBuilder.RenameColumn(
                name: "SizeEffectType_SizeEffect_SizeToSet",
                table: "EffectInstances",
                newName: "SizeEffect_SizeToSet");

            migrationBuilder.RenameColumn(
                name: "SizeEffectType_SizeEffect",
                table: "EffectInstances",
                newName: "SizeEffect");

            migrationBuilder.RenameColumn(
                name: "SavingThrowEffectType_SavingThrowEffect_Ability",
                table: "EffectInstances",
                newName: "SavingThrowEffect_Ability");

            migrationBuilder.RenameColumn(
                name: "SavingThrowEffectType_SavingThrowEffect",
                table: "EffectInstances",
                newName: "SavingThrowEffect");

            migrationBuilder.RenameColumn(
                name: "ResistanceEffectType_ResistanceEffect_DamageType",
                table: "EffectInstances",
                newName: "ResistanceEffect_DamageType");

            migrationBuilder.RenameColumn(
                name: "ResistanceEffectType_ResistanceEffect",
                table: "EffectInstances",
                newName: "ResistanceEffect");

            migrationBuilder.RenameColumn(
                name: "ProficiencyEffectType_ProficiencyEffect",
                table: "EffectInstances",
                newName: "ProficiencyEffect");

            migrationBuilder.RenameColumn(
                name: "MovementEffectType_MovementEffect",
                table: "EffectInstances",
                newName: "MovementEffect");

            migrationBuilder.RenameColumn(
                name: "MovementCostEffectType_MovementCost_Multiplier",
                table: "EffectInstances",
                newName: "MovementCost_Multiplier");

            migrationBuilder.RenameColumn(
                name: "HitpointEffectType_HitpointEffect",
                table: "EffectInstances",
                newName: "HitpointEffect");

            migrationBuilder.RenameColumn(
                name: "DamageEffectType_DamageEffect_DamageType",
                table: "EffectInstances",
                newName: "DamageEffect_DamageType");

            migrationBuilder.RenameColumn(
                name: "DamageEffectType_DamageEffect",
                table: "EffectInstances",
                newName: "DamageEffect");

            migrationBuilder.RenameColumn(
                name: "AttackRollEffectType_AttackRollEffect_Type",
                table: "EffectInstances",
                newName: "AttackRollEffect_Type");

            migrationBuilder.RenameColumn(
                name: "AttackRollEffectType_AttackRollEffect_Source",
                table: "EffectInstances",
                newName: "AttackRollEffect_Source");

            migrationBuilder.RenameColumn(
                name: "AttackRollEffectType_AttackRollEffect_Range",
                table: "EffectInstances",
                newName: "AttackRollEffect_Range");

            migrationBuilder.RenameColumn(
                name: "AttackPerAttackActionEffectType_AttackPerActionEffect",
                table: "EffectInstances",
                newName: "AttackPerActionEffect");

            migrationBuilder.RenameColumn(
                name: "ActionEffectType_ActionEffect",
                table: "EffectInstances",
                newName: "ActionEffect");

            migrationBuilder.RenameColumn(
                name: "AbilityEffectType_AbilityEffect_Ability",
                table: "EffectInstances",
                newName: "AbilityEffect_Ability");

            migrationBuilder.RenameColumn(
                name: "AbilityEffectType_AbilityEffect",
                table: "EffectInstances",
                newName: "AbilityEffect");

            migrationBuilder.RenameColumn(
                name: "SkillEffectType_SkillEffect_Value_flat",
                table: "EffectInstances",
                newName: "SkillEffect_Value");

            migrationBuilder.RenameColumn(
                name: "SkillEffectType_SkillEffect_Value_d8",
                table: "EffectInstances",
                newName: "SizeEffect_Value");

            migrationBuilder.RenameColumn(
                name: "SkillEffectType_SkillEffect_Value_d6",
                table: "EffectInstances",
                newName: "SavingThrowEffect_Value");

            migrationBuilder.RenameColumn(
                name: "SkillEffectType_SkillEffect_Value_d4",
                table: "EffectInstances",
                newName: "MovementEffect_Value");

            migrationBuilder.RenameColumn(
                name: "SkillEffectType_SkillEffect_Value_d20",
                table: "EffectInstances",
                newName: "MagicItemEffect_Value");

            migrationBuilder.RenameColumn(
                name: "SkillEffectType_SkillEffect_Value_d12",
                table: "EffectInstances",
                newName: "InitiativeEffect_Value");

            migrationBuilder.RenameColumn(
                name: "SkillEffectType_SkillEffect_Value_d100",
                table: "EffectInstances",
                newName: "HitpointEffect_Value");

            migrationBuilder.RenameColumn(
                name: "SkillEffectType_SkillEffect_Value_d10",
                table: "EffectInstances",
                newName: "HealingEffec_Value");

            migrationBuilder.RenameColumn(
                name: "SizeEffectType_SizeEffect_Value_flat",
                table: "EffectInstances",
                newName: "AttackPerActionEffect_Value");

            migrationBuilder.RenameColumn(
                name: "SizeEffectType_SizeEffect_Value_d8",
                table: "EffectInstances",
                newName: "ArmorClassEffect_Value");

            migrationBuilder.RenameColumn(
                name: "SizeEffectType_SizeEffect_Value_d6",
                table: "EffectInstances",
                newName: "ActionEffect_Value");

            migrationBuilder.RenameColumn(
                name: "SizeEffectType_SizeEffect_Value_d4",
                table: "EffectInstances",
                newName: "AbilityEffect_Value");

            migrationBuilder.AddColumn<string>(
                name: "AttackRollEffect_Value",
                table: "EffectInstances",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DamageEffect_Value",
                table: "EffectInstances",
                type: "TEXT",
                nullable: true);
        }
    }
}
