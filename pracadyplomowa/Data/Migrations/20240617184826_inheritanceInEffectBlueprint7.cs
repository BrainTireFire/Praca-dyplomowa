using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Migrations
{
    /// <inheritdoc />
    public partial class inheritanceInEffectBlueprint7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EffectBlueprints_ItemFamilies_ItemFamilyId",
                table: "EffectBlueprints");

            migrationBuilder.DropIndex(
                name: "IX_EffectBlueprints_ItemFamilyId",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "ItemFamilyId",
                table: "EffectBlueprints");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemFamilyId",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EffectBlueprints_ItemFamilyId",
                table: "EffectBlueprints",
                column: "ItemFamilyId");

            migrationBuilder.AddForeignKey(
                name: "FK_EffectBlueprints_ItemFamilies_ItemFamilyId",
                table: "EffectBlueprints",
                column: "ItemFamilyId",
                principalTable: "ItemFamilies",
                principalColumn: "Id");
        }
    }
}
