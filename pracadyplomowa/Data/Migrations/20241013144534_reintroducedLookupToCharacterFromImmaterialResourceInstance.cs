using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class reintroducedLookupToCharacterFromImmaterialResourceInstance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImmaterialResourceInstances_Characters_CharacterId",
                table: "ImmaterialResourceInstances");

            migrationBuilder.RenameColumn(
                name: "CharacterId",
                table: "ImmaterialResourceInstances",
                newName: "R_CharacterId");

            migrationBuilder.RenameIndex(
                name: "IX_ImmaterialResourceInstances_CharacterId",
                table: "ImmaterialResourceInstances",
                newName: "IX_ImmaterialResourceInstances_R_CharacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImmaterialResourceInstances_Characters_R_CharacterId",
                table: "ImmaterialResourceInstances",
                column: "R_CharacterId",
                principalTable: "Characters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImmaterialResourceInstances_Characters_R_CharacterId",
                table: "ImmaterialResourceInstances");

            migrationBuilder.RenameColumn(
                name: "R_CharacterId",
                table: "ImmaterialResourceInstances",
                newName: "CharacterId");

            migrationBuilder.RenameIndex(
                name: "IX_ImmaterialResourceInstances_R_CharacterId",
                table: "ImmaterialResourceInstances",
                newName: "IX_ImmaterialResourceInstances_CharacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImmaterialResourceInstances_Characters_CharacterId",
                table: "ImmaterialResourceInstances",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id");
        }
    }
}
