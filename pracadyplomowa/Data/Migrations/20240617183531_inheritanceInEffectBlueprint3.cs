using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Migrations
{
    /// <inheritdoc />
    public partial class inheritanceInEffectBlueprint3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "SizeEffectType_SizeEffect_Value_d4",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "SizeEffectType_SizeEffect_Value_d6",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "SizeEffectType_SizeEffect_Value_d8",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "SizeEffectType_SizeEffect_Value_flat",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "SkillEffectType_SkillEffect_Value_d10",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "SkillEffectType_SkillEffect_Value_d100",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "SkillEffectType_SkillEffect_Value_d12",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "SkillEffectType_SkillEffect_Value_d20",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "SkillEffectType_SkillEffect_Value_d4",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "SkillEffectType_SkillEffect_Value_d6",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "SkillEffectType_SkillEffect_Value_d8",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "SkillEffectType_SkillEffect_Value_flat",
                table: "EffectInstances");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<int>(
                name: "SizeEffectType_SizeEffect_Value_d4",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SizeEffectType_SizeEffect_Value_d6",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SizeEffectType_SizeEffect_Value_d8",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SizeEffectType_SizeEffect_Value_flat",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SkillEffectType_SkillEffect_Value_d10",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SkillEffectType_SkillEffect_Value_d100",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SkillEffectType_SkillEffect_Value_d12",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SkillEffectType_SkillEffect_Value_d20",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SkillEffectType_SkillEffect_Value_d4",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SkillEffectType_SkillEffect_Value_d6",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SkillEffectType_SkillEffect_Value_d8",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SkillEffectType_SkillEffect_Value_flat",
                table: "EffectInstances",
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
        }
    }
}
