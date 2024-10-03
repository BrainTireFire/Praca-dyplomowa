using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedItemTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EffectBlueprints_ItemFamilies_R_GrantsProficiencyInItemFamilyId",
                table: "EffectBlueprints");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectInstances_ItemFamilies_R_GrantsProficiencyInItemFamilyId",
                table: "EffectInstances");

            migrationBuilder.AlterColumn<int>(
                name: "ItemType",
                table: "ItemFamilies",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "ItemType",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ItemType",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EffectBlueprints_ItemFamilies_R_GrantsProficiencyInItemFamilyId",
                table: "EffectBlueprints",
                column: "R_GrantsProficiencyInItemFamilyId",
                principalTable: "ItemFamilies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EffectInstances_ItemFamilies_R_GrantsProficiencyInItemFamilyId",
                table: "EffectInstances",
                column: "R_GrantsProficiencyInItemFamilyId",
                principalTable: "ItemFamilies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EffectBlueprints_ItemFamilies_R_GrantsProficiencyInItemFamilyId",
                table: "EffectBlueprints");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectInstances_ItemFamilies_R_GrantsProficiencyInItemFamilyId",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "ItemType",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "ItemType",
                table: "EffectBlueprints");

            migrationBuilder.AlterColumn<string>(
                name: "ItemType",
                table: "ItemFamilies",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_EffectBlueprints_ItemFamilies_R_GrantsProficiencyInItemFamilyId",
                table: "EffectBlueprints",
                column: "R_GrantsProficiencyInItemFamilyId",
                principalTable: "ItemFamilies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EffectInstances_ItemFamilies_R_GrantsProficiencyInItemFamilyId",
                table: "EffectInstances",
                column: "R_GrantsProficiencyInItemFamilyId",
                principalTable: "ItemFamilies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
