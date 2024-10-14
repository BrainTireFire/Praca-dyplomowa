using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class effectFeatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Conditional",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsImplemented",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SavingThrowEffectType_SavingThrowEffect_Condition",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SavingThrowEffectType_SavingThrowEffect_Nature",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Conditional",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasNoEffectInCombat",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsImplemented",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SavingThrowEffectType_SavingThrowEffect_Condition",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SavingThrowEffectType_SavingThrowEffect_Nature",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Conditional",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "IsImplemented",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "SavingThrowEffectType_SavingThrowEffect_Condition",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "SavingThrowEffectType_SavingThrowEffect_Nature",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "Conditional",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "HasNoEffectInCombat",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "IsImplemented",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "SavingThrowEffectType_SavingThrowEffect_Condition",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "SavingThrowEffectType_SavingThrowEffect_Nature",
                table: "EffectBlueprints");
        }
    }
}
