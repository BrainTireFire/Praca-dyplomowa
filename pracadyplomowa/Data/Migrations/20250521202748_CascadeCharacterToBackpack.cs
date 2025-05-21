using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class CascadeCharacterToBackpack : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Backpacks_R_CharacterHasBackpackId",
                table: "Characters");

            migrationBuilder.DropIndex(
                name: "IX_Characters_R_CharacterHasBackpackId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "R_CharacterHasBackpackId",
                table: "Characters");

            migrationBuilder.AddColumn<int>(
                name: "R_BackpackOfCharacterId",
                table: "Backpacks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Backpacks_R_BackpackOfCharacterId",
                table: "Backpacks",
                column: "R_BackpackOfCharacterId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Backpacks_Characters_R_BackpackOfCharacterId",
                table: "Backpacks",
                column: "R_BackpackOfCharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Backpacks_Characters_R_BackpackOfCharacterId",
                table: "Backpacks");

            migrationBuilder.DropIndex(
                name: "IX_Backpacks_R_BackpackOfCharacterId",
                table: "Backpacks");

            migrationBuilder.DropColumn(
                name: "R_BackpackOfCharacterId",
                table: "Backpacks");

            migrationBuilder.AddColumn<int>(
                name: "R_CharacterHasBackpackId",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Characters_R_CharacterHasBackpackId",
                table: "Characters",
                column: "R_CharacterHasBackpackId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Backpacks_R_CharacterHasBackpackId",
                table: "Characters",
                column: "R_CharacterHasBackpackId",
                principalTable: "Backpacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
