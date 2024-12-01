using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class aligningPowerBackendWithFrontend : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpcastByCharacterLevel",
                table: "Powers");

            migrationBuilder.DropColumn(
                name: "UpcastByClassLevel",
                table: "Powers");

            migrationBuilder.DropColumn(
                name: "UpcastByResourceLevel",
                table: "Powers");

            migrationBuilder.DropColumn(
                name: "EffectType",
                table: "EffectBlueprints");

            migrationBuilder.AddColumn<int>(
                name: "UpcastBy",
                table: "Powers",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpcastBy",
                table: "Powers");

            migrationBuilder.AddColumn<bool>(
                name: "UpcastByCharacterLevel",
                table: "Powers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UpcastByClassLevel",
                table: "Powers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UpcastByResourceLevel",
                table: "Powers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "EffectType",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
