using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class renamedEffectTypeColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StatusEffectType_StatusEffect",
                table: "EffectInstances",
                newName: "EffectType_StatusEffect");

            migrationBuilder.RenameColumn(
                name: "SkillEffectType_SkillEffect_Skill",
                table: "EffectInstances",
                newName: "EffectType_SkillEffect_Skill");

            migrationBuilder.RenameColumn(
                name: "SkillEffectType_SkillEffect",
                table: "EffectInstances",
                newName: "EffectType_SkillEffect");

            migrationBuilder.RenameColumn(
                name: "SizeEffectType_SizeEffect_SizeToSet",
                table: "EffectInstances",
                newName: "EffectType_SizeEffect_SizeToSet");

            migrationBuilder.RenameColumn(
                name: "SizeEffectType_SizeEffect",
                table: "EffectInstances",
                newName: "EffectType_SizeEffect");

            migrationBuilder.RenameColumn(
                name: "SizeEffectType_SizeBonus",
                table: "EffectInstances",
                newName: "EffectType_SizeBonus");

            migrationBuilder.RenameColumn(
                name: "SavingThrowEffectType_SavingThrowEffect_Nature",
                table: "EffectInstances",
                newName: "EffectType_SavingThrowEffect_Nature");

            migrationBuilder.RenameColumn(
                name: "SavingThrowEffectType_SavingThrowEffect_Condition",
                table: "EffectInstances",
                newName: "EffectType_SavingThrowEffect_Condition");

            migrationBuilder.RenameColumn(
                name: "SavingThrowEffectType_SavingThrowEffect_Ability",
                table: "EffectInstances",
                newName: "EffectType_SavingThrowEffect_Ability");

            migrationBuilder.RenameColumn(
                name: "SavingThrowEffectType_SavingThrowEffect",
                table: "EffectInstances",
                newName: "EffectType_SavingThrowEffect");

            migrationBuilder.RenameColumn(
                name: "ResistanceEffectType_ResistanceEffect_DamageType",
                table: "EffectInstances",
                newName: "EffectType_ResistanceEffect_DamageType");

            migrationBuilder.RenameColumn(
                name: "ResistanceEffectType_ResistanceEffect",
                table: "EffectInstances",
                newName: "EffectType_ResistanceEffect");

            migrationBuilder.RenameColumn(
                name: "MovementEffectType_MovementEffect",
                table: "EffectInstances",
                newName: "EffectType_MovementEffect");

            migrationBuilder.RenameColumn(
                name: "MovementCostEffectType_MovementCost_Multiplier",
                table: "EffectInstances",
                newName: "EffectType_MovementCost_Multiplier");

            migrationBuilder.RenameColumn(
                name: "HitpointEffectType_HitpointEffect",
                table: "EffectInstances",
                newName: "EffectType_HitpointEffect");

            migrationBuilder.RenameColumn(
                name: "DamageEffectType_DamageEffect_DamageType",
                table: "EffectInstances",
                newName: "EffectType_DamageEffect_DamageType");

            migrationBuilder.RenameColumn(
                name: "DamageEffectType_DamageEffect",
                table: "EffectInstances",
                newName: "EffectType_DamageEffect");

            migrationBuilder.RenameColumn(
                name: "AttackRollEffectType_AttackRollEffect_Type",
                table: "EffectInstances",
                newName: "EffectType_AttackRollEffect_Type");

            migrationBuilder.RenameColumn(
                name: "AttackRollEffectType_AttackRollEffect_Source",
                table: "EffectInstances",
                newName: "EffectType_AttackRollEffect_Source");

            migrationBuilder.RenameColumn(
                name: "AttackRollEffectType_AttackRollEffect_Range",
                table: "EffectInstances",
                newName: "EffectType_AttackRollEffect_Range");

            migrationBuilder.RenameColumn(
                name: "AttackPerAttackActionEffectType_AttackPerActionEffect",
                table: "EffectInstances",
                newName: "EffectType_AttackPerActionEffect");

            migrationBuilder.RenameColumn(
                name: "ActionEffectType_ActionEffect",
                table: "EffectInstances",
                newName: "EffectType_ActionEffect");

            migrationBuilder.RenameColumn(
                name: "AbilityEffectType_AbilityEffect_Ability",
                table: "EffectInstances",
                newName: "EffectType_AbilityEffect_Ability");

            migrationBuilder.RenameColumn(
                name: "AbilityEffectType_AbilityEffect",
                table: "EffectInstances",
                newName: "EffectType_AbilityEffect");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EffectType_StatusEffect",
                table: "EffectInstances",
                newName: "StatusEffectType_StatusEffect");

            migrationBuilder.RenameColumn(
                name: "EffectType_SkillEffect_Skill",
                table: "EffectInstances",
                newName: "SkillEffectType_SkillEffect_Skill");

            migrationBuilder.RenameColumn(
                name: "EffectType_SkillEffect",
                table: "EffectInstances",
                newName: "SkillEffectType_SkillEffect");

            migrationBuilder.RenameColumn(
                name: "EffectType_SizeEffect_SizeToSet",
                table: "EffectInstances",
                newName: "SizeEffectType_SizeEffect_SizeToSet");

            migrationBuilder.RenameColumn(
                name: "EffectType_SizeEffect",
                table: "EffectInstances",
                newName: "SizeEffectType_SizeEffect");

            migrationBuilder.RenameColumn(
                name: "EffectType_SizeBonus",
                table: "EffectInstances",
                newName: "SizeEffectType_SizeBonus");

            migrationBuilder.RenameColumn(
                name: "EffectType_SavingThrowEffect_Nature",
                table: "EffectInstances",
                newName: "SavingThrowEffectType_SavingThrowEffect_Nature");

            migrationBuilder.RenameColumn(
                name: "EffectType_SavingThrowEffect_Condition",
                table: "EffectInstances",
                newName: "SavingThrowEffectType_SavingThrowEffect_Condition");

            migrationBuilder.RenameColumn(
                name: "EffectType_SavingThrowEffect_Ability",
                table: "EffectInstances",
                newName: "SavingThrowEffectType_SavingThrowEffect_Ability");

            migrationBuilder.RenameColumn(
                name: "EffectType_SavingThrowEffect",
                table: "EffectInstances",
                newName: "SavingThrowEffectType_SavingThrowEffect");

            migrationBuilder.RenameColumn(
                name: "EffectType_ResistanceEffect_DamageType",
                table: "EffectInstances",
                newName: "ResistanceEffectType_ResistanceEffect_DamageType");

            migrationBuilder.RenameColumn(
                name: "EffectType_ResistanceEffect",
                table: "EffectInstances",
                newName: "ResistanceEffectType_ResistanceEffect");

            migrationBuilder.RenameColumn(
                name: "EffectType_MovementEffect",
                table: "EffectInstances",
                newName: "MovementEffectType_MovementEffect");

            migrationBuilder.RenameColumn(
                name: "EffectType_MovementCost_Multiplier",
                table: "EffectInstances",
                newName: "MovementCostEffectType_MovementCost_Multiplier");

            migrationBuilder.RenameColumn(
                name: "EffectType_HitpointEffect",
                table: "EffectInstances",
                newName: "HitpointEffectType_HitpointEffect");

            migrationBuilder.RenameColumn(
                name: "EffectType_DamageEffect_DamageType",
                table: "EffectInstances",
                newName: "DamageEffectType_DamageEffect_DamageType");

            migrationBuilder.RenameColumn(
                name: "EffectType_DamageEffect",
                table: "EffectInstances",
                newName: "DamageEffectType_DamageEffect");

            migrationBuilder.RenameColumn(
                name: "EffectType_AttackRollEffect_Type",
                table: "EffectInstances",
                newName: "AttackRollEffectType_AttackRollEffect_Type");

            migrationBuilder.RenameColumn(
                name: "EffectType_AttackRollEffect_Source",
                table: "EffectInstances",
                newName: "AttackRollEffectType_AttackRollEffect_Source");

            migrationBuilder.RenameColumn(
                name: "EffectType_AttackRollEffect_Range",
                table: "EffectInstances",
                newName: "AttackRollEffectType_AttackRollEffect_Range");

            migrationBuilder.RenameColumn(
                name: "EffectType_AttackPerActionEffect",
                table: "EffectInstances",
                newName: "AttackPerAttackActionEffectType_AttackPerActionEffect");

            migrationBuilder.RenameColumn(
                name: "EffectType_ActionEffect",
                table: "EffectInstances",
                newName: "ActionEffectType_ActionEffect");

            migrationBuilder.RenameColumn(
                name: "EffectType_AbilityEffect_Ability",
                table: "EffectInstances",
                newName: "AbilityEffectType_AbilityEffect_Ability");

            migrationBuilder.RenameColumn(
                name: "EffectType_AbilityEffect",
                table: "EffectInstances",
                newName: "AbilityEffectType_AbilityEffect");
        }
    }
}
