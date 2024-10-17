using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class versatileWeapons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Versatile",
                table: "MeleeWeapons");

            migrationBuilder.AddColumn<int>(
                name: "VersatileDamageValueId",
                table: "MeleeWeapons",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "OccupiesAllSlots",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_MeleeWeapons_VersatileDamageValueId",
                table: "MeleeWeapons",
                column: "VersatileDamageValueId");

            migrationBuilder.AddForeignKey(
                name: "FK_MeleeWeapons_DiceSet_VersatileDamageValueId",
                table: "MeleeWeapons",
                column: "VersatileDamageValueId",
                principalTable: "DiceSet",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeleeWeapons_DiceSet_VersatileDamageValueId",
                table: "MeleeWeapons");

            migrationBuilder.DropIndex(
                name: "IX_MeleeWeapons_VersatileDamageValueId",
                table: "MeleeWeapons");

            migrationBuilder.DropColumn(
                name: "VersatileDamageValueId",
                table: "MeleeWeapons");

            migrationBuilder.DropColumn(
                name: "OccupiesAllSlots",
                table: "Items");

            migrationBuilder.AddColumn<bool>(
                name: "Versatile",
                table: "MeleeWeapons",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
