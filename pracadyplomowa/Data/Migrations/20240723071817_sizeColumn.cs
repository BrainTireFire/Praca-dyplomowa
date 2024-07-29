using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class sizeColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EffectInstances_ItemFamilies_R_GrantsProficiencyInItemFamilyId",
                table: "EffectInstances");

            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "Races",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SizeEffectType_SizeBonus",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SizeEffectType_SizeBonus",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_EffectInstances_ItemFamilies_R_GrantsProficiencyInItemFamilyId",
                table: "EffectInstances",
                column: "R_GrantsProficiencyInItemFamilyId",
                principalTable: "ItemFamilies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EffectInstances_ItemFamilies_R_GrantsProficiencyInItemFamilyId",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Races");

            migrationBuilder.DropColumn(
                name: "SizeEffectType_SizeBonus",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "SizeEffectType_SizeBonus",
                table: "EffectBlueprints");

            migrationBuilder.AddForeignKey(
                name: "FK_EffectInstances_ItemFamilies_R_GrantsProficiencyInItemFamilyId",
                table: "EffectInstances",
                column: "R_GrantsProficiencyInItemFamilyId",
                principalTable: "ItemFamilies",
                principalColumn: "Id");
        }
    }
}
