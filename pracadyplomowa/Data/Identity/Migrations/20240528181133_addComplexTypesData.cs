using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Identity.Migrations
{
    /// <inheritdoc />
    public partial class addComplexTypesData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "HitDie_d8",
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "HitDie_d8",
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
        }
    }
}
