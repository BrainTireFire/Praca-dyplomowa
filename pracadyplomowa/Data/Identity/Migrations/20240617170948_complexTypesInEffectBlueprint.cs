using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Identity.Migrations
{
    /// <inheritdoc />
    public partial class complexTypesInEffectBlueprint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StatusEffect",
                table: "EffectBlueprints",
                newName: "StatusEffectType_StatusEffect");

            migrationBuilder.RenameColumn(
                name: "SkillEffect_Value",
                table: "EffectBlueprints",
                newName: "SkillEffectType_SkillEffect_Value");

            migrationBuilder.RenameColumn(
                name: "SkillEffect_Skill",
                table: "EffectBlueprints",
                newName: "SkillEffectType_SkillEffect_Skill");

            migrationBuilder.RenameColumn(
                name: "SkillEffect",
                table: "EffectBlueprints",
                newName: "SkillEffectType_SkillEffect");

            migrationBuilder.RenameColumn(
                name: "SizeEffect_Value",
                table: "EffectBlueprints",
                newName: "SizeEffectType_SizeEffect_Value");

            migrationBuilder.RenameColumn(
                name: "SizeEffect_SizeToSet",
                table: "EffectBlueprints",
                newName: "SizeEffectType_SizeEffect_SizeToSet");

            migrationBuilder.RenameColumn(
                name: "SizeEffect",
                table: "EffectBlueprints",
                newName: "SizeEffectType_SizeEffect");

            migrationBuilder.RenameColumn(
                name: "SavingThrowEffect_Value",
                table: "EffectBlueprints",
                newName: "SavingThrowEffectType_SavingThrowEffect_Value");

            migrationBuilder.RenameColumn(
                name: "SavingThrowEffect_Ability",
                table: "EffectBlueprints",
                newName: "SavingThrowEffectType_SavingThrowEffect_Ability");

            migrationBuilder.RenameColumn(
                name: "SavingThrowEffect",
                table: "EffectBlueprints",
                newName: "SavingThrowEffectType_SavingThrowEffect");

            migrationBuilder.RenameColumn(
                name: "ResistanceEffect_DamageType",
                table: "EffectBlueprints",
                newName: "ResistanceEffectType_ResistanceEffect_DamageType");

            migrationBuilder.RenameColumn(
                name: "ResistanceEffect",
                table: "EffectBlueprints",
                newName: "ResistanceEffectType_ResistanceEffect");

            migrationBuilder.RenameColumn(
                name: "ProficiencyEffect",
                table: "EffectBlueprints",
                newName: "ProficiencyEffectType_ProficiencyEffect");

            migrationBuilder.RenameColumn(
                name: "MovementEffect_Value",
                table: "EffectBlueprints",
                newName: "MovementEffectType_MovementEffect_Value");

            migrationBuilder.RenameColumn(
                name: "MovementEffect",
                table: "EffectBlueprints",
                newName: "MovementEffectType_MovementEffect");

            migrationBuilder.RenameColumn(
                name: "MovementCost_Multiplier",
                table: "EffectBlueprints",
                newName: "MovementCostEffectType_MovementCost_Multiplier");

            migrationBuilder.RenameColumn(
                name: "MagicItemEffect_Value",
                table: "EffectBlueprints",
                newName: "MagicItemEffectType_MagicItemEffect_Value");

            migrationBuilder.RenameColumn(
                name: "InitiativeEffect_Value",
                table: "EffectBlueprints",
                newName: "InitiativeEffectType_InitiativeEffect_Value");

            migrationBuilder.RenameColumn(
                name: "HitpointEffect_Value",
                table: "EffectBlueprints",
                newName: "HitpointEffectType_HitpointEffect_Value");

            migrationBuilder.RenameColumn(
                name: "HitpointEffect",
                table: "EffectBlueprints",
                newName: "HitpointEffectType_HitpointEffect");

            migrationBuilder.RenameColumn(
                name: "DamageEffect_Value",
                table: "EffectBlueprints",
                newName: "DamageEffectType_DamageEffect_Value");

            migrationBuilder.RenameColumn(
                name: "DamageEffect_DamageType",
                table: "EffectBlueprints",
                newName: "DamageEffectType_DamageEffect_DamageType");

            migrationBuilder.RenameColumn(
                name: "DamageEffect",
                table: "EffectBlueprints",
                newName: "DamageEffectType_DamageEffect");

            migrationBuilder.RenameColumn(
                name: "AttackRollEffect_Value",
                table: "EffectBlueprints",
                newName: "AttackRollEffectType_AttackRollEffect_Value");

            migrationBuilder.RenameColumn(
                name: "AttackRollEffect_Type",
                table: "EffectBlueprints",
                newName: "AttackRollEffectType_AttackRollEffect_Type");

            migrationBuilder.RenameColumn(
                name: "AttackRollEffect_Source",
                table: "EffectBlueprints",
                newName: "AttackRollEffectType_AttackRollEffect_Source");

            migrationBuilder.RenameColumn(
                name: "AttackRollEffect_Range",
                table: "EffectBlueprints",
                newName: "AttackRollEffectType_AttackRollEffect_Range");

            migrationBuilder.RenameColumn(
                name: "AttackPerActionEffect_Value",
                table: "EffectBlueprints",
                newName: "AttackPerAttackActionEffectType_AttackPerActionEffect_Value");

            migrationBuilder.RenameColumn(
                name: "AttackPerActionEffect",
                table: "EffectBlueprints",
                newName: "AttackPerAttackActionEffectType_AttackPerActionEffect");

            migrationBuilder.RenameColumn(
                name: "ArmorClassEffect_Value",
                table: "EffectBlueprints",
                newName: "ArmorClassEffectType_ArmorClassEffect_Value");

            migrationBuilder.RenameColumn(
                name: "ActionEffect_Value",
                table: "EffectBlueprints",
                newName: "ActionEffectType_ActionEffect_Value");

            migrationBuilder.RenameColumn(
                name: "ActionEffect",
                table: "EffectBlueprints",
                newName: "ActionEffectType_ActionEffect");

            migrationBuilder.RenameColumn(
                name: "AbilityEffect_Value",
                table: "EffectBlueprints",
                newName: "AbilityEffectType_AbilityEffect_Value");

            migrationBuilder.RenameColumn(
                name: "AbilityEffect_Ability",
                table: "EffectBlueprints",
                newName: "AbilityEffectType_AbilityEffect_Ability");

            migrationBuilder.RenameColumn(
                name: "AbilityEffect",
                table: "EffectBlueprints",
                newName: "AbilityEffectType_AbilityEffect");

            migrationBuilder.RenameColumn(
                name: "HealingEffec_Value",
                table: "EffectBlueprints",
                newName: "HealingEffectType_HealingEffect_Value");

            migrationBuilder.AddColumn<int>(
                name: "HitDie_flat",
                table: "ClassLevels",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsedHitDice_flat",
                table: "Characters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HitDie_flat",
                table: "ClassLevels");

            migrationBuilder.DropColumn(
                name: "UsedHitDice_flat",
                table: "Characters");

            migrationBuilder.RenameColumn(
                name: "StatusEffectType_StatusEffect",
                table: "EffectBlueprints",
                newName: "StatusEffect");

            migrationBuilder.RenameColumn(
                name: "SkillEffectType_SkillEffect_Value",
                table: "EffectBlueprints",
                newName: "SkillEffect_Value");

            migrationBuilder.RenameColumn(
                name: "SkillEffectType_SkillEffect_Skill",
                table: "EffectBlueprints",
                newName: "SkillEffect_Skill");

            migrationBuilder.RenameColumn(
                name: "SkillEffectType_SkillEffect",
                table: "EffectBlueprints",
                newName: "SkillEffect");

            migrationBuilder.RenameColumn(
                name: "SizeEffectType_SizeEffect_Value",
                table: "EffectBlueprints",
                newName: "SizeEffect_Value");

            migrationBuilder.RenameColumn(
                name: "SizeEffectType_SizeEffect_SizeToSet",
                table: "EffectBlueprints",
                newName: "SizeEffect_SizeToSet");

            migrationBuilder.RenameColumn(
                name: "SizeEffectType_SizeEffect",
                table: "EffectBlueprints",
                newName: "SizeEffect");

            migrationBuilder.RenameColumn(
                name: "SavingThrowEffectType_SavingThrowEffect_Value",
                table: "EffectBlueprints",
                newName: "SavingThrowEffect_Value");

            migrationBuilder.RenameColumn(
                name: "SavingThrowEffectType_SavingThrowEffect_Ability",
                table: "EffectBlueprints",
                newName: "SavingThrowEffect_Ability");

            migrationBuilder.RenameColumn(
                name: "SavingThrowEffectType_SavingThrowEffect",
                table: "EffectBlueprints",
                newName: "SavingThrowEffect");

            migrationBuilder.RenameColumn(
                name: "ResistanceEffectType_ResistanceEffect_DamageType",
                table: "EffectBlueprints",
                newName: "ResistanceEffect_DamageType");

            migrationBuilder.RenameColumn(
                name: "ResistanceEffectType_ResistanceEffect",
                table: "EffectBlueprints",
                newName: "ResistanceEffect");

            migrationBuilder.RenameColumn(
                name: "ProficiencyEffectType_ProficiencyEffect",
                table: "EffectBlueprints",
                newName: "ProficiencyEffect");

            migrationBuilder.RenameColumn(
                name: "MovementEffectType_MovementEffect_Value",
                table: "EffectBlueprints",
                newName: "MovementEffect_Value");

            migrationBuilder.RenameColumn(
                name: "MovementEffectType_MovementEffect",
                table: "EffectBlueprints",
                newName: "MovementEffect");

            migrationBuilder.RenameColumn(
                name: "MovementCostEffectType_MovementCost_Multiplier",
                table: "EffectBlueprints",
                newName: "MovementCost_Multiplier");

            migrationBuilder.RenameColumn(
                name: "MagicItemEffectType_MagicItemEffect_Value",
                table: "EffectBlueprints",
                newName: "MagicItemEffect_Value");

            migrationBuilder.RenameColumn(
                name: "InitiativeEffectType_InitiativeEffect_Value",
                table: "EffectBlueprints",
                newName: "InitiativeEffect_Value");

            migrationBuilder.RenameColumn(
                name: "HitpointEffectType_HitpointEffect_Value",
                table: "EffectBlueprints",
                newName: "HitpointEffect_Value");

            migrationBuilder.RenameColumn(
                name: "HitpointEffectType_HitpointEffect",
                table: "EffectBlueprints",
                newName: "HitpointEffect");

            migrationBuilder.RenameColumn(
                name: "DamageEffectType_DamageEffect_Value",
                table: "EffectBlueprints",
                newName: "DamageEffect_Value");

            migrationBuilder.RenameColumn(
                name: "DamageEffectType_DamageEffect_DamageType",
                table: "EffectBlueprints",
                newName: "DamageEffect_DamageType");

            migrationBuilder.RenameColumn(
                name: "DamageEffectType_DamageEffect",
                table: "EffectBlueprints",
                newName: "DamageEffect");

            migrationBuilder.RenameColumn(
                name: "AttackRollEffectType_AttackRollEffect_Value",
                table: "EffectBlueprints",
                newName: "AttackRollEffect_Value");

            migrationBuilder.RenameColumn(
                name: "AttackRollEffectType_AttackRollEffect_Type",
                table: "EffectBlueprints",
                newName: "AttackRollEffect_Type");

            migrationBuilder.RenameColumn(
                name: "AttackRollEffectType_AttackRollEffect_Source",
                table: "EffectBlueprints",
                newName: "AttackRollEffect_Source");

            migrationBuilder.RenameColumn(
                name: "AttackRollEffectType_AttackRollEffect_Range",
                table: "EffectBlueprints",
                newName: "AttackRollEffect_Range");

            migrationBuilder.RenameColumn(
                name: "AttackPerAttackActionEffectType_AttackPerActionEffect_Value",
                table: "EffectBlueprints",
                newName: "AttackPerActionEffect_Value");

            migrationBuilder.RenameColumn(
                name: "AttackPerAttackActionEffectType_AttackPerActionEffect",
                table: "EffectBlueprints",
                newName: "AttackPerActionEffect");

            migrationBuilder.RenameColumn(
                name: "ArmorClassEffectType_ArmorClassEffect_Value",
                table: "EffectBlueprints",
                newName: "ArmorClassEffect_Value");

            migrationBuilder.RenameColumn(
                name: "ActionEffectType_ActionEffect_Value",
                table: "EffectBlueprints",
                newName: "ActionEffect_Value");

            migrationBuilder.RenameColumn(
                name: "ActionEffectType_ActionEffect",
                table: "EffectBlueprints",
                newName: "ActionEffect");

            migrationBuilder.RenameColumn(
                name: "AbilityEffectType_AbilityEffect_Value",
                table: "EffectBlueprints",
                newName: "AbilityEffect_Value");

            migrationBuilder.RenameColumn(
                name: "AbilityEffectType_AbilityEffect_Ability",
                table: "EffectBlueprints",
                newName: "AbilityEffect_Ability");

            migrationBuilder.RenameColumn(
                name: "AbilityEffectType_AbilityEffect",
                table: "EffectBlueprints",
                newName: "AbilityEffect");

            migrationBuilder.RenameColumn(
                name: "HealingEffectType_HealingEffect_Value",
                table: "EffectBlueprints",
                newName: "HealingEffec_Value");
        }
    }
}
