using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Migrations
{
    /// <inheritdoc />
    public partial class inheritanceInEffectBlueprint5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "diceSet_d10",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "diceSet_d100",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "diceSet_d12",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "diceSet_d20",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "diceSet_d4",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "diceSet_d6",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "diceSet_d8",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "diceSet_flat",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "diceSet_d10",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "diceSet_d100",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "diceSet_d12",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "diceSet_d20",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "diceSet_d4",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "diceSet_d6",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "diceSet_d8",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "diceSet_flat",
                table: "EffectBlueprints");
        }
    }
}
