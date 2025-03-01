using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class SpecifiedItemAsPrincipalForEffectsOnEquip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EffectInstances_Items_R_GrantedByEquippingItemId",
                table: "EffectInstances");

            migrationBuilder.AddForeignKey(
                name: "FK_EffectInstances_Items_R_GrantedByEquippingItemId",
                table: "EffectInstances",
                column: "R_GrantedByEquippingItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EffectInstances_Items_R_GrantedByEquippingItemId",
                table: "EffectInstances");

            migrationBuilder.AddForeignKey(
                name: "FK_EffectInstances_Items_R_GrantedByEquippingItemId",
                table: "EffectInstances",
                column: "R_GrantedByEquippingItemId",
                principalTable: "Items",
                principalColumn: "Id");
        }
    }
}
