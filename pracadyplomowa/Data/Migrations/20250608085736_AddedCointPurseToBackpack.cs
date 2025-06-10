using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedCointPurseToBackpack : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CoinSack_CopperPieces",
                table: "Backpacks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CoinSack_GoldPieces",
                table: "Backpacks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CoinSack_SilverPieces",
                table: "Backpacks",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoinSack_CopperPieces",
                table: "Backpacks");

            migrationBuilder.DropColumn(
                name: "CoinSack_GoldPieces",
                table: "Backpacks");

            migrationBuilder.DropColumn(
                name: "CoinSack_SilverPieces",
                table: "Backpacks");
        }
    }
}
