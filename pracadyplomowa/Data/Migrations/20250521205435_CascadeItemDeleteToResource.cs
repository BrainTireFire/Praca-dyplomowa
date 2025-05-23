using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class CascadeItemDeleteToResource : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImmaterialResourceInstances_Items_R_ItemId",
                table: "ImmaterialResourceInstances");

            migrationBuilder.AddForeignKey(
                name: "FK_ImmaterialResourceInstances_Items_R_ItemId",
                table: "ImmaterialResourceInstances",
                column: "R_ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImmaterialResourceInstances_Items_R_ItemId",
                table: "ImmaterialResourceInstances");

            migrationBuilder.AddForeignKey(
                name: "FK_ImmaterialResourceInstances_Items_R_ItemId",
                table: "ImmaterialResourceInstances",
                column: "R_ItemId",
                principalTable: "Items",
                principalColumn: "Id");
        }
    }
}
