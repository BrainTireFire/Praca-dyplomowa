using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class CascadeBackpackDeleteToItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Backpacks_R_BackpackHasItemId",
                table: "Items");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Backpacks_R_BackpackHasItemId",
                table: "Items",
                column: "R_BackpackHasItemId",
                principalTable: "Backpacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Backpacks_R_BackpackHasItemId",
                table: "Items");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Backpacks_R_BackpackHasItemId",
                table: "Items",
                column: "R_BackpackHasItemId",
                principalTable: "Backpacks",
                principalColumn: "Id");
        }
    }
}
