using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class FieldEnumTypeAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FieldMovementCost",
                table: "Fields",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Boards",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SizeX",
                table: "Boards",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SizeY",
                table: "Boards",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FieldMovementCost",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "SizeX",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "SizeY",
                table: "Boards");
        }
    }
}
