using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class EffectSubtypeChangeForItemFamily : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EffectInstances_ItemFamilies_ItemFamilyId",
                table: "EffectInstances");

            migrationBuilder.DropIndex(
                name: "IX_EffectInstances_ItemFamilyId",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "ItemFamilyId",
                table: "EffectInstances");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemFamilyId",
                table: "EffectInstances",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EffectInstances_ItemFamilyId",
                table: "EffectInstances",
                column: "ItemFamilyId");

            migrationBuilder.AddForeignKey(
                name: "FK_EffectInstances_ItemFamilies_ItemFamilyId",
                table: "EffectInstances",
                column: "ItemFamilyId",
                principalTable: "ItemFamilies",
                principalColumn: "Id");
        }
    }
}
