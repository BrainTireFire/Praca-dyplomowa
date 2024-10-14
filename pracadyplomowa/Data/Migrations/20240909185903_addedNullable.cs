using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EffectBlueprints_Items_R_CreatedByEquippingId",
                table: "EffectBlueprints");

            migrationBuilder.AlterColumn<int>(
                name: "R_CreatedByEquippingId",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_EffectBlueprints_Items_R_CreatedByEquippingId",
                table: "EffectBlueprints",
                column: "R_CreatedByEquippingId",
                principalTable: "Items",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EffectBlueprints_Items_R_CreatedByEquippingId",
                table: "EffectBlueprints");

            migrationBuilder.AlterColumn<int>(
                name: "R_CreatedByEquippingId",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EffectBlueprints_Items_R_CreatedByEquippingId",
                table: "EffectBlueprints",
                column: "R_CreatedByEquippingId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
