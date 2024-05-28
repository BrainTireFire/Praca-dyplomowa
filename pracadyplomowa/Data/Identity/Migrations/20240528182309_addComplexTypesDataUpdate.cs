using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Identity.Migrations
{
    /// <inheritdoc />
    public partial class addComplexTypesDataUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Price_CopperPieces",
                table: "ShopItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Price_GoldPieces",
                table: "ShopItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Price_SilverPieces",
                table: "ShopItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price_CopperPieces",
                table: "ShopItems");

            migrationBuilder.DropColumn(
                name: "Price_GoldPieces",
                table: "ShopItems");

            migrationBuilder.DropColumn(
                name: "Price_SilverPieces",
                table: "ShopItems");
        }
    }
}
