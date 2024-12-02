using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatingItemCostRequirement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "ItemCostRequirements",
                newName: "SilverPieces");

            migrationBuilder.AddColumn<int>(
                name: "CopperPieces",
                table: "ItemCostRequirements",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GoldPieces",
                table: "ItemCostRequirements",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CopperPieces",
                table: "ItemCostRequirements");

            migrationBuilder.DropColumn(
                name: "GoldPieces",
                table: "ItemCostRequirements");

            migrationBuilder.RenameColumn(
                name: "SilverPieces",
                table: "ItemCostRequirements",
                newName: "Value");
        }
    }
}
