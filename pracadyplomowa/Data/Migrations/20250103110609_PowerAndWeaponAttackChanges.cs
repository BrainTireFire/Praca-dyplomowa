using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class PowerAndWeaponAttackChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SavingThrowRetakenEveryTurn",
                table: "EffectGroups");

            migrationBuilder.RenameColumn(
                name: "SilverPieces",
                table: "ItemCostRequirements",
                newName: "Worth_SilverPieces");

            migrationBuilder.RenameColumn(
                name: "GoldPieces",
                table: "ItemCostRequirements",
                newName: "Worth_GoldPieces");

            migrationBuilder.RenameColumn(
                name: "CopperPieces",
                table: "ItemCostRequirements",
                newName: "Worth_CopperPieces");

            migrationBuilder.AddColumn<bool>(
                name: "IsMagic",
                table: "Powers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRanged",
                table: "Powers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CriticalHit",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RollerId",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SavingThrow",
                table: "EffectGroups",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "DurationLeft",
                table: "EffectGroups",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "DifficultyClassToBreak",
                table: "EffectGroups",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "TemporaryHitpoints",
                table: "Characters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EffectInstances_RollerId",
                table: "EffectInstances",
                column: "RollerId");

            migrationBuilder.AddForeignKey(
                name: "FK_EffectInstances_Characters_RollerId",
                table: "EffectInstances",
                column: "RollerId",
                principalTable: "Characters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EffectInstances_Characters_RollerId",
                table: "EffectInstances");

            migrationBuilder.DropIndex(
                name: "IX_EffectInstances_RollerId",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "IsMagic",
                table: "Powers");

            migrationBuilder.DropColumn(
                name: "IsRanged",
                table: "Powers");

            migrationBuilder.DropColumn(
                name: "CriticalHit",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "RollerId",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "TemporaryHitpoints",
                table: "Characters");

            migrationBuilder.RenameColumn(
                name: "Worth_SilverPieces",
                table: "ItemCostRequirements",
                newName: "SilverPieces");

            migrationBuilder.RenameColumn(
                name: "Worth_GoldPieces",
                table: "ItemCostRequirements",
                newName: "GoldPieces");

            migrationBuilder.RenameColumn(
                name: "Worth_CopperPieces",
                table: "ItemCostRequirements",
                newName: "CopperPieces");

            migrationBuilder.AlterColumn<int>(
                name: "SavingThrow",
                table: "EffectGroups",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DurationLeft",
                table: "EffectGroups",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DifficultyClassToBreak",
                table: "EffectGroups",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SavingThrowRetakenEveryTurn",
                table: "EffectGroups",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
