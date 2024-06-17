using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Migrations
{
    /// <inheritdoc />
    public partial class inheritanceInEffectBlueprint2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AbilityEffectType_AbilityEffect",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AbilityEffectType_AbilityEffect_Ability",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

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
                name: "ActionEffectType_ActionEffect",
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
                name: "AttackPerAttackActionEffectType_AttackPerActionEffect",
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
                name: "AttackRollEffectType_AttackRollEffect_Range",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AttackRollEffectType_AttackRollEffect_Source",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AttackRollEffectType_AttackRollEffect_Type",
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
                name: "DamageEffectType_DamageEffect",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DamageEffectType_DamageEffect_DamageType",
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

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "EffectBlueprints",
                type: "TEXT",
                maxLength: 55,
                nullable: false,
                defaultValue: "");

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
                name: "HitpointEffectType_HitpointEffect",
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
                name: "MovementCostEffectType_MovementCost_Multiplier",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MovementEffectType_MovementEffect",
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
                name: "ProficiencyEffectType_ProficiencyEffect",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ResistanceEffectType_ResistanceEffect",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ResistanceEffectType_ResistanceEffect_DamageType",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SavingThrowEffectType_SavingThrowEffect",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SavingThrowEffectType_SavingThrowEffect_Ability",
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
                name: "SizeEffectType_SizeEffect",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SizeEffectType_SizeEffect_SizeToSet",
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
                name: "SkillEffectType_SkillEffect",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SkillEffectType_SkillEffect_Skill",
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

            migrationBuilder.AddColumn<int>(
                name: "SkillEffectType_SkillEffect_Value_d4",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SkillEffectType_SkillEffect_Value_d6",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SkillEffectType_SkillEffect_Value_d8",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SkillEffectType_SkillEffect_Value_flat",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StatusEffectType_StatusEffect",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AbilityEffectType_AbilityEffect",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "AbilityEffectType_AbilityEffect_Ability",
                table: "EffectBlueprints");

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
                name: "ActionEffectType_ActionEffect",
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
                name: "AttackPerAttackActionEffectType_AttackPerActionEffect",
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
                name: "AttackRollEffectType_AttackRollEffect_Range",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "AttackRollEffectType_AttackRollEffect_Source",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "AttackRollEffectType_AttackRollEffect_Type",
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
                name: "DamageEffectType_DamageEffect",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "DamageEffectType_DamageEffect_DamageType",
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
                name: "Discriminator",
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
                name: "HitpointEffectType_HitpointEffect",
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
                name: "MovementCostEffectType_MovementCost_Multiplier",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "MovementEffectType_MovementEffect",
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
                name: "ProficiencyEffectType_ProficiencyEffect",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "ResistanceEffectType_ResistanceEffect",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "ResistanceEffectType_ResistanceEffect_DamageType",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "SavingThrowEffectType_SavingThrowEffect",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "SavingThrowEffectType_SavingThrowEffect_Ability",
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
                name: "SizeEffectType_SizeEffect",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "SizeEffectType_SizeEffect_SizeToSet",
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
                name: "SkillEffectType_SkillEffect",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "SkillEffectType_SkillEffect_Skill",
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

            migrationBuilder.DropColumn(
                name: "SkillEffectType_SkillEffect_Value_d4",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "SkillEffectType_SkillEffect_Value_d6",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "SkillEffectType_SkillEffect_Value_d8",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "SkillEffectType_SkillEffect_Value_flat",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "StatusEffectType_StatusEffect",
                table: "EffectBlueprints");
        }
    }
}
