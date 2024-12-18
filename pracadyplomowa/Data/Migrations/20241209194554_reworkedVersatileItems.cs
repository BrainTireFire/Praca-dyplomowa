using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class reworkedVersatileItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeleeWeapons_DiceSet_VersatileDamageValueId",
                table: "MeleeWeapons");

            migrationBuilder.AlterColumn<int>(
                name: "VersatileDamageValueId",
                table: "MeleeWeapons",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Versatile",
                table: "MeleeWeapons",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_MeleeWeapons_DiceSet_VersatileDamageValueId",
                table: "MeleeWeapons",
                column: "VersatileDamageValueId",
                principalTable: "DiceSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeleeWeapons_DiceSet_VersatileDamageValueId",
                table: "MeleeWeapons");

            migrationBuilder.DropColumn(
                name: "Versatile",
                table: "MeleeWeapons");

            migrationBuilder.AlterColumn<int>(
                name: "VersatileDamageValueId",
                table: "MeleeWeapons",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_MeleeWeapons_DiceSet_VersatileDamageValueId",
                table: "MeleeWeapons",
                column: "VersatileDamageValueId",
                principalTable: "DiceSet",
                principalColumn: "Id");
        }
    }
}
